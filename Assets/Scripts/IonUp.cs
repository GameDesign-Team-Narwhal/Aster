using UnityEngine;
using System.Collections;

public class IonUp : Upgrade {
	public float IonDeration = 0.5f;
	// Use this for initialization
	protected override void BuffPlayer(GameObject player)
	{
		PelletShooter PS = GameController.instance.playerShipInstance.GetComponent<PelletShooter> ();
		GameObject Pelet = PS.pelletPrefab;
		Projectile Pro = Pelet.GetComponent<Projectile> ();
		Pro.ion += IonDeration;
	}
	
	protected override string GetMessage()
	{
		return "Ion Up!";
	}
}
