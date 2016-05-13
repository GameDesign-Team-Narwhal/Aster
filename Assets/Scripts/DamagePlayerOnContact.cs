using UnityEngine;
using System.Collections;

public class DamagePlayerOnContact : MonoBehaviour
{
	public int damage = 4;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.Equals(GameController.instance.playerShipInstance))
        {
			GameController.instance.Damage(damage);
        }
    }
}
