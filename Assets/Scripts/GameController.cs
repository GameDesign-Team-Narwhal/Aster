using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Vector2 levelSizeBackgrounds = new Vector2(0, 0);

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
        foreach(GameObject objectToClean in GameObject.FindGameObjectsWithTag("Cruft to Clean Up"))
        {
            GameObject.Destroy(objectToClean);
        }

        GameObject.Instantiate
    }
}
