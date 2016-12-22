using UnityEngine;
using System.Collections;

public class DialogueHandler : MonoBehaviour {
    public GameObject dialogueText;
    private UnityEngine.UI.Text textThing;
    private int pos = 0;
    private float timeSoFar = 0;
	// Use this for initialization
	void Start () {
        textThing = dialogueText.GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timeSoFar += Time.deltaTime;
        string full = "This is a test dialogue thing and it seems to work alright";
        if(pos < full.Length)
        {
            if(timeSoFar > 0.01)
            {
                timeSoFar = 0;
                pos++;
            }
        }
        textThing.text = full.Substring(0, pos);
    }
}
