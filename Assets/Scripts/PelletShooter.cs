using UnityEngine;
using System.Collections;

public class PelletShooter : MonoBehaviour {

	public GameObject pelletPrefab;

	public float pelletSpeed;

	Rigidbody2D body2D;

	void Awake()
	{
		body2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space))
		{
			GameObject pellet = GameObject.Instantiate(pelletPrefab);

			Rigidbody2D pelletBody = pellet.GetComponent<Rigidbody2D>();

			pelletBody.velocity = Utils.VecFromAngleMagnitude(pelletBody.rotation, pelletSpeed);
		}
	}
}
