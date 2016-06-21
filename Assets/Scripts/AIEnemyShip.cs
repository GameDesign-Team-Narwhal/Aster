using UnityEngine;
using System.Collections;
using System;

public class AIEnemyShip : MonoBehaviour, IShootable
{
	private Animator animator;
	private Rigidbody2D body2d;
	private PelletShooter pelletShooter;
	public float maxTurningTorque = 4; //effectively a P constant
	public float forwardThrust = 35;
    public float shootingRange = 100; //distance from the player at which the AI will begin to shoot 
	public float shotCooldown = .25f; // cooldown time before the AI can shoot again

    public float shotPredictionFactor = 1;
    public float shotDistancePredictionFactor = .1f;

    //if the player is moving slower than this around the AI, shot prediction will be disabled.
    public float playerAngularSpeedThreshold = 50;
    public GameObject[] upgrades;
    public int[] upgradeProb;
    public int health = 2;
    public uint pointValue = 0;
	public string team = "Enemies";
	public bool Desabled = false;
	public float DesabledTime = 0;
	public float TimeStartDesabled = 0f;
	float lastShotTime = 0f;
    PolarVec2 prevTargetPos;

	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
		pelletShooter = GetComponent<PelletShooter>();
		animator = GetComponent<Animator>();
	}
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(GameController.instance == null)
        {
            Debug.LogError("GameController instance is null!!!!! AAAAAAAAAAAAHHHHHHHHH!!!!!!!!!  This is VERY BAD!!!!!!!");
            return; 
        }

        if (GameController.instance.playerShipInstance != null && Desabled == false) //this is null if the game is not started
        {

            Vector2 targetPosition = GameController.instance.playerShipInstance.transform.position;

            PolarVec2 targetPosPolar = PolarVec2.FromCartesian(targetPosition - ((Vector2)transform.position));

			PolarVec2 targetSpeed = (targetPosPolar - prevTargetPos) / Time.deltaTime;


            PolarVec2 predictedTargetPos;
           // if (Mathf.Abs(targetSpeed.r) < playerAngularSpeedThreshold)
           // {
          //      predictedTargetPos = targetPosPolar;
          //  }
          //  else
          //  {
                predictedTargetPos = targetPosPolar + targetSpeed * (shotPredictionFactor/pelletShooter.pelletSpeed);
           // }


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

		if(Time.time - TimeStartDesabled > DesabledTime)
		{
			Desabled = false;
		}
		animator.SetBool ("Desabled", Desabled);

	}


	public void OnShotBy(GameObject shooter, string team, int damage, float ion)
	{
        health -= damage;

		if(health <= 0 )
		{
			animator.SetTrigger("Destruction");
            for (uint counter = 0; counter < upgrades.Length; ++counter)
            {
                int prob = upgradeProb[counter];
                int toSpawn = (int)UnityEngine.Random.Range(1, 100);
                if (prob >= toSpawn)
                {
                    GameObject newUpgrade = GameObject.Instantiate(upgrades[counter]);
                    newUpgrade.transform.position = transform.position;
                }
            }
        }
		if (ion != 0) {
			Desabled = true;
			DesabledTime = ion;
			TimeStartDesabled = Time.time;
		}
    }

	public string GetTeam()
	{
		return team;
	}
	public void OnDeath()
	{
		GameObject.Destroy(gameObject);
		
		GameController.instance.AddScore(pointValue);
	}
}
