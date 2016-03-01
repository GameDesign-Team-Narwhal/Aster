using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Projectile : MonoBehaviour {

    public GameObject shooter;

	void Start () {
	  
	}
	
	void OnTriggerEnter2D(Collider2D other)
    {
        //make sure they're not hitting themselves
        if(!other.gameObject.Equals(shooter))
        {
            List<IShootable> shootableBehaviors = Utils.GetBehaviorsWithInterface<IShootable>(other.gameObject);

            if(shootableBehaviors.Count > 0)
            {
                foreach(IShootable shootable in shootableBehaviors)
                {
                    shootable.OnShotBy(shooter);
                }
            }
        }
    }
}
