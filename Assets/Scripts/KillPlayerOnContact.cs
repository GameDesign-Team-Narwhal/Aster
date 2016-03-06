using UnityEngine;
using System.Collections;

public class KillPlayerOnContact : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.Equals(GameController.instance.playerShipInstance))
        {
            GameController.instance.OnPlayerKilled();
        }
    }
}
