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
		numAttachments = attachmentPoints.Count;

		if(visualizeAttachments)
		{
			attachmentMarkers = new List<GameObject>(numAttachments);
			
			for(int counter = 0; counter < numAttachments; ++counter)
			{
				attachmentMarkers.Add(GameObject.Instantiate(markerPrefab));
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(GameObject marker
	}
}
