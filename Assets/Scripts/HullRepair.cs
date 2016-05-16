using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public int health = 300;
	// Use this for initialization
	void Awake ()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			if(GameController.instance.playerHealth + health < GameController.instance.maxPlayerHealth)
			{
				GameController.instance.HealPlayer(health);
                GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}
			else if(GameController.instance.playerHealth + health > GameController.instance.maxPlayerHealth)
			{
				GameController.instance.playerHealth = (int)GameController.instance.maxPlayerHealth;
                GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}else
			{

			}
	
		}
	}
}
