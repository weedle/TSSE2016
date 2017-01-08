using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public abstract class Character
{
    public int index = 0;
    public struct Dialogue
    {
        [XmlAttribute("level")]
        public int level;
        [XmlAttribute("DialogueRegular")]
        public string dialogueRegular;
        [XmlAttribute("DialogueYes")]
        public string dialogueYes;
        [XmlAttribute("DialogueNo")]
        public string dialogueNo;
    };
    [XmlAttribute("name")]
    public string name = "";

    [XmlArray("Dialogues")]
    [XmlArrayItem("Dialogue")]
    public List<Dialogue> dialogues = new List<Dialogue>();

    //public string Name;
    //private string factionRaw;
    //public ShipDefinitions.Faction Affiliation;

    //public int level;
    //public int dialogueLevel;

    public abstract void initialize();

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

    // Each character needs to handle the index value in their own special way
    public abstract void handleIndex();
}