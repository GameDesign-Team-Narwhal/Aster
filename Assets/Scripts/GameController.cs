using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Vector2 levelSizeBackgrounds = new Vector2(0, 0);

    public GameObject playerShipPrefab;
    public GameObject asteroidSpawnerObject;
    public GameObject camera;
    public uint asteroidsToPrespawn = 100;

    public GameObject playerShipInstance;


    PlayerFollowingCamera playerFollowingCam;
    Spawner asteroidSpawner;


    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple GameController instances in one scene!  There's only supposed to be one!");
        }

        instance = this;

        playerFollowingCam = camera.GetComponent<PlayerFollowingCamera>();
        asteroidSpawner = asteroidSpawnerObject.GetComponent<Spawner>();
    }

	void Start () {
        ResetGame();
	}
	
	void Update () {
	
	}

    public void OnPlayerKilled()
    {

    }

    void ResetGame()
    {


        foreach (GameObject objectToClean in GameObject.FindGameObjectsWithTag("Cruft to Clean Up"))
        {
            GameObject.Destroy(objectToClean);
        }

        for(uint counter = 0; counter < asteroidsToPrespawn; ++counter)
        {
            asteroidSpawner.SpawnOne();
        }

        playerShipInstance = GameObject.Instantiate(playerShipPrefab);
        playerFollowingCam.player = playerShipInstance;
    }
}
