using UnityEngine;
using System.Collections;

public class SSLaserCannon : MonoBehaviour {

    public GameObject laserBeamPrefab;
    public float range; //maximum distance that the cannon can hit
    public Vector2 shotStartPoint = new Vector2(15, 0);
    public float maxFiringAngle = 60;

    private GameObject laserBeamObject;
    private PolarVecArrow laserBeamScript;
    public bool laserBeamActive = false, laserBeamInstantiated = false;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        PolarVec2 playerPos = PolarVec2.FromCartesian(GameController.instance.playerShipInstance.transform.position - transform.position);

        //Debug.Log("Player pos: " + playerPos.ToString());


        //check that the player is in front of the cannon
        if (playerPos.r <= range && Mathf.Abs((playerPos.ToOiler(Axis.Z) - transform.rotation.eulerAngles).z) <= maxFiringAngle)
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
                animator.SetBool("Firing", true);
                laserBeamActive = true;
            }

            laserBeamScript.SetVectorToDisplay(new PolarVec2(playerPos.A, 3000), transform.position + (Vector3)shotStartPoint);
        }
        else
        {
            if(laserBeamActive)
            {
                //hide laser beam
                laserBeamActive = false;
                laserBeamObject.SetActive(false);
                animator.SetBool("Firing", false);
            }
        }
	}
}
