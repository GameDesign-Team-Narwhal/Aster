using UnityEngine;
using System.Collections;
using System;

public class PlayerControlledShip : MonoBehaviour, IShootable
{
    private Rigidbody2D body2d;
    private PelletShooter pelletShooter;
    private Animator animator;

    public float turningTorque = 5000f;
    public float forwardThrust = 10000f;
    public float translationalDecelerationFactor = 100f; //force per unit velocity
    public float rotationalDecelerationFactor = .01f; //torque per angular velocity
	public uint maxShields = 1000;
	public float shieldEnergy = 1000;
	public float shieldUsePerSec = 10;
	public float shieldRegenPerSec = 2;

	public GameObject shieldsSprite;
    private ColorOscillator shieldsColorer;

	public bool shieldsActive = false;

	public String team = "Player";


	public bool Desabled = false;
	public float DesabledTime = 1.5f;
	float TimeStartDesabled = 0f;
    float lastShotTime = 0f;
    // Use this for initialization
    void Awake ()
    {
        body2d = GetComponent<Rigidbody2D>();
        pelletShooter = GetComponent<PelletShooter>();
        animator = GetComponent<Animator>();

        shieldsColorer = shieldsSprite.GetComponent<ColorOscillator>();
    }
	
	// Update is called once per frame
	void Update () {

        bool engineUsed = false;


        //handle shields

        if (Input.GetKey(KeyCode.LeftShift) && shieldEnergy > 0)
        {
            shieldsSprite.SetActive(true);
            shieldEnergy -= shieldUsePerSec * Time.deltaTime;

            shieldsActive = true;
        }
        else
        {
            shieldsSprite.SetActive(false);

            if (shieldEnergy < maxShields)
            {
                shieldEnergy += Mathf.Clamp(shieldRegenPerSec * Time.deltaTime, 0, maxShields);
            }

            shieldsActive = false;

            //handle other input, if shields are not in use

            if (Input.GetKey(KeyCode.LeftArrow) && Desabled == false)
            {
                body2d.AddTorque(turningTorque);

                engineUsed = true;
            }
            if (Input.GetKey(KeyCode.RightArrow) && Desabled == false)
            {
                body2d.AddTorque(-turningTorque);

                engineUsed = true;
            }

            if (Input.GetKey(KeyCode.UpArrow) && Desabled == false)
            {
                body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation + 90, forwardThrust));
                engineUsed = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Desabled == false)
            {
                Vector2 decelerationForce = new Vector2(-1 * body2d.velocity.x * translationalDecelerationFactor, -1 * body2d.velocity.y * translationalDecelerationFactor);
                body2d.AddForce(decelerationForce);

                body2d.AddTorque(-1 * body2d.angularVelocity * rotationalDecelerationFactor);

                engineUsed = true;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (Time.time - lastShotTime > GameController.instance.playerCooldown)
                {
                    pelletShooter.Shoot();
                    lastShotTime = Time.time;
                }
            }
        }


        

        animator.SetBool("EngineOn", engineUsed);






		GameController.instance.shieldBar.UpdateBar(shieldEnergy, maxShields);
		if (Time.time - TimeStartDesabled > DesabledTime) {
			Desabled = false;
		}
    }

    public void OnShotBy(GameObject shooter, string team, int damage)
    {
		if(shieldsActive)
		{
			shieldsColorer.OscillateOnce();
		}
		else
		{
			GameController.instance.Damage(damage);
			
			if (shooter.GetComponent<PelletShooter>() != null) 
			{
				PelletShooter PS = shooter.GetComponent<PelletShooter>();
				if(PS.pelletPrefab.name.Contains("Ion"))
				{
				Desabled = true;
				TimeStartDesabled = Time.time;
				}
			}
		}
		

      
    }

	public string GetTeam()
	{
		return team;
      
    }
}
