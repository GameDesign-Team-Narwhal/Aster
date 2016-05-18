using UnityEngine;
using System.Collections;
using System;

public class AIEnemyShip : MonoBehaviour, IShootable
{
	private Rigidbody2D body2d;
	private PelletShooter pelletShooter;
	public float maxTurningTorque = 4; //effectively a P constant
	public float forwardThrust = 35;
    public float shootingRange = 100; //distance from the player at which the AI will begin to shoot 
	public float shotCooldown = .25f; // cooldown time before the AI can shoot again

    public float shotPredictionFactor = 50;
    public float shotDistancePredictionFactor = .1f;

    //if the player is moving slower than this around the AI, shot prediction will be disabled.
    public float playerAngularSpeedThreshold = 50;

	public int health = 2;
    public uint pointValue = 0;
	public string team = "Enemies";

	float lastShotTime = 0f;
    PolarVec2 prevTargetPos;

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
	void FixedUpdate () {
        if (GameController.instance.playerShipInstance != null) //this is null if the game is not started
        {

            Vector2 targetPosition = GameController.instance.playerShipInstance.transform.position;

            PolarVec2 targetPosPolar = PolarVec2.FromCartesian(targetPosition - ((Vector2)transform.position));

			PolarVec2 targetSpeed = (targetPosPolar - prevTargetPos) / Time.deltaTime;


            PolarVec2 predictedTargetPos;
            if (Mathf.Abs(targetSpeed.r) < playerAngularSpeedThreshold)
            {
                predictedTargetPos = targetPosPolar;
            }
            else
            {
                predictedTargetPos = targetPosPolar + targetSpeed * Time.deltaTime * shotPredictionFactor * shotDistancePredictionFactor * targetPosPolar.r;
            }


            //turn towards player
            float angleError = predictedTargetPos.A - body2d.rotation - 90;


            float newAngularVelocity = maxTurningTorque * angleError;

            if(!float.IsNaN(newAngularVelocity))
            {
                body2d.angularVelocity = newAngularVelocity;
            }

            //move at constant speed
            body2d.velocity = new PolarVec2(body2d.rotation + 90, forwardThrust).Cartesian2D;

            //shoot when in range
            if (Vector2.Distance(transform.position, targetPosition) < shootingRange)
            {
                if (Time.time - lastShotTime > shotCooldown)
                {
                    pelletShooter.Shoot();

                    lastShotTime = Time.time;
                }
            }


            prevTargetPos = targetPosPolar;
        }
	}


	public void OnShotBy(GameObject shooter, string team, int damage)
	{
        health -= damage;

		if(health <= 0 )
		{
			GameObject.Destroy(gameObject);
			
			GameController.instance.AddScore(pointValue);
		}
    }

	public string GetTeam()
	{
		return team;
	}
}
