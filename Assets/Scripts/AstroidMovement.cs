using UnityEngine;
using System.Collections;

public class AstroidMovement : MonoBehaviour {

 Rigidbody2D body2d;

    //base speed, not randomized
	public float AsteroidSpeed = 1f;
	public float SpeedOffset = 1f;
    public float maxRotSpeed = .1f;

    public bool fixedLocation = false;

	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
	}
	void Start () {
		var Angle = Random.Range (0, 360);
        Vector3 spawnLocation = body2d.transform.position;

        if (!fixedLocation)
        {
            spawnLocation = GameController.instance.RandomLocationInLevel();
        }
		body2d.velocity = Utils.VecFromAngleMagnitude (Angle, AsteroidSpeed + Random.Range (-SpeedOffset, SpeedOffset));
        body2d.angularVelocity = Random.Range(-maxRotSpeed, maxRotSpeed);
        transform.position = spawnLocation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
