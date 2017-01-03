using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {
    private GameObject textbox;
    private GameObject nameBox;
    private GameObject yesButton;
    private GameObject noButton;
    private GameObject nextButton;
	// Use this for initialization
	void Start () {
        foreach (Transform childT in 
            gameObject.GetComponentInChildren<Transform>())
        {
            switch(childT.gameObject.name)
            {
                case "textbox":
                    textbox = childT.gameObject;
                    break;
                case "namebox":
                    nameBox = childT.gameObject;
                    break;
                case "yesButton":
                    yesButton = childT.gameObject;
                    break;
                case "noButton":
                    noButton = childT.gameObject;
                    break;
                case "next":
                    nextButton = childT.gameObject;
                    break;
            }
        }
        deactivate();
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonUp("Fire2"))
        {
            if(textbox.activeSelf == false)
            {
                activate();
            }
            else
            {
                deactivate();
            }
        }
	}

    void activate()
    {
        textbox.SetActive(true);
        nameBox.SetActive(true);
        noButton.SetActive(true);
        yesButton.SetActive(true);
        nextButton.SetActive(true);
    }

    void deactivate()
    {
        textbox.SetActive(false);
        nameBox.SetActive(false);
        noButton.SetActive(false);
        yesButton.SetActive(false);
        nextButton.SetActive(false);
    }
}
