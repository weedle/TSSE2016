using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;

[XmlRoot("DialogueCollection")]
public class Character
{
    public int index = 0;
	public string name;
	public List<Dialogue> dialogues = new List<DialogueManager> ();
	public int level;

    public struct Dialogue
    {

        // what to say to the player
        public string dialogueRegular;

        // what to say if the player responds "yes"
        // leave blank to not display yes/no
        public string dialogueYes;

        // what to say if the player responds "no"
        public string dialogueNo;

        // 0 for no callback
        // 1 for after dialogueRegular
        // 2 after dialogueYes
        // 3 after dialogueNo
        public int callbackType;

        // what function to call after dialogue
        // this is handled in GameLogic->GameEventHandler
        public string cb;

		// 'Dialogue' constructor
		public Dialogue(string dialogueRegular, string dialogueYes,
			string dialogueNo, int callbackType, string cb) {

			this.dialogueRegular = dialogueRegular;
			this.dialogueNo = dialogueNo;
			this.dialogueYes = dialogueYes;
			this.callbackType = callbackType;
			this.cb = cb;
		}

    };


    //public abstract void initialize();


	/* 'Character' constructor 
	 * 	- given a valid character name and the desired level, we
	 * 	  should be able to grab all the different dialogues the
	 *    character has in that level
	 * 
	 *  - please refer to the new formatting of the .xml files for
	 *    character dialogue for more info
	 */
	public Character(string name, int level) {
		this.name = name;
		this.level = level;

		dialogues = getAllDialogues (name, level);

	}


	/*
	 * gets all dialogues for a given character and level
	 * 
	 * NOTE: 'stringInUse' is a multi-purpose string >.< 
	 */
	public List<Dialogue> getAllDialogues(string name, int level) {
		
		List<Dialogue> allDialogues = new List<Dialogue> ();

		XmlDocument doc = new XmlDocument ();
		string stringInUse = string.Concat (name, ".xml");
		string path = Path.Combine ("Assets\\scenes\\dialogueFail\\", stringInUse);
		doc.Load (path);

		stringInUse = string.Concat ("\\Dialogues[@type='", level, "']");
		XmlNode relevantDialogues = doc.SelectNodes (stringInUse);

		foreach (XmlNode node in relevantDialogues.ChildNodes) {
			allDialogues = dialogues.Add (parseSingleDialogue (node));
		}

		return allDialogues;
	}



	/*
	 * gets all the possible dialogue types given a relevant 
	 * 'dialogue' node/element 
	 * 
	 * - PLEASE REFER TO A .xml FILE FOR RELEVANT FORMATTING
	 */
	public Dialogue parseSingleDialogue(XmlNode dialogueN) {
		string regularDialogue = dialogueN.SelectSingleNode ("regular").InnerText;
		string yesDialogue = dialogueN.SelectSingleNode ("yes").InnerText;
		string noDialogue = dialogueN.SelectSingleNode ("no").InnerText;

		XmlNode callback = dialogueN.SelectSingleNode ("callback");
		int callbackType = callback.Attributes ["type"].InnerText;
		string cb = callback.Attributes ["cb"].InnerText;

		Dialogue retDia = new Dialogue (regularDialogue, yesDialogue, noDialogue,
			                  callbackType, cb);
		return retDia;
	}


	// still have yet to do this properly
	// Each character needs to handle the index value in their own special way
	public abstract void handleIndex();


	// mainly 'get' methods below


    public string getRegularDialogue()
    {
        return dialogues[index].dialogueRegular;
    }

    public string getYesDialogue()
    {
        return dialogues[index].dialogueYes;
    }
    public string getNoDialogue()
    {
        return dialogues[index].dialogueNo;
    }

    public string getName()
    {
        return name;
    }

    public int getCallbackType()
    {
        return dialogues[index].callbackType;
    }

    public string getCallback()
    {
        return dialogues[index].callback;
    }

}