using System;
using UnityEngine;

/**
 * Class to represnt a 2D polar vector.
 * 
 * Angle is in DEGREES!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
 * */
public struct PolarVec2
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

    public static PolarVec2 operator+(PolarVec2 a, PolarVec2 b)
    {

        PolarVec2 result = new PolarVec2();

        result.A = 180 - a.A + b.A;
        
        //law of cosines
        result.r = Mathf.Sqrt(Mathf.Pow(a.r, 2) + Mathf.Pow(b.r, 2) - 2 * a.r * b.r * Mathf.Cos(result.Theta));

        return result;
    }

    public static PolarVec2 operator-(PolarVec2 a, PolarVec2 b)
    {
        if(b.r == 0)
        {
            return a;
        }

        PolarVec2 result = new PolarVec2();

        //law of sines
        result.A = 180 - Mathf.Rad2Deg * Mathf.Asin((a.r / b.r) * Mathf.Sin(a.Theta - b.Theta)) + b.A;

        //law of cosines
        result.r = Mathf.Sqrt(Mathf.Pow(a.r, 2) + Mathf.Pow(b.r, 2) - 2 * a.r * b.r * Mathf.Cos(result.Theta));

        return result;
    }

    // Multiply radius of vector by a scalar.
    public static PolarVec2 operator*(PolarVec2 vec, float number)
    {
        vec.r *= number;
        return vec;
    }

    // Divide radius of vector by a scalar.
    public static PolarVec2 operator/(PolarVec2 vec, float number)
    {
        vec.r /= number;
        return vec;
    }

    /*
     Convert the rotation to a euler (pronounced oiler) angle of rotation around the supplied axis.
    */
    public Vector3 ToOiler(Axis rotationAxis)
    {
        switch(rotationAxis)
        {
            case Axis.X:
                return new Vector3(A - 90, 0, 0);
            case Axis.Y:
                return new Vector3(0, A - 90, 0);
            default:
                return new Vector3(0, 0, A - 90);
        } 
    }
}


