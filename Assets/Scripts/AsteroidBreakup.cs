using UnityEngine;
using System.Collections;
using System;

public class AsteroidBreakup : MonoBehaviour, IShootable
{
    Animator breakupAnim;

    bool alreadyDead;

    public GameObject[] smallerAsteroids;
    public Vector2[] asteroidMinMaxCounts;

    public void OnShotBy(GameObject shooter)
    {
        if (!alreadyDead)
        {
            StartCoroutine(BreakupCoroutine());

            alreadyDead = true;
        }
    }

    void Awake()
    {
        breakupAnim = GetComponent<Animator>();

        if(asteroidMinMaxCounts.Length != smallerAsteroids.Length)
        {
            Debug.LogError("Mismatch between number and count of smaller asteroids");
        }
    }


    // Update is called once per frame
    void Update () {
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

        yield return new WaitForSeconds((breakupAnim.GetCurrentAnimatorClipInfo(0).Length) / 2f);


        GameObject.Destroy(gameObject);
    }
}
