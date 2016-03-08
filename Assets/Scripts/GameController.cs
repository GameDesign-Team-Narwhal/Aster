using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;


    public GameObject playerShipPrefab;
    public GameObject asteroidSpawnerObject;
    public new GameObject camera;
    public GameObject background;
    public uint asteroidsToPrespawn = 100;
    public Text scoreText;
    public Text startGameText;
    public GameObject playerShipInstance;
    public uint spawnSafezoneRadius = 20;

    bool gameStarted = false;
    PlayerFollowingCamera playerFollowingCam;
    Spawner asteroidSpawner;
    TiledBackground backgroundTiler;
    public Vector2 levelSizePx;

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
        backgroundTiler = background.GetComponent<TiledBackground>();
        levelSizePx = backgroundTiler.textureSize * backgroundTiler.numTiles;
    }

	void Start () {
        startGameText.text = "Press a key to start";
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

        startGameText.text = "Press a key to restart";
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

        Vector3 spawnLocation;
        //find a spawn location where there aren't any asteroids
        do
        {
            spawnLocation = RandomLocationInLevel();
            Debug.Log("Trying spawn location:" + spawnLocation);
        }
        while (Physics2D.OverlapArea(new Vector2(spawnLocation.x - spawnSafezoneRadius, spawnLocation.y - spawnSafezoneRadius),
        new Vector2(spawnLocation.x + spawnSafezoneRadius, spawnLocation.y + spawnSafezoneRadius)) != null);

        playerShipInstance = GameObject.Instantiate(playerShipPrefab);
        playerShipInstance.transform.position = spawnLocation;
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

    public Vector3 RandomLocationInLevel()
    {
        Vector3 location = new Vector3();
        location.x = Random.Range(-levelSizePx.x / 2, levelSizePx.x / 2);
        location.y = Random.Range(-levelSizePx.y / 2, levelSizePx.y / 2);
        return location;
    }
}
