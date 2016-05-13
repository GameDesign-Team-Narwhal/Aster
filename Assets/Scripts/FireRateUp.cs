using UnityEngine;
using System.Collections;

public class FireRateUp : MonoBehaviour {
	public float CoolDownLower = 0.1f;
	// Use this for initialization
	void Awake ()
	{
		Debug.Log("Fire Rate up");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Fire Rate up");
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Fire Rate up");
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			Debug.Log("Fire Rate up");
			GameController.instance.PlayerCoolDownLower(CoolDownLower);
			GameObject.Destroy(gameObject);
		}
	}
}
