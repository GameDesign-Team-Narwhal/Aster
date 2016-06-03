using UnityEngine;
using System.Collections;

/**
    Script for a 2D arrow which represents a polar vector.

    The pivot of the texture must be on the LEFT for this to work.
*/
public class PolarVecArrow : MonoBehaviour 
{
	public float textureLength = 128;

	public void SetVectorToDisplay(PolarVec2 vector, Vector3 center)
	{
		transform.localScale = new Vector2(vector.r / textureLength, 1);
		transform.eulerAngles = vector.ToOiler(Axis.Z);
		transform.position = center;
	}
}
