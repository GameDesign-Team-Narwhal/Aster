using System;
using UnityEngine;

/**
 * Class to represnt a 2D polar vector.
 * 
 * Angle is in DEGREES!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 * */
public class PolarVec2
{
	public float A, r;

	//angle in radians
	public float Theta
	{
		get
		{
			return A * Mathf.Deg2Rad;
		}

		set
		{
			A = Mathf.Rad2Deg * value;
		}
	}

	//2d Cartesian vector
	public Vector2 Cartesian2D
	{
		get
		{
			return new Vector2(Mathf.Cos(Theta) * r, Mathf.Sin(Theta) * r);
		}
	}

	//3d vector with the y axis as the axis of rotation
	// (the vector will be on the XZ plane)
	public Vector3 Cartesian3DHorizontal
	{
		get
		{
			return new Vector3(Mathf.Cos(Theta) * r, 0, Mathf.Sin(Theta) * r);
		}
	}

	public PolarVec2 ()
	{
		A = 0;
		r = 0;
	}

	public PolarVec2 (float A, float r)
	{
		this.A = A;
		this.r = r;
	}


	public static PolarVec2 FromCartesian(float x, float y)
	{
		PolarVec2 vector = new PolarVec2();

		vector.r = Mathf.Sqrt(x*x + y*y);
		vector.Theta = Mathf.Atan2(y, x);

		return vector;
	}

	public static PolarVec2 FromCartesian(Vector2 cartesian)
	{
		return FromCartesian(cartesian.x, cartesian.y);
	}

    public override string ToString()
    {
        return "PolarVec2: A=" + A + "deg, r=" + r;
    }
}


