using UnityEngine;
using System.Collections.Generic;
public abstract class Character
{
    /*
    public class Dialogue
    {
        public string dialogueRegular;
        public string dialogueYes;
        public string dialogueNo;
    };
    */
    //public string Name;
    //private string factionRaw;
    //public ShipDefinitions.Faction Affiliation;

    //public int level;
    //public int dialogueLevel;


    public abstract void initialize();

    public abstract string getRegularDialogue();

    public abstract string getYesDialogue();

    public abstract string getNoDialogue();
}