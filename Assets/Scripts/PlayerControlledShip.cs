using UnityEngine;
using System.Collections;

public class PlayerControlledShip : MonoBehaviour
{
    private Rigidbody2D body2d;

    public float turningTorque = 1f;
    public float forwardThrust = 1f;
    public float backwardThrust = .5f;


	// Use this for initialization
	void Awake ()
    {
        body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.LeftArrow))
        {
			body2d.rotation += turningTorque;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
			body2d.rotation += -turningTorque;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation + 90, forwardThrust));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation - 90, backwardThrust));
        }
    }
}
