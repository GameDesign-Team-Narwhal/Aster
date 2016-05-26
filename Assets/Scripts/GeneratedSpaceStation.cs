using UnityEngine;
using System.Collections.Generic;

public class GeneratedSpaceStation : MonoBehaviour {

	public List<Vector2> attachmentPoints = new List<Vector2>();
	public List<float> attachmentAngles = new List<float>();
	public GameObject markerPrefab;
	public bool visualizeAttachments = true;


	private List<GameObject> attachmentMarkers;
	private uint numAttachments;

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
