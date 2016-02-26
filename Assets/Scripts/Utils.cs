using System;
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
}

