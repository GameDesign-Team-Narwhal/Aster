using UnityEngine;
using System.Collections;

public class WeaponUp : MonoBehaviour {
	public uint damageIncrease = 1;
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
		//	GameObject Player = GameObject.Instantiate(other);
			PelletShooter PS = other.GetComponent<PelletShooter>();
			PS.pelletDamage += damageIncrease;
			GameObject.Destroy(gameObject);
		}
	}
}
