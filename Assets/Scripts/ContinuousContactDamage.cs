using UnityEngine;
using System.Collections;

public class ContinuousContactDamage : ObjectCounter
{
    public int damage = 1;
	
	// Update is called once per frame
	void Update () {
	    if(objectsInTrigger.Contains(GameController.instance.playerShipInstance))
        {
            GameController.instance.Damage(damage);
        }
	}
}
