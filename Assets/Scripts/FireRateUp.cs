using UnityEngine;
using System.Collections;

public class FireRateUp : MonoBehaviour {
	public float CoolDownLower = 0.1f;
	// Use this for initialization
	void Awake ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{

			GameController.instance.PlayerCoolDownLower(CoolDownLower);
			GameObject.Destroy(gameObject);
		}
	}
}
