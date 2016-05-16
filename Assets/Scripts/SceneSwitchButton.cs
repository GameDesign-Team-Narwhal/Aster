using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Button))]
public class SceneSwitchButton : MonoBehaviour {

    public string sceneName;

    private Button button;

	// Use this for initialization
	void Awake () {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnClick);
	}

    void OnClick()
    {
        Application.LoadLevel(sceneName);
    }
}
