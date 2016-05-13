using UnityEngine;
using System.Collections;

public class RangeUp : MonoBehaviour {
	public float range = 0.1f;
	PelletShooter Shooter;
	Rigidbody2D body2d;
	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
		Shooter = GameController.instance.playerShipInstance.GetComponent<PelletShooter> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			Shooter.RangeUp(range);
			GameObject.Destroy(gameObject);
		}
	}
}
