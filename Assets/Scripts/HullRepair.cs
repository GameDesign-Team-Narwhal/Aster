using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public int Health = 0;
	public int MaxHealth = 1200;
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
			if(GameController.instance.playerHealth < MaxHealth-Health){
			GameController.instance.Damage(-Health);
			GameObject.Destroy(gameObject);
			}else if(GameController.instance.playerHealth > MaxHealth-Health && GameController.instance.playerHealth < MaxHealth){
				GameController.instance.playerHealth = MaxHealth;
				GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}else
			{

			}
		}
	}
}
