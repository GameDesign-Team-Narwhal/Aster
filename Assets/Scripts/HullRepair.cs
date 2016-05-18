using UnityEngine;
using System.Collections;
using System;

public class HullRepair : Upgrade {
	public int health = 300;

    protected override void BuffPlayer(GameObject player)
    {
        GameController.instance.playerHealth = (int)Mathf.Clamp(GameController.instance.playerHealth + health, 0, GameController.instance.maxPlayerHealth);
        GameController.instance.UpdateHealth();     
    }

    protected override string GetMessage()
    {
        return "Hull Repaired";
    }
}
