using UnityEngine;
using System.Collections;
using System;

public class AsteroidBreakup : MonoBehaviour, IShootable
{
    public void OnShotBy(GameObject shooter)
    {
        GameObjectUtil.Destroy(gameObject);
    }


    // Update is called once per frame
    void Update () {
	
	}
}
