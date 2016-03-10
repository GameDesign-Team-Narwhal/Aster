using UnityEngine;
using System.Collections;
using System;

public class AILightShip : MonoBehaviour, IShootable
{
	private Rigidbody2D body2d;
	private PelletShooter pelletShooter;
	public float SpawnArea = 1f;
	public float turningTorque = 5000f;
	public float forwardThrust = 10000f;
	public float translationalDecelerationFactor = 100f; //force per unit velocity
	public float rotationalDecelerationFactor = .01f; //torque per angular velocity
	public float shotCooldown = .25f; // cooldown time before the plyer can shoot again

	public float moveCooldown = 1f; 
	float lastMoveTime = 0f;
	float lastShotTime = 0f;
	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
		pelletShooter = GetComponent<PelletShooter>();
	}
	void Start () {
		var Angle = UnityEngine.Random.Range (0, 360);
		var SpawnLocationX = body2d.transform.position.x;
		var SpawnLocationY = body2d.transform.position.y;

			SpawnLocationX += UnityEngine.Random.Range(-SpawnArea, SpawnArea);
			SpawnLocationY += UnityEngine.Random.Range(-SpawnArea, SpawnArea);
		transform.position = new Vector2 (SpawnLocationX, SpawnLocationY);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - lastMoveTime > moveCooldown) {
			movement();
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			if(Time.time - lastShotTime > shotCooldown)
			{
				pelletShooter.Shoot();
				lastShotTime = Time.time;
			}
		}
	}


	void movement ()
	{
		body2d.AddTorque(0);
		var move = UnityEngine.Random.Range (0, 3);
		if (move == 0)
		{
			body2d.AddTorque(turningTorque);
		}
		if(move == 1)
		{
			body2d.AddTorque(-turningTorque);
		}
		
		
		if (move == 2)
		{
			body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation + 90, forwardThrust));
		}
		else if (move == 3)
		{
			Vector2 decelerationForce = new Vector2(-1 * body2d.velocity.x * translationalDecelerationFactor, -1 * body2d.velocity.y * translationalDecelerationFactor);
			body2d.AddForce(decelerationForce);
			
			body2d.AddTorque(-1 * body2d.angularVelocity * rotationalDecelerationFactor);
		}
		lastMoveTime = Time.time;
	}
	public void OnShotBy(GameObject shooter)
	{
		GameObject.Destroy(gameObject);
	}
}
