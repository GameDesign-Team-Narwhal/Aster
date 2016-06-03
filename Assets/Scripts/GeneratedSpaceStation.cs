using UnityEngine;
using System.Collections.Generic;

public class GeneratedSpaceStation : MonoBehaviour {
	
	public List<Vector2> attachmentPoints = new List<Vector2>();
	public List<float> attachmentAngles = new List<float>();
	public GameObject markerPrefab;
	public bool visualizeAttachments = true;
	public uint maxGenerationDepth = 4; // 1-indexed

	private List<GameObject> attachmentMarkers;
	private uint numAttachments;

	private bool isTopLevel = true;
	private List<GameObject> subParts;

	private static string STATION_PREFAB_BASE_PATH = "SpaceStation/";

	private static string[] STATION_PART_PREFABS = new string[]
	{
        "SS Large Connector",
        "SS Large Ring",
        "SS Small Connector",
        "SS Small Large Connector",
        "SS Small T",
        "SS Portal",
        "SS Small 4-Way",
        "SS Small Elbow",
        "SS Small Connector With Pod",
    };

    private static string[] STATION_END_CAP_PREFABS = new string[]
    {
            "SS Solar Panel",
            "SS Turret End Pod",
            "SS Shield Emitter",
            "SS Laser",

    };

    // Use this for initialization
    void Awake () 
	{
		if(attachmentPoints.Count != attachmentAngles.Count)
		{
			Debug.LogError("Mismatch between attachment point and angle list lengths!");
			return;
		}
		numAttachments = (uint)attachmentPoints.Count;

		subParts = new List<GameObject>((int)numAttachments);

		//pre-expand array to full length
		while(subParts.Count < numAttachments)
		{
			subParts.Add(null);
		}
		
        //can't visualize without a marker
        if(markerPrefab == null)
        {
            visualizeAttachments = false;
        }
	}

	void Start()
	{
		//if no other GeneratedSpaceStation has told us we AREN'T the top level, then assume we are and spawn sub-parts
		if(isTopLevel)
		{
			GenerateSubParts(1, maxGenerationDepth);

            if (visualizeAttachments)
            {
                attachmentMarkers = new List<GameObject>((int)numAttachments);

                for (int counter = 0; counter < numAttachments; ++counter)
                {
                    GameObject marker = GameObject.Instantiate(markerPrefab);

                    attachmentMarkers.Add(marker);
                }
            }
        }
        else
        {
            visualizeAttachments = false;
        }
	}

	void GenerateSubParts(uint currentDepth, uint maxDepth)
	{
		if(currentDepth <= maxDepth)
		{
            //Debug.Log(">>>Depth: " + currentDepth);

			for(int index = 0; index < numAttachments; ++index)
			{
				bool attachmentAvailable = subParts.Count <= index || subParts[index] == null;
				
				if(attachmentAvailable)
				{
                    string partPrefabPath;

                    if (currentDepth == maxDepth)
                    {
                        //generate pieces with no other attachments
                        partPrefabPath = STATION_PREFAB_BASE_PATH + STATION_END_CAP_PREFABS[Random.Range(0, STATION_END_CAP_PREFABS.Length)];
                    }
                    else
                    {
                        partPrefabPath = STATION_PREFAB_BASE_PATH + STATION_PART_PREFABS[Random.Range(0, STATION_PART_PREFABS.Length)];
                    }

                   // Debug.Log("Instantiating part: " + partPrefabPath);

					GameObject subPart = GameObject.Instantiate((GameObject)Resources.Load(partPrefabPath));

					//stop it from generating its own parts
					GeneratedSpaceStation subPartScript = subPart.GetComponent<GeneratedSpaceStation>();
					subPartScript.isTopLevel = false;

					int subPartAttachmentIndex = Mathf.FloorToInt(Random.Range(0, subPartScript.numAttachments));

                    float rotationAngle = Utils.NormalizeAngle(subPartScript.attachmentAngles[subPartAttachmentIndex] - attachmentAngles[index]);

                    float subPartAngle = Utils.NormalizeAngle(subPartScript.attachmentAngles[subPartAttachmentIndex]);

                    float angleDifference = Mathf.Abs(attachmentAngles[index] - subPartAngle);

                    if (angleDifference < 45 || (angleDifference > 135 && angleDifference < 225) || angleDifference > 315)
                    {
                        //no idea why this is necessary, but it is
                        rotationAngle += 180;
                        //positionAddSign = -1;
                        //swapAttachmentPosition = true;

                        // Debug.Log("Flipping next...");
                    }

                    //Debug.Log("Attachment angle: " + attachmentAngles[index] + ", other part AA: " + subPartAngle + ", planned angle: " + rotationAngle);

                    subPart.transform.localRotation = transform.rotation * subPart.transform.rotation * Quaternion.Euler(0, 0, rotationAngle);

					//its attachment needs to match ours
					subPart.transform.parent = transform;
					subPart.transform.localPosition = ((Vector3)attachmentPoints[index]) + (Quaternion.Euler(0, 0, subPartScript.transform.localRotation.eulerAngles.z - 180) * subPartScript.attachmentPoints[subPartAttachmentIndex]);
                    //Debug.Log("Attaching subpart (point " + subPartAttachmentIndex + ") in slot " + index);

                    int subPartIndexToFill = subPartAttachmentIndex;

                    subPartScript.subParts[subPartIndexToFill] = gameObject;
					subParts[index] = subPart;

					//have the sub-part generate its sub-sub-parts

					subPartScript.GenerateSubParts(currentDepth + 1, maxDepth);
				}
			}
		}

	}

	// Update is called once per frame
	void Update () 
	{
        if(visualizeAttachments)
        {
            for(int markerIndex = 0; markerIndex < numAttachments; ++markerIndex)
            {
                GameObject marker = attachmentMarkers[markerIndex];

                marker.transform.position = transform.TransformPoint(attachmentPoints[markerIndex]);
                marker.transform.rotation = Quaternion.Euler(0, 0, attachmentAngles[markerIndex]) * transform.rotation;
            }
        }
	}
}
