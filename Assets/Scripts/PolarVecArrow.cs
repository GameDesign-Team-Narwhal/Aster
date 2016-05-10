using UnityEngine;
using System.Collections;

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
