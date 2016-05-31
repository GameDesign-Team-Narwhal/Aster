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
        "SS Solar Panel",
        "SS Small Connector",
        "SS Small Large Connector",
        "SS Laser",
        "SS Small T",
        "SS Portal"
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
	}

	void GenerateSubParts(uint currentDepth, uint maxDepth)
	{
		if(currentDepth <= maxDepth)
		{
            Debug.Log(">>>Depth: " + currentDepth);

			for(int index = 0; index < numAttachments; ++index)
			{
				bool attachmentAvailable = subParts.Count <= index || subParts[index] == null;
				
				if(attachmentAvailable)
				{
					string partPrefabPath = STATION_PREFAB_BASE_PATH + STATION_PART_PREFABS[Random.Range(0, STATION_PART_PREFABS.Length)];
					GameObject subPart = GameObject.Instantiate((GameObject)Resources.Load(partPrefabPath));

					//stop it from generating its own parts
					GeneratedSpaceStation subPartScript = subPart.GetComponent<GeneratedSpaceStation>();
					subPartScript.isTopLevel = false;

					int subPartAttachmentIndex = Mathf.FloorToInt(Random.Range(0, subPartScript.numAttachments));

                    float rotationAngle = Utils.NormalizeAngle(attachmentAngles[index] + subPartScript.attachmentAngles[subPartAttachmentIndex]);

                    float subPartAngle = Utils.NormalizeAngle(subPartScript.attachmentAngles[subPartAttachmentIndex]);

                    float positionAddSign = 1;

                    bool swapAttachmentPosition = false;

                    if((subPartAngle > 135 && subPartAngle < 225) || subPartAngle < 45 || subPartAngle > 315)
                    {
                        //no idea why this is necessary, but it is
                        //subPartAngle += 180;
                        positionAddSign = -1;
                        swapAttachmentPosition = true;

                       // Debug.Log("Flipping next...");
                    }

                    //Debug.Log("Attachment angle: " + attachmentAngles[index] + ", other part AA: " + subPartAngle);

                    subPart.transform.localRotation = transform.rotation * subPart.transform.rotation * Quaternion.Euler(0, 0, rotationAngle);

					//its attachment needs to match ours
					subPart.transform.parent = transform;
					subPart.transform.localPosition = ((Vector3)attachmentPoints[index]) + positionAddSign * (Quaternion.Euler(0, 0, subPartScript.transform.localRotation.eulerAngles.z - 180) * subPartScript.attachmentPoints[subPartAttachmentIndex]);
                    Debug.Log("Attaching subpart (point " + subPartAttachmentIndex + ") in slot " + index);

                    int subPartIndexToFill = subPartAttachmentIndex;

                    if (swapAttachmentPosition)
                    {
                        for (int attachmentIndex = 0; attachmentIndex < subPartScript.numAttachments; ++attachmentIndex)
                        {
                            if (Mathf.Abs(Mathf.Abs(Utils.NormalizeAngle(subPartScript.attachmentAngles[subPartAttachmentIndex]) - Utils.NormalizeAngle(subPartScript.attachmentAngles[attachmentIndex])) - 180) < 1)
                            {
                                subPartIndexToFill = attachmentIndex;
                            }
                        }
                    }
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
