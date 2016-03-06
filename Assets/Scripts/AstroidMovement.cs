using UnityEngine;
using System.Collections;

public class AstroidMovement : MonoBehaviour {

 Rigidbody2D body2d;

    //base speed, not randomized
	public float AsteroidSpeed = 1f;
	public float SpeedOffset = 1f;
	public float SpawnArea = 1f;
    public float maxRotSpeed = .1f;

    public bool fixedLocation = false;

	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
	}
	void Start () {
		var Angle = Random.Range (0, 360);
        var SpawnLocationX = body2d.transform.position.x;
        var SpawnLocationY = body2d.transform.position.y;

        if (!fixedLocation)
        {
            SpawnLocationX += Random.Range(-SpawnArea, SpawnArea);
            SpawnLocationY += Random.Range(-SpawnArea, SpawnArea);
        }
		body2d.velocity = Utils.VecFromAngleMagnitude (Angle, AsteroidSpeed + Random.Range (-SpeedOffset, SpeedOffset));
        body2d.angularVelocity = Random.Range(-maxRotSpeed, maxRotSpeed);
		transform.position = new Vector2 (SpawnLocationX, SpawnLocationY);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
