using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// Used to implement tab functionality in the Merchant menu/scene

public class tabs : MonoBehaviour {
	public Button button;
	public string panelName;

	// adds listener to button
	// calls changePanes() when button is selected
	void Start() {
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (changePanes);
	}
		
	// displays the appropriate panel by bringing it forward
	// WARNING: may need modification to accomodate more than
	// 			two panels!
	void changePanes() {
		var panel = GameObject.Find (panelName);
		panel.transform.SetAsLastSibling();
	}
}
