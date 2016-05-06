using UnityEngine;
using System.Collections;
using System;

public class AILightShip : MonoBehaviour, IShootable
{
	private Rigidbody2D body2d;
	private PelletShooter pelletShooter;
	public float maxTurningTorque = 5000f; //effectively a P constant
	public float forwardThrust = 1000f;
    public float shootingRange = 100; //distance from the player at which the AI will begin to shoot 
	public float shotCooldown = .25f; // cooldown time before the AI can shoot again
	Vector2 Pos = new Vector2(0,0);
	Vector2 LastPos;
	bool counter = false;
	bool counterofcounters = false;
	float CurrentSlopeY = 0f;
	float CurrentSlopeX = 0f;
	float lastShotTime = 0f;
	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
		pelletShooter = GetComponent<PelletShooter>();


	}
	void Start () {

        transform.position = GameController.instance.RandomLocationInLevel();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.instance.playerShipInstance != null) //this is null if the game is not started
        {

			Vector2 playerPosition = new Vector2(GameController.instance.playerShipInstance.transform.position.x + 10*CurrentSlopeX, GameController.instance.playerShipInstance.transform.position.y + 10*CurrentSlopeY) ;

            //turn towards player
            float angleError = Utils.VecAngle((playerPosition - ((Vector2)transform.position))) - Utils.VecAngle(Utils.VecFromAngleMagnitude(body2d.rotation + 90, 1));

            body2d.angularVelocity = maxTurningTorque * angleError;

            //move at constant speed
            body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation + 90, forwardThrust));

            //shoot when in range
            if (Vector2.Distance(transform.position, playerPosition) < shootingRange)
            {
                if (Time.time - lastShotTime > shotCooldown)
                {
                    pelletShooter.Shoot();

                    lastShotTime = Time.time;
                }
            }
        }
	}
	void FixedUpdate(){
		LastPos = Pos;
		Pos = GameController.instance.playerShipInstance.transform.position;
		CurrentSlopeY = (LastPos.y - Pos.y);
		CurrentSlopeX = (LastPos.x - Pos.x);
	}


	public void OnShotBy(GameObject shooter)
	{
        if(shooter.GetComponent("AILightShip") == null)
        {
            GameObject.Destroy(gameObject);

            GameController.instance.AddScore(4);
        }
    }
}
