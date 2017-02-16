using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// adapted off of tabs.cs in TSSE2016
public class bringForth : MonoBehaviour {
	private Button button;
	public GameObject correctPanel;
	private Text box; 


	// place a listener onto button
	// calls showPanel() when clicked
	void Start () {
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (showPanel);

		// for testing purposes only
		box = GameObject.Find ("nameDisplay").GetComponent<Text> ();
	}

	// brings the appropriate panel to the front
	void showPanel() {
		var panel = GameObject.Find (correctPanel.name);
		panel.transform.SetAsLastSibling ();

		// for testing purposes only
		box.text = correctPanel.name;
	}
}
