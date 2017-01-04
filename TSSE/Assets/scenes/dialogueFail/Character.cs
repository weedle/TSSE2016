using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public abstract class Character
{
    public struct Dialogue
    {
        [XmlAttribute("DialogueRegular")]
        public string dialogueRegular;
        [XmlAttribute("DialogueYes")]
        public string dialogueYes;
        [XmlAttribute("DialogueNo")]
        public string dialogueNo;
    };
    [XmlAttribute("name")]
    public string name;

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
        return dialogues[0].dialogueRegular;
    }

    public string getYesDialogue()
    {
        return dialogues[0].dialogueYes;
    }
    public string getNoDialogue()
    {
        return dialogues[0].dialogueNo;
    }

    public string getName()
    {
        return name;
    }
}