using UnityEngine;
using System.Collections;
using System;

public class AsteroidBreakup : MonoBehaviour, IShootable
{

    //stops smaller asteroids from getting insta-destroyed by the other laser in the pair.
    public float spawnInvulnTime = 1f;
    Animator breakupAnim;

    protected bool alreadyDead;

    //absolute time when the asteroid was created
    float spawnTime;

    public GameObject[] smallerAsteroids;
    public Vector2[] asteroidMinMaxCounts;

    Collider2D physicsCollider;

    public void OnShotBy(GameObject shooter)
    {
        if (!(alreadyDead || (Time.time - spawnTime) < spawnInvulnTime))
        {
            StartCoroutine(BreakupCoroutine());

            alreadyDead = true;
        }
    }

    void Awake()
    {
        breakupAnim = GetComponent<Animator>();
        foreach(Collider2D collider in GetComponents<Collider2D>())
        {
            //find the non-trigger collider
            if(!collider.isTrigger)
            {
                physicsCollider = collider;
            }
        }

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
                newAsteroid.GetComponent<AstroidMovement>().fixedLocation = true;

            }
        }

        //make the asteroid intangible
        physicsCollider.enabled = false;

        yield return StartCoroutine(Utils.WaitForAnimation(breakupAnim));

        GameObject.Destroy(gameObject);
    }
}
