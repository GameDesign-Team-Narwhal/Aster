using UnityEngine;
using System.Collections;

public class WeaponUp : MonoBehaviour {
	public uint damageIncrease = 1;
    public GameObject upgradeText;

	// Use this for initialization
	void Awake ()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.Equals(GameController.instance.playerShipInstance))
		{
		//	GameObject Player = GameObject.Instantiate(other);
			PelletShooter PS = other.GetComponent<PelletShooter>();
			PS.pelletDamage += damageIncrease;

            GameObject instantiatedText = Instantiate(upgradeText);
            instantiatedText.GetComponent<UpgradeText>().text = "Damage Up!";
            instantiatedText.transform.position = transform.position;
			Destroy(gameObject);
		}
	}
}
