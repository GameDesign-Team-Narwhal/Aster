using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



interface IShootable
{
	//called whan a projectile from a different team hits the entity
    void OnShotBy(GameObject shooter, string shooterTeam, int damage);

	string GetTeam();
}
