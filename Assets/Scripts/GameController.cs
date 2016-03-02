using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Vector2 levelSizeBackgrounds = new Vector2(0, 0);

    public GameObject playerShipPrefab;
    public GameObject asteroidSpawner;
    public GameObject camera;

    public GameObject playerShipInstance;

    PlayerFollowingCamera playerFollowingCam;

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple GameController instances in one scene!  There's only supposed to be one!");
        }

        instance = this;

        playerFollowingCam = camera.GetComponent<PlayerFollowingCamera>();
    }

	void Start () {
        ResetGame();
	}
	
	void Update () {
	
	}

    void OnPlayerKilled()
    {

    }

    void ResetGame()
    {
        foreach(GameObject objectToClean in GameObject.FindGameObjectsWithTag("Cruft to Clean Up"))
        {
            GameObject.Destroy(objectToClean);
        }

        playerShipInstance = GameObject.Instantiate(playerShipPrefab);
        playerFollowingCam.player = playerShipInstance;
    }
}
