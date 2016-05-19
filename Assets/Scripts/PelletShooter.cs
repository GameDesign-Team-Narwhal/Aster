using UnityEngine;
using System.Collections;

[AddComponentMenu("Damage/Pellet Shooter")]
public class PelletShooter : MonoBehaviour {

	public GameObject pelletPrefab;

	public float pelletSpeed;

    //controls whether the pellets move at an absolute speed or move relative to the firer's speed.
    public bool pelletsConstantSpeed = false;

	public uint pelletDamage = 2;
	public string pelletTeam = "";

    public Vector2[] firingLocations;
	public float rangeInSec;

    //if true, play the AudioSource on this component when a shot is fired
    public bool playSound;

	Rigidbody2D body2D;
    AudioSource audioSource;

	void Awake()
	{
		body2D = GetComponent<Rigidbody2D> ();
        audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	public void Shoot ()
    {


        foreach(Vector2 firingLocation in firingLocations)
        {
            GameObject pellet = GameObject.Instantiate(pelletPrefab);

            pellet.transform.position = transform.TransformPoint(firingLocation);
            pellet.transform.rotation = transform.rotation;

            Rigidbody2D pelletBody = pellet.GetComponent<Rigidbody2D>();

            if (pelletsConstantSpeed)
            {
                pelletBody.velocity = new PolarVec2(body2D.rotation + 90, pelletSpeed).Cartesian2D;

            }
            else // the pellet's speed is affected by the speed of the firer
            {
                pelletBody.velocity = body2D.velocity + Utils.VecFromAngleMagnitude(body2D.rotation + 90, pelletSpeed);
            }

			//set projectile's properties
			Projectile projectileScript = pellet.GetComponent<Projectile>();
			projectileScript.damage = (int)pelletDamage;
			projectileScript.shooter = gameObject;
			projectileScript.team = pelletTeam;
        }

        if (playSound)
        {
            if (audioSource == null)
            {
                Debug.LogWarning("This PelletShooter is set to play audio, byt there is no AudioSOurce attached to it.");
            }
            else
            {
                if(audioSource.isPlaying)
                {
                    audioSource.Stop();
                }

                audioSource.Play();
            }
        }
		
	}
	public void RangeUp(float rangeToUp)
	{
		rangeInSec += rangeToUp;
	}
}
