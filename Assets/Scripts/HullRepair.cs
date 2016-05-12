using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public uint Health = 0;
	public uint MaxHealth = 1200;
	// Use this for initialization
	void Awake ()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			if(GameController.instance.playerHealth < MaxHealth-Health)
			{
				GameController.instance.HealPlayer(Health);
				GameObject.Destroy(gameObject);
			}
			else if(GameController.instance.playerHealth > MaxHealth-Health && GameController.instance.playerHealth < MaxHealth)
			{
				GameController.instance.playerHealth = MaxHealth;
				GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}
			else
			{

			}
		}
	}
}
