﻿using UnityEngine;
using System.Collections;

public class ColorOscillator : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public float saturation = 1, lightness = .5f, alpha = 1;
	public float currentH = 0;

	public float huePerSecond = 1;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		currentH += huePerSecond * Time.deltaTime;

		if(currentH > 1)
		{
			currentH += huePerSecond * Time.deltaTime;
			
			if(automatic && currentH > 1)
			{
				currentH -= 1;
			}

			if(!automatic && currentH > 1)
			{
				//done oscillating
				spriteRenderer.color = Color.white;
			}
			else
			{
				Color newColor = Utils.ColorFromHSL(currentH, saturation, lightness);
				newColor.a = alpha;
				//Debug.Log("New color:" + newColor.ToString());
				
				spriteRenderer.color = newColor;
			}
		}
		Color newColor = Utils.ColorFromHSL(currentH, saturation, lightness);
		newColor.a = alpha;
		//Debug.Log("New color:" + newColor.ToString());

		spriteRenderer.color = newColor;
	}
}
