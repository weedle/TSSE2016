using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// adapted off of tabs.cs in TSSE2016
public class bringFront : MonoBehaviour {
	private Button button;
	public GameObject correctPanel;


	// place a listener onto button
	// calls showPanel() when clicked
	void Start () {
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (showPanel);
	}

	// brings the appropriate panel to the front
	void showPanel() {
		var panel = GameObject.Find (correctPanel.name).transform;

		// BEWARE:   --> TESTING BELOW!!! 

		// moar testing
		if (panel) {
			Debug.Log ("we got: " + panel.name + " at " + panel.GetSiblingIndex ());
		}
		panel.transform.SetAsLastSibling ();
		Debug.Log (panel.GetSiblingIndex ());	// testing

		// shouldn't be the transparency issue, hmmmmm
		// despite both tabs being on separate hierarchy order, their index are the same ....




		// END OF TESTING

	}
}
