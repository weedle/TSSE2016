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
	public List<Dialogue> dialogues;
	public int level;
    private string displayName = "";

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

        //dialogues = getAllDialogues (name, level);
    }


	/*
	 * gets all dialogues for a given character and level
	 * 
	 * NOTE: 'stringInUse' is a multi-purpose string >.< 
	 */
	public List<Dialogue> getAllDialogues(string name, int level) {
		
		List<Dialogue> allDialogues = new List<Dialogue>();

        XmlDocument doc = new XmlDocument ();
		string stringInUse = string.Concat (name, ".xml");
		string path = Path.Combine ("Assets/scripts/characters/", stringInUse);
		doc.Load (path);

        stringInUse = "/DialogueCollection/Dialogues [@level=\"0\"]";
        XmlNodeList relevantDialogues = doc.SelectNodes (stringInUse);

        displayName = doc.SelectSingleNode("/DialogueCollection").Attributes[0].Value;

        //GameEventHandler.print(doc.HasChildNodes);
        //foreach (XmlNode node in relevantDialogues) {
        foreach (XmlNode node in relevantDialogues)
        {
            parseSingleDialogue(node.ChildNodes[0]);
            //allDialogues.Add(parseSingleDialogue (node.ChildNodes[0]));
        }
        return allDialogues;
	}

    public void initialize()
    {
        dialogues = new List<Dialogue>();
        getAllDialogues(name, level);
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
		int callbackType = int.Parse(callback.Attributes ["type"].InnerText);
		string cb = callback.Attributes ["cb"].InnerText;

		Dialogue retDia = new Dialogue (regularDialogue, yesDialogue, noDialogue,
			                  callbackType, cb);
        dialogues.Add(retDia);
        return retDia;
	}


	// still have yet to do this properly
	// Each character needs to handle the index value in their own special way
	public void handleIndex()
    {
        index = 0;
    }


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
        return displayName;
    }

    public int getCallbackType()
    {
        return dialogues[index].callbackType;
    }

    public string getCallback()
    {
        return dialogues[index].cb;
    }

}