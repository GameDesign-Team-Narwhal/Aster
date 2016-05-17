using UnityEngine;
using System.Collections;

[RequireComponent (typeof(TextMesh))]
//script for the text which pops up when you pick up an upgrade
//It starts animating when it is instantiated, and destroys itself when the animation is over
public class UpgradeText : MonoBehaviour {

    TextMesh textMesh;

    public string text
    {
        get
        {
            return textMesh.text;
        }
        set
        {
            textMesh.text = value;
        }
    }

	// Use this for initialization
	void Awake () {
        textMesh = GetComponent<TextMesh>();
	}
	
    //called by the animation when it ends
    public void OnAnimationEnd()
    {
        GameObject.Destroy(gameObject);
    }
}
