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

    /*
    Get the angle of a vector from horizontal
    */
    public static float VecAngle(Vector2 vector)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
    }

	/**
	 * From http://stackoverflow.com/questions/2353211/hsl-to-rgb-color-conversion
	 * 
	 * Converts an HSL color value to RGB. Conversion formula
	 * adapted from http://en.wikipedia.org/wiki/HSL_color_space.
	 * Assumes h, s, and l are contained in the set [0, 1] and
	 * returns r, g, and b in the set [0, 255].
	 *
	 * @param h       The hue
	 * @param s       The saturation
	 * @param l       The lightness
	 * @return Color  The RGB representation
	 */
	public static Color ColorFromHSL(float h, float s, float l){
		float r = 0, g = 0, b = 0;
		
		if(s == 0){
			r = g = b = l; // achromatic
		}else{

			
			float q = l < 0.5 ? l * (1 + s) : l + s - l * s;
			float p = 2 * l - q;
			r = hue2rgb(p, q, h + 1/3f);
			g = hue2rgb(p, q, h);
			b = hue2rgb(p, q, h - 1/3f);
		}
		
		return new Color(r, g, b);
	}

	private static float hue2rgb(float p, float q, float t){
		if(t < 0) t += 1;
		if(t > 1) t -= 1;
		if(t < 1/6f) return p + (q - p) * 6 * t;
		if(t < 1/2f) return q;
		if(t < 2/3f) return p + (q - p) * (2/3f - t) * 6;
		return p;
	}
}

