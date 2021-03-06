using UnityEngine;
using System.Collections;
using System;

public class AsteroidBreakup : MonoBehaviour, IShootable
{

    //stops smaller asteroids from getting insta-destroyed by the other laser in the pair.
    public float spawnInvulnTime = 1f;

	public uint score = 1;
	public string team = "Asteroids";

    Animator breakupAnim;

    protected bool alreadyDead;

    //absolute time when the asteroid was created
    float spawnTime;

    public GameObject[] smallerAsteroids;
	public GameObject[] upgrades;
    public Vector2[] asteroidMinMaxCounts;
	public int[] upgradeProb;

    public void OnShotBy(GameObject shooter, string shooterTeam, int damage, float ion)
    {

        if (!(alreadyDead || (Time.time - spawnTime) < spawnInvulnTime))
        {
            StartCoroutine(BreakupCoroutine());

            alreadyDead = true;
        }
        if (shooterTeam.Equals("Player"))
		{
	        GameController.instance.AddScore(score);
		}
    }

    void Awake()
    {
        breakupAnim = GetComponent<Animator>();

        if (asteroidMinMaxCounts.Length != smallerAsteroids.Length)
        {
            Debug.LogError("Mismatch between number of smaller asteroids and spawn frequencies");
        }

        spawnTime = Time.time;
        
    }

    IEnumerator BreakupCoroutine()
    {
        breakupAnim.SetTrigger("Breakup");
        yield return null;

        for(uint counter = 0; counter < smallerAsteroids.Length; ++counter)
        {
            uint numToSpawn = (uint)UnityEngine.Random.Range(asteroidMinMaxCounts[counter].x, asteroidMinMaxCounts[counter].y);
            for(int spawnCounter = 0; spawnCounter < numToSpawn; ++spawnCounter)
            {
                GameObject newAsteroid = GameObject.Instantiate(smallerAsteroids[counter]);
                newAsteroid.transform.position = transform.position;


				AstroidMovement asterMovement = newAsteroid.GetComponent<AstroidMovement>();

				if(asterMovement != null)
				{
					asterMovement.fixedLocation = true;
				}

            }
        }
		for(uint counter = 0; counter < upgrades.Length; ++counter)
		{
			int prob = upgradeProb[counter];
			int toSpawn = (int)UnityEngine.Random.Range(1, 100);
			if(prob >= toSpawn)
			{
				GameObject newUpgrade = GameObject.Instantiate(upgrades[counter]);
				newUpgrade.transform.position = transform.position;
			}
		}

        //make the asteroid intangible and stop bullets and players from colliding with it while it is breaking up
        foreach (Collider2D collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }


        yield return StartCoroutine(Utils.WaitForAnimation(breakupAnim));

        GameObject.Destroy(gameObject);
    }

	public string GetTeam()
	{
		return team;
	}
}
