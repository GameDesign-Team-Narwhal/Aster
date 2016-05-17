using UnityEngine;
using System.Collections;
using System;

public class FireRateUp : Upgrade {
	public float cooldownReduction = 0.1f;

    protected override void BuffPlayer(GameObject player)
    {
        GameController.instance.PlayerCoolDownLower(cooldownReduction);
    }

    protected override string GetMessage()
    {
        return "Fire Rate Up!";
    }
}
