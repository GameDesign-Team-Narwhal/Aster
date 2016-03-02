using UnityEngine;
using System.Collections;
using System;

public class AsteroidBreakup : MonoBehaviour, IShootable
{
    Animator breakupAnim;

    bool alreadyDead;

    public void OnShotBy(GameObject shooter)
    {
        if (!alreadyDead)
        {
            StartCoroutine(BreakupCoroutine());

            alreadyDead = true;
        }
    }

    void Awake()
    {
        breakupAnim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update () {
	}
    

    IEnumerator BreakupCoroutine()
    {
        breakupAnim.SetTrigger("Breakup");
        yield return null;

        Debug.Log("Anim length:" + breakupAnim.GetCurrentAnimatorClipInfo(0).Length / 2.0);

        yield return new WaitForSeconds((breakupAnim.GetCurrentAnimatorClipInfo(0).Length) / 2f);


        GameObject.Destroy(gameObject);
    }
}
