using UnityEngine;
using System.Collections;

//Base class for upgrade objects
public abstract class Upgrade : MonoBehaviour {

    public GameObject upgradeText;

    protected abstract void BuffPlayer(GameObject player);
    protected abstract string GetMessage(); //get the string that should be displayed when the player picks up the upgrade

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.Equals(GameController.instance.playerShipInstance))
        {
            BuffPlayer(other.gameObject);

            GameObject instantiatedText = Instantiate(upgradeText);
            instantiatedText.GetComponent<UpgradeText>().text = GetMessage();
            instantiatedText.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
