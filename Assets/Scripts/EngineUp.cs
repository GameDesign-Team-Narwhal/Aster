using UnityEngine;
using System.Collections;

public class EngineUp :  Upgrade
{
	public float dragUp = .1f;
	public int TorqueUp = 100;
	private Rigidbody2D body2d;
	
	protected override void BuffPlayer(GameObject player)
	{
		body2d = GetComponent<Rigidbody2D>();
		body2d.angularDrag += dragUp;
		player.GetComponent<PlayerControlledShip> ().turningTorque += TorqueUp;
	}
	
	protected override string GetMessage()
	{
		return "Handleding Up!";
	}
}
