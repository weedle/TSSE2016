using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tabs : MonoBehaviour {
	public Button button;
	public string panelName;

	void Start() {
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (changePanes);
	}

	void changePanes() {
		var panel = GameObject.Find (panelName);
		panel.transform.SetAsLastSibling();
	}
}
