using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour {

	public int textureSize = 32;
	public bool scaleHorizontially = true;
	public bool scaleVertically = true;

	public uint numTiles = 10;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (numTiles * textureSize, numTiles * textureSize, 1);

		GetComponent<Renderer> ().material.mainTextureScale = new Vector3 (numTiles, numTiles, 1);
	}

}
