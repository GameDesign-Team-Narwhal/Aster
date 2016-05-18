using UnityEngine;
using System.Collections;
using System;

public class RangeUp : Upgrade
{
    public float rangeBonus = .1f;

    protected override void BuffPlayer(GameObject player)
    {
        player.GetComponent<PelletShooter>().RangeUp(rangeBonus);
    }

    protected override string GetMessage()
    {
        return "Range Up!";
    }
}
