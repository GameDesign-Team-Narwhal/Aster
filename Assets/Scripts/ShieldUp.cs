using UnityEngine;
using System.Collections;

public class ShieldUp : Upgrade {
	public uint amountToIncress = 0;

	// Use this for initialization
	protected override void BuffPlayer(GameObject player)
	{
		PlayerControlledShip PCS = GameController.instance.playerShipInstance.GetComponent<PlayerControlledShip> ();
		PCS.maxShields += amountToIncress;
	}
	
	protected override string GetMessage()
	{
		return "Sheild Up!";
	}
}
