using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class handler : MonoBehaviour {
	public GameObject dialogueText;
	private UnityEngine.UI.Text textThing;
	private int globalPos = 0;
    private int linePos = 0;
	private float currentTime = 0;
	private float speed1 = 0.4f; 		// use values between 0.2 - 1.0, with 1.0 being the max speed
    private float speed2 = 1.2f;
    private float currSpeed = 0.4f;
    private int lineLength = 60;
    private int whichLine = 1;
    private int linesMax = 4;
    private string assignment; // the text we are currently displaying in the box
    private bool newAssignment; // this is used to prevent next skipping us ahead when we aren't done with the text
    private string test = "She lay down beside me, towards dawn she pronounced for the " +
                          "first time the word 'death'. She too seemed to be weary " +
                          "beyond endurance of the task of being a human being; " +
                          "and when I reflected on my dread of the world and its " +
                          "bothersomeness, on money, the movement, women, my studies, " + 
                          "it seemed impossible that I could go on living. I consented " +
                          "easily to her proposal.";

    // this is from Unseen Academicals
    private string test2 = "Technically, the city of Ankh-Morpork is a Tyranny, which is not always the same thing " + 
        "as a monarchy, and in fact even the post of Tyrant has been somewhat redefined by the incumbent, Lord Vetinari, " + 
        "as the only form of democracy that works. Everyone is entitled to vote, unless disqualified by reason of age or not " + 
        "being Lord Vetinari. And yet it does work.This has annoyed a number of people who feel, somehow, that it should not, " + 
        "and who want a monarch instead, thus replacing a man who has achieved his position by cunning, a deep understanding " + 
        "of the realities of the human psyche, breathtaking diplomacy, a certain prowess with the stiletto dagger, and, all " + 
        "agree, a mind like a perfectly balanced circular saw, with a man who has got there by being born. A third proposition, " + 
        "that the city be governed by a choice of respectable members of the community who would promise not to give themselves " + 
        "airs or betray the public trust at every turn, was instantly the subject of music-hall jokes all over the city.";


    void Start () {
		textThing = dialogueText.GetComponent<UnityEngine.UI.Text> ();
        textThing.text = "";
        assignment = test;
        newAssignment = true;

    }

    // Update is called once per frame
    void Update() {
        if (newAssignment)
        {
            printer();
        }
    }


	private void printer(){
         // track time
         currentTime += currSpeed * Time.deltaTime;

        // enough time has passed to try and print another letter
        // use 'while' so if we're going really fast we can print multiple letters
        while (currentTime > 0.01 && globalPos < assignment.Length)
        {
            // reset timer in preparation for next letter
            currentTime -= 0.01f;
            // if we're at a space
            if (assignment.Substring(globalPos, 1).Equals(" "))
            {
                // get the remaining letters
                string rest = assignment.Substring(globalPos,
                        assignment.Length - globalPos);
                string[] restSplit = rest.Split(' ');

                // if we have another word coming up
                if (restSplit.Length > 0)
                {
                    // get the number of chars needed to print next word
                    int next = restSplit[1].Length + 1;

                    // if we don't have enough space, print a newline
                    if (linePos + next >= lineLength)
                    {
                        // we're one letter forward but at the beginning of a new line
                        textThing.text += " \n";
                        linePos = 0;
                        globalPos++;
                        whichLine++;
                    }
                }
            }

            // check if we're currently out of view of the dialogue box
            if (whichLine <= linesMax)
            {
                // if not, add a new line and keep going
                string text = assignment.Substring(globalPos, 1);
                textThing.text += text;
                if (text == "\n")
                    whichLine++;
                globalPos++;
                linePos++;
            }
            else
            {
                // we're finished with this box's worth of text
                newAssignment = false;
                print("press next");
            }
        }
        if (globalPos == assignment.Length)
        {
            // we're out of letters, so still done with this box's worth of text
            newAssignment = false;
        }
    }

    // if the box is full but we have more text, reset box and prepare
    // to display remaining text.
    // if we're done, do nothing.
    // if we're still typing, speed up the text display rate.
    public void nextButton()
    {
        if (!newAssignment && globalPos < assignment.Length)
        {
            assignment = assignment.Substring(globalPos,
                assignment.Length - globalPos);
            // reset this to say we have a new set of text for the dialogue box
            newAssignment = true;
            resetBox();
        }
        else
        {
            // change speed
            currSpeed = speed2;
        }
    }

    // if yes, do this
    // this should be updated to retrieving the correct data
    public void yesButton()
    {
        assignment = test2;
        newAssignment = true;
        resetBox();
    }

    public void noButton()
    {

    }

    // reset progress within dialogue box
    // also reset typing speed and clear existing text
    private void resetBox()
    {
        currSpeed = speed1;
        textThing.text = "";
        whichLine = 1;
        globalPos = 0;
        linePos = 0;
    }
}
