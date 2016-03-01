using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject playerShipPrefab;
    public GameObject asteroidSpawner;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple GameController instances in one scene!  There's only supposed to be one!");
        }

        instance = this;


    }

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnPlayerKilled()
    {

    }

    void ResetGame()
    {
        foreach(GameObject objectToClean in GameObject.FindGameObjectsWithTag("Projectiles"))
        {
            GameObject.Destroy(objectToClean);
        }
    }
}
