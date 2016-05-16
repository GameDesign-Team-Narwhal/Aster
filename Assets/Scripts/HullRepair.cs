using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public uint MaxHealth = 1200;
	// Use this for initialization
	void Awake ()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			if(GameController.instance.playerHealth + MaxHealth > GameController.instance.maxPlayerHealth)
			{
				GameController.instance.HealPlayer(MaxHealth);
                GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}
			else
			{
				GameController.instance.maxPlayerHealth = MaxHealth;
                GameController.instance.playerHealth = (int)MaxHealth;

                GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}
	
		}
	}
}
