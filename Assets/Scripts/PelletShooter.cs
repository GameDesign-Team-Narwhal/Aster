using UnityEngine;
using System.Collections;

public class PelletShooter : MonoBehaviour {

	public GameObject pelletPrefab;

	public float pelletSpeed;

    //controls whether the pellets move at an absolute speed or move relative to the firer's speed.
    public bool pelletsConstantSpeed = false;

    public Vector2[] firingLocations;

	Rigidbody2D body2D;

	void Awake()
	{
		body2D = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	public void Shoot ()
    {


        foreach(Vector2 firingLocation in firingLocations)
        {
            GameObject pellet = GameObject.Instantiate(pelletPrefab);

            pellet.transform.position = transform.TransformPoint(firingLocation);
            pellet.transform.rotation = transform.rotation;

            Rigidbody2D pelletBody = pellet.GetComponent<Rigidbody2D>();

            if (pelletsConstantSpeed)
            {
                pelletBody.velocity = new PolarVec2(body2D.rotation + 90, pelletSpeed).Cartesian2D;

            }
            else // the pellet's speed is affected by the speed of the firer
            {
                pelletBody.velocity = body2D.velocity + Utils.VecFromAngleMagnitude(body2D.rotation + 90, pelletSpeed);
            }

            pellet.GetComponent<Projectile>().shooter = gameObject;
        }

		
	}
}
