using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Vector2 levelSizeBackgrounds = new Vector2(0, 0);

    public GameObject playerShipPrefab;
    public GameObject asteroidSpawnerObject;
    public new GameObject camera;
    public GameObject background;
    public uint asteroidsToPrespawn = 100;
    public Text scoreText;
    public Text startGameText;
    public GameObject playerShipInstance;

    bool gameStarted = false;
    PlayerFollowingCamera playerFollowingCam;
    Spawner asteroidSpawner;
    int score = 0;


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
        startGameText.text = "Press any key to start";
        startGameText.enabled = true;

    }

    void Update ()
    {
	    if(!gameStarted && Input.anyKeyDown)
        {
            ResetGame();

        }
    }

    public void OnPlayerKilled()
    {
        StartCoroutine(explodePlayerShip());

        startGameText.text = "Press any key to restart";
        startGameText.enabled = true;

    }

    IEnumerator explodePlayerShip()
    {
        Animator shipAnimator = playerShipInstance.GetComponent<Animator>();
        shipAnimator.SetTrigger("Explode");
        yield return null;
        yield return StartCoroutine(Utils.WaitForAnimation(shipAnimator));
        GameObject.Destroy(playerShipInstance);
        gameStarted = false;
    }

    void ResetGame()
    {

        startGameText.enabled = false;
        gameStarted = true;

        score = 0;
        UpdateScore();

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

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
