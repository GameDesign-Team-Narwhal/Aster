using UnityEngine;
using System.Collections;

public class HullRepair : MonoBehaviour {
	public int Health = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if(other.Equals(GameController.instance.playerShipInstance))
		   {
			GameController.instance.Damage(-Health);
			GameObject.Destroy(gameObject);
		}
	}
}
