using UnityEngine;
using System.Collections;

public class AstroidMovement : MonoBehaviour {

 Rigidbody2D body2d;
	public float AsteroidSpeed = 1f;
	public float SpeedOffset = 1f;

	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
	}
	void Start () {
		var Angle = Random.Range (0, 360);
		body2d.velocity = Utils.VecFromAngleMagnitude (Angle, AsteroidSpeed + Random.Range (-SpeedOffset, SpeedOffset));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
