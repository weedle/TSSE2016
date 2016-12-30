using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class loadNext : MonoBehaviour {
	public GameObject button;
	private Button myButton;
	private string text;


	// Use this for initialization
	void Start () {
		myButton = button.GetComponent<Button> ();
		// myButton.onClick.AddListener(getText(text));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void getText(string text) {
		// text = genXMLparse.Deserialize ("dialogue.xml");

	}
}
