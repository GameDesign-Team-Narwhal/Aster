using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {

    public bool horizontal = true;
    public bool vertical = false;

    public Vector2 desiredSpeed = Vector2.zero;

    Rigidbody2D body2d;

	// Use this for initialization
	void Awake () {
        body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        body2d.velocity = new Vector2(horizontal ? desiredSpeed.x : body2d.velocity.x , vertical ? desiredSpeed.y : body2d.velocity.y);
	}
}
