using UnityEngine;
using System.Collections;

public class SSLaserCannon : MonoBehaviour {

    public GameObject laserBeamPrefab;
    public float range; //maximum distance that the cannon can hit
    public Vector2 shotStartPoint = new Vector2(0, 10);

    private GameObject laserBeamObject;
    private PolarVecArrow laserBeamScript;
    bool laserBeamActive = false, laserBeamInstantiated = false;

	// Update is called once per frame
	void Update () {
        PolarVec2 playerPos = PolarVec2.FromCartesian(GameController.instance.playerShipInstance.transform.position - transform.position);

        Debug.Log("Player pos: " + playerPos.ToString());

        if(playerPos.r <= range)
        {
            //check that the player is in front of the cannon
            if(Mathf.Abs((playerPos.ToOiler(Axis.Z) - transform.rotation.eulerAngles).z) <= 90)
            {
                //FIRE!!!!
                if(!laserBeamInstantiated)
                {
                    laserBeamObject = GameObject.Instantiate(laserBeamPrefab);
                    laserBeamScript = laserBeamObject.GetComponent<PolarVecArrow>();
                    laserBeamInstantiated = true;
                    laserBeamActive = true;
                }

                if (!laserBeamActive)
                {
                    laserBeamObject.SetActive(true);
                }

                laserBeamScript.SetVectorToDisplay(new PolarVec2(playerPos.A, range), transform.position + (Vector3)shotStartPoint);
                
            }
        }
        else
        {
            if(laserBeamActive)
            {
                //hide laser beam
                laserBeamActive = false;
                laserBeamObject.SetActive(false);
            }
        }
	}
}
