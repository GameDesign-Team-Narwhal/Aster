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
		"SS Small Large Connector"
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

		if(visualizeAttachments)
		{
			attachmentMarkers = new List<GameObject>((int)numAttachments);
			
			for(int counter = 0; counter < numAttachments; ++counter)
			{
                GameObject marker = GameObject.Instantiate(markerPrefab);

                attachmentMarkers.Add(marker);
			}
		}

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
		}
	}

	void GenerateSubParts(uint currentDepth, uint maxDepth)
	{
		if(currentDepth <= maxDepth)
		{
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

					//its attachment needs to match ours
					Vector3 attachmentPosWorld = transform.TransformPoint(attachmentPoints[index]);
					subPart.transform.position = attachmentPosWorld + ((Vector3)subPartScript.attachmentPoints[subPartAttachmentIndex]);

					subPartScript.subParts[subPartAttachmentIndex] = gameObject;

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
                marker.transform.rotation = Quaternion.Euler(0, 0, attachmentAngles[markerIndex]);
            }
        }
	}
}
