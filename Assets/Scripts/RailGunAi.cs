using UnityEngine;
using System.Collections;

public class RailGunAi : MonoBehaviour, IShootable {
	public int Health = 300;
	Animator animator;
	public string team;
	public GameObject Parent;
	public bool IsLeft;
	void Start () {
		animator = Parent.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	
	
	public void OnShotBy(GameObject shooter, string team, int damage, float ion)
	{
		Health -= damage;

		if (Health <= 0) {
			if(IsLeft)
			{
				animator.SetBool("LeftDown", true);

			}else
			{
				animator.SetBool("RightDown", true);
			}
		}
	}
	public string GetTeam()
	{
		return team;
	}
}
