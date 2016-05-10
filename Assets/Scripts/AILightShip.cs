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

    public float shotPredictionFactor;

	Vector2 Pos = new Vector2(0,0);
	Vector2 LastPos;
	bool counter = false;
	bool counterofcounters = false;
	public int Health = 2;
	float CurrentSlopeY = 0f;
	float CurrentSlopeX = 0f;
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

            PolarVec2 predictedTargetPos = targetPosPolar + targetSpeed * Time.deltaTime * shotPredictionFactor;

            //turn towards player
            //float angleError = Utils.VecAngle((playerPosition - ((Vector2)transform.position))) - Utils.VecAngle(Utils.VecFromAngleMagnitude(body2d.rotation + 90, 1));

            float angleError = predictedTargetPos.A - body2d.rotation - 90;


            body2d.angularVelocity = maxTurningTorque * angleError;

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
			if(Health <= 0 )
			{
				GameObject.Destroy(gameObject);
				
				GameController.instance.AddScore(10);
			}


            prevTargetPos = targetPosPolar;
        }
	}


	public void OnShotBy(GameObject shooter)
	{
        if(shooter.GetComponent<AILightShip>() == null)
        {
			Health = Health- GameController.instance.PlayerDamge;
        }
    }
}
