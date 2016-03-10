using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Utils
{
    public static Vector2 VecFromAngleMagnitude(float angle, float magnitude)
    {
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * angle) * magnitude, Mathf.Sin(Mathf.Deg2Rad * angle) * magnitude);
    }

    /*
     From http://answers.unity3d.com/questions/523409/strategy-pattern-with-monobehaviours.html
    */
    public static List<T> GetBehaviorsWithInterface<T>(GameObject objectToSearch) where T : class
    {
        MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
        List<T> resultList = new List<T>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is T)
            {
                //found one
                resultList.Add((T)((System.Object)mb));
            }
        }

        return resultList;
    }

    /*
        Wait for the current animation clip to f
    */
    public static IEnumerator WaitForAnimation(Animator animator)
    {
		var state = animator.GetCurrentAnimatorStateInfo (0);
        yield return new WaitForSeconds(state.length - state.normalizedTime - .1f);
    }
}

