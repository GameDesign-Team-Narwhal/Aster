using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Projectile : MonoBehaviour {

    public GameObject shooter;

	public string team = "";

	public uint damage = 2;

    //if true, the projectile can pass through objects without being destroyed
    bool penetrating = false;


	
	void OnTriggerEnter2D(Collider2D other)
    {
        //make sure they're not hitting themselves
        if(!other.gameObject.Equals(shooter))
        {
            IShootable shootableBehavior = Utils.GetBehaviorWithInterface<IShootable>(other.gameObject);

			if(shootableBehavior != null && CanDamage(shootableBehavior.GetTeam()))
			{
	
				shootableBehavior.OnShotBy(shooter, team, damage);
				
				if(!penetrating)
				{
					GameObject.Destroy(gameObject);
				}
			}
			else if(!penetrating) //hit a non-shootable object, like a rock.  destroy.
			{
				GameObject.Destroy(gameObject);
	        }
		}
    }

	//trturn tru if this projectile can damage
	public bool CanDamage(string otherTeam)
	{
		if(string.IsNullOrEmpty(otherTeam))
		{
			return true;
		}

		return !otherTeam.Equals(team);
	}
}
