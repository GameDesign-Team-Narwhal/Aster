using UnityEngine;
using System.Collections;
using System;

public class PlayerControlledShip : MonoBehaviour, IShootable
{
    private Rigidbody2D body2d;
    private PelletShooter pelletShooter;
    private Animator animator;
	private ColorOscillator shieldsColorer;

    public float turningTorque = 5000f;
    public float forwardThrust = 10000f;
    public float translationalDecelerationFactor = 100f; //force per unit velocity
    public float rotationalDecelerationFactor = .01f; //torque per angular velocity
    public float shotCooldown = .25f; // cooldown time before the player can shoot again
	public uint maxShields = 1000;
	public float shieldEnergy = 1000;
	public float shieldUsePerSec = 10;
	public float shieldRegenPerSec = 2;

	public GameObject shieldsSprite;

	public bool shieldsActive = false;

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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
			body2d.AddTorque(turningTorque);

            engineUsed = true;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
			body2d.AddTorque(-turningTorque);

            engineUsed = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            body2d.AddForce(Utils.VecFromAngleMagnitude(body2d.rotation + 90, forwardThrust));
            engineUsed = true;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 decelerationForce = new Vector2(-1 * body2d.velocity.x * translationalDecelerationFactor, -1 * body2d.velocity.y * translationalDecelerationFactor);
            body2d.AddForce(decelerationForce);

            body2d.AddTorque(-1 * body2d.angularVelocity * rotationalDecelerationFactor);

            engineUsed = true;
        }

        animator.SetBool("EngineOn", engineUsed);

        if (Input.GetKey(KeyCode.Space))
        {
            if(Time.time - lastShotTime > shotCooldown)
            {
                pelletShooter.Shoot();
                lastShotTime = Time.time;
            }
        }

		if(Input.GetKey(KeyCode.LeftShift) && shieldEnergy > 0)
		{
			shieldsSprite.SetActive(true);
			shieldEnergy -= shieldUsePerSec * Time.deltaTime;

			shieldsActive = true;
		}
		else
		{
			shieldsSprite.SetActive(false);
			shieldEnergy += shieldRegenPerSec * Time.deltaTime;

			shieldsActive = false;
		}
    }

    public void OnShotBy(GameObject shooter)
    {
		if(shieldsActive)
		{
			shieldsColorer.OscillateOnce();
		}
		else
			{if (shooter.GetComponent<AILightShip>() == null) {
				GameController.instance.Damage(300);
			} else{
				GameController.instance.Damage(100);
			}
		}
      
    }

//	void UpdateShieldbar()
//	{
//		Color tintColor;
//		
//		//deal with negative health values (player is dead)
//		uint clampedShield = shieldEnergy < 0 ? 0 : (uint)shieldEnergy;
//		float shieldPercentage = ((float)clampedShield) / shieldEnergy;
//
//		
//		
//		//calculate where to draw it
//		Rect drawnPart = new Rect(0, 0, shieldPercentage, 1);
//		
//		GameController.instance.shieldBarImage.uvRect = drawnPart;
//		//healthbarRawImage.color = tintColor;
//		
//		//now move and scale it to the right place
//		Vector2 shieldbarPos = GameController.instance.shieldBarImage.rectTransform.anchoredPosition;
//		
//		healthbarPos.x = initialXPos - ((initialWidth * (1 - healthPercentage)) / 2); //divide by 2 because it's anchored to the center
//		
//		healthbarRawImage.rectTransform.anchoredPosition = healthbarPos;
//		
//		healthbarRawImage.rectTransform.sizeDelta = new Vector2(healthPercentage * initialWidth, initialHeight);
//	}
}
