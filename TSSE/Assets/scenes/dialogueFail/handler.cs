using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class handler : MonoBehaviour {
	public GameObject dialogueText;
	private UnityEngine.UI.Text textThing;
	private int pos = 0;
	private float currentTime = 0;
	private float speed = 0.5F; 		// use values between 0.2 - 1.0, with 1.0 being the max speed

	


	private string test = "She lay down beside me, towards dawn she pronounced for the first time the word 'death'."+
		"She too seemd to be weary beyond endurance of the task of being a human being; and when I reflected on my dread"+
		"of the world and its bothersomeness, on money, the movement, women, my studies, it seemed impossible that I could"+
		"go on living. I consented easily to her proposal.";

	void Start () {
		textThing = dialogueText.GetComponent<UnityEngine.UI.Text> ();

	}

	// Update is called once per frame
	void Update () {
		printer (test);		// is this practical? it seems unnecessary to call this 'once per frame'
	}

	private void printer(string sampleText){
		currentTime += speed*Time.deltaTime;
		if (pos < sampleText.Length && currentTime > 0.01) {
			currentTime = 0;
			pos++;
			textThing.text = sampleText.Substring (0, pos);
		}
	}
}
