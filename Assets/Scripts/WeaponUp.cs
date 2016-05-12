using UnityEngine;
using System.Collections;

public class WeaponUp : MonoBehaviour {
	public int DamgeUp = 1;
	Rigidbody2D body2d;
	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			GameController.instance.PlayerDamgeUp(DamgeUp);
			GameObject.Destroy(gameObject);
		}
	}
}
