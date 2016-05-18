using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject playerShipPrefab;
    public GameObject asteroidSpawnerObject;

	public Bar healthBar;
	public Bar shieldBar;
	public GameObject shieldBarObject;
	public GameObject healthBarObject;

    public new GameObject camera;
    public GameObject background;
    public uint asteroidsToPrespawn = 100;
    public Text scoreText;
	public Text healthText;
    public Text startGameText;
    public GameObject playerShipInstance;
    public uint spawnSafezoneRadius = 20;
	public int playerHealth = 4;
    public float playerCooldown = .6f;
    public uint maxPlayerHealth;

    bool gameStarted = false;

    PlayerFollowingCamera playerFollowingCam;
    Spawner asteroidSpawner;
    TiledBackground backgroundTiler;
    Jukebox jukebox;
    uint prespawnedAsteroids; //manually spawned asteroids since the game has started. Manual spawning stops when this value == asteroidsToPrespawn.
    public Vector2 levelSizePx;

    uint score = 0;


    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Multiple GameController instances in one scene!  There's only supposed to be one!");
        }

        instance = this;

    }

	void Start () {
        playerFollowingCam = camera.GetComponent<PlayerFollowingCamera>();
        asteroidSpawner = asteroidSpawnerObject.GetComponent<Spawner>();
        backgroundTiler = background.GetComponent<TiledBackground>();
        jukebox = GetComponent<Jukebox>();
        levelSizePx = backgroundTiler.textureSize * backgroundTiler.numTiles;

        shieldBar = shieldBarObject.GetComponent<Bar>();
        healthBar = healthBarObject.GetComponent<Bar>();
        ResetGame();

    }

    void Update ()
    {
	    if(!gameStarted && Input.anyKeyDown)
        {
            ResetGame();

        }

        if(prespawnedAsteroids < asteroidsToPrespawn)
        {
            asteroidSpawner.SpawnOne();
            ++prespawnedAsteroids;
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
        prespawnedAsteroids = 0;
        UpdateScore();
		playerHealth = (int)maxPlayerHealth;
		UpdateHealth ();

        foreach (GameObject objectToClean in GameObject.FindGameObjectsWithTag("Cruft to Clean Up"))
        {
            GameObject.Destroy(objectToClean);
        }



        Vector3 spawnLocation;
        //find a spawn location where there aren't any asteroids
        do
        {
            spawnLocation = RandomLocationInLevel();
            Debug.Log("Trying spawn location:" + spawnLocation);
        }
		while (Physics2D.OverlapCircle(spawnLocation, spawnSafezoneRadius) != null);


        playerShipInstance = GameObject.Instantiate(playerShipPrefab);
        playerShipInstance.transform.position = spawnLocation;
        playerFollowingCam.player = playerShipInstance;

        //play a new song
        jukebox.PlayNewSong();
    }

    public void AddScore(uint scoreToAdd)
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
	public void Damage(int damage)
	{
		playerHealth -=  damage;
		UpdateHealth ();
		if (playerHealth <= 0) {
			GameController.instance.OnPlayerKilled();
		}
	}
	public void UpdateHealth()
	{
		healthText.text = "Health: " + playerHealth;

        healthBar.UpdateBar(playerHealth, maxPlayerHealth);
	}

	public void HealPlayer(int amount)
	{
		playerHealth += amount;

		UpdateHealth();
	}
	public void PlayerCoolDownLower (float amountToLower)
	{
		playerCooldown = playerCooldown - amountToLower;
	}
}
