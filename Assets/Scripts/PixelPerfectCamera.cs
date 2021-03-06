using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera/Pixel Perfect Camera")]
public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsToUnits = 1f;
	public static float scale = 1f;

	public Vector2 nativeResolution = new Vector2 (240, 160);

	void Awake () {
		var camera = GetComponent<Camera> ();

        //these may have been set by a previous scene, so we have to reset them
        pixelsToUnits = 1f;
        scale = 1f;

		if (camera.orthographic) {
			scale = Screen.height/nativeResolution.y;
			pixelsToUnits *= scale;
			camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnits;

            Debug.Log("pixelsToUnits = " + pixelsToUnits + " scale = " + scale);
		}
	}

}
