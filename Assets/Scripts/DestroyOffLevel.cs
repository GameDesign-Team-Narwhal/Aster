using UnityEngine;
using System.Collections;

public class DestroyOffLevel : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		



		if (Mathf.Abs(transform.position.x) > GameController.instance.levelSizePx.x / 2)
        {
			//Debug.Log("thing crossed x boundary");
			Vector3 pos = transform.position;
			pos.x *= -1;
			transform.position = pos;
		}


		
		if (Mathf.Abs(transform.position.y) > GameController.instance.levelSizePx.y / 2)
		{
			//Debug.Log("thing crossed y boundary");
			Vector3 pos = transform.position;
			pos.y *= -1;
			transform.position = pos;
		}

		
	}
	

}


