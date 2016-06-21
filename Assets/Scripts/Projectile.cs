using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Damage/Projectile")]
public class Projectile : MonoBehaviour {

    public GameObject shooter;

	public string team = "";
	public float ion = 0;
	public int damage = 2;
    public int penitration = 0;
    //true if the projectile has an explosion animation with a call to OnDestructionAnimationComplete() at the end.
    public bool hasDestructionAnimation = false;

    //name of the trigger for the destruction animation
    //only used if hasDestructionAnimation is true
    public string destructionTriggerName = "Destroy";

    //if true, the projectile can pass through objects without being destroyed
    bool penetrating = false;
    Animator animator;

	public void Awake()
    {
        animator = GetComponent<Animator>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        //make sure they're not hitting themselves
        if (!other.gameObject.Equals(shooter))
        {
            IShootable shootableBehavior = Utils.GetBehaviorWithInterface<IShootable>(other.gameObject);

            if (shootableBehavior != null && CanDamage(shootableBehavior.GetTeam()))
            {

                shootableBehavior.OnShotBy(shooter, team, damage, ion);

 
                
                    if(hasDestructionAnimation)
                    {
                        if(animator == null)
                        {
                            Debug.LogWarning("Destruction animation set to play on projectile without animator");
                        }
                        else
                        {
                            animator.SetTrigger(destructionTriggerName);
                        }
                    }
                    else
                    {
                        if (penitration >= 0)
                        {
                            penitration--;
                        }else
                        {
                            GameObject.Destroy(gameObject);
                        }
                    }
                
            }
            else
            {
                //Debug.Log("Projectile hit a non-shootable object, like a teammate or a rock");
                
                    if (penitration >= 0)
                    {
                        penitration--;
                    }
                    else
                    {
                        GameObject.Destroy(gameObject);
                    }

                

            }
        }
    }

	//returns true if this projectile can damage something on the provided team
	public bool CanDamage(string otherTeam)
	{
		if(string.IsNullOrEmpty(otherTeam))
		{
			return true;
		}

		return !otherTeam.Equals(team);
	}

    public void OnDestructionAnimationComplete()
    {
        GameObject.Destroy(gameObject);
    }
}
