using UnityEngine;
using System.Collections;

public class ShotUp : Upgrade
{
    public uint amountToIncress = 1;

    // Use this for initialization
    protected override void BuffPlayer(GameObject player)
    {
        PelletShooter PS = GameController.instance.playerShipInstance.GetComponent<PelletShooter>();
    }

    protected override string GetMessage()
    {
        return "Sheild Up!";
    }
}
