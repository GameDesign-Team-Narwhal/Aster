using UnityEngine;
using System.Collections;

public class PlayerFollowingCamera : MonoBehaviour {
	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		Vector3 newPosition = player.transform.position;
		newPosition.z = -10;
		transform.position = newPosition;
	}
}
