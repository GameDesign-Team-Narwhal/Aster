using UnityEngine;
using System.Collections;

public class Regen : Upgrade {
	public uint amountToIncress = 0;
	
	// Use this for initialization
	protected override void BuffPlayer(GameObject player)
	{
		GameController.instance.Regen += amountToIncress;
	
	}
	
	protected override string GetMessage()
	{
		return "Regen Up!";
	}
}
