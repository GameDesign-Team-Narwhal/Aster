using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Script for for a GUI indicator bar which shows a percentage of something, like health or XP
[RequireComponent (typeof (RawImage))]
[AddComponentMenu("UI/Bar")]
public class Bar : MonoBehaviour 
{
	private RawImage barImage;

	private float initialXPos;
	private Vector2 initialSize;


	// Use this for initialization
	void Awake () {

		barImage = GetComponent<RawImage>();

		//save initial values
		initialSize = barImage.rectTransform.sizeDelta;

		initialXPos = barImage.rectTransform.anchoredPosition.x;
	}

	//update the bar to display a new value with a different color.
	public void UpdateBar(float currentValue, float maxValue, Color newColor)
	{

		barImage.color = newColor;
		UpdateBar(currentValue, maxValue);
	}

	
	//update the bar to display a new value.
	public void UpdateBar(float currentValue, float maxValue)
	{
		float valuePercentage = Mathf.Clamp(currentValue, 0, maxValue) / maxValue;

		//set how much of the image is shown
		Rect drawnPart = new Rect(0, 0, valuePercentage, 1);
		
		barImage.uvRect = drawnPart;		
		//now move and scale it to the right place
		Vector2 healthbarPos = barImage.rectTransform.anchoredPosition;
		
		healthbarPos.x = initialXPos - ((initialSize.x * (1 - valuePercentage))); //no idea why this is not divided by 2, it should be
		
		barImage.rectTransform.anchoredPosition = healthbarPos;

		barImage.rectTransform.sizeDelta = new Vector2(valuePercentage * initialSize.x, initialSize.y);
	}
}
