using UnityEngine;
using System.Collections;
using System;

public class WeaponUp : Upgrade {
	public uint damageIncrease = 1;

    protected override void BuffPlayer(GameObject player)
    {
        PelletShooter PS = player.GetComponent<PelletShooter>();
        PS.pelletDamage += damageIncrease;

    }

    protected override string GetMessage()
    {
        return "Damage Up!";
    }
}
