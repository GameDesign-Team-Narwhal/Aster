using UnityEngine;
using System.Collections;

public class PelletLifetime : MonoBehaviour
{
	public Color startingColor, endingColor;

	public float lifetimeSec;

	SpriteRenderer spriteRenderer;
	float startingTime;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
		spriteRenderer.color = startingColor;	
		startingTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		float timeSinceSpawn = Time.time - startingTime;

		spriteRenderer.color = Color.Lerp (startingColor, endingColor, timeSinceSpawn / lifetimeSec);

		if (timeSinceSpawn > lifetimeSec) {
			GameObject.Destroy(gameObject);
		}
	
	}
}
