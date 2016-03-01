using UnityEngine;
using System.Collections;

public class PelletShooter : MonoBehaviour {

	public GameObject pelletPrefab;

	public float pelletSpeed;

    public Vector2 firingLocation = new Vector2(0, 0);

	Rigidbody2D body2D;

	void Awake()
	{
		body2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject pellet = GameObject.Instantiate(pelletPrefab);

			pellet.transform.position = transform.TransformPoint(firingLocation);
            pellet.transform.rotation = transform.rotation;

			Rigidbody2D pelletBody = pellet.GetComponent<Rigidbody2D>();

			pelletBody.velocity = body2D.velocity + Utils.VecFromAngleMagnitude(body2D.rotation + 90, pelletSpeed);

            pellet.GetComponent<Projectile>().shooter = gameObject;
		}
	}
}
