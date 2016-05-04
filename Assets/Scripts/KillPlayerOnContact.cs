using UnityEngine;
using System.Collections;

public class KillPlayerOnContact : MonoBehaviour
{
	public int Damage = 4;
	private PlayerControlledShip PlayerControlledShip;
	void Awake ()
	{
		PlayerControlledShip = GetComponent<PlayerControlledShip>();
	}
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.Equals(GameController.instance.playerShipInstance))
        {
			GameController.instance.Damage(Damage);
          //  GameController.instance.OnPlayerKilled();
        }
    }
}
