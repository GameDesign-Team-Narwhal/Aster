using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public int Health = 0;
	Rigidbody2D body2d;
	// Use this for initialization
	void Awake ()
	{
		body2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
			if(GameController.instance.playerHealth < 12-Health){
			GameController.instance.Damage(-Health);
			GameObject.Destroy(gameObject);
			}else if(GameController.instance.playerHealth > 12-Health && GameController.instance.playerHealth < 12){
				GameController.instance.playerHealth = 12;
				GameController.instance.UpdateHealth();
				GameObject.Destroy(gameObject);
			}else
			{

			}
		}
	}
}
