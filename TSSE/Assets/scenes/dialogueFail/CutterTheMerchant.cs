using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("Dialogue")]
public class CutterTheMerchant : Character
{
    [XmlAttribute("DialogueRegular")]
    public string dReg;
    [XmlAttribute("DialogueYes")]
    public string dYes;
    [XmlAttribute("DialogueNo")]
    public string dNo;
    //[XmlArray("DialogueSnippets"), XmlArrayItem("DialogueSet")]
    //public List<Dialogue> dialogues;
    public override void initialize()
    {
        var serializer = new XmlSerializer(typeof(CutterTheMerchant));
        var stream = new FileStream("Assets/scenes/dialogueFail/CutterTheMerchant.xml", FileMode.Open);
        var container = serializer.Deserialize(stream) as CutterTheMerchant;
        stream.Close();
        dReg = container.dReg;
        dYes = container.dYes;
        dNo = container.dNo;
        //this.dialogues = container.dialogues;
    }

    public override string getRegularDialogue()
    {
        return dReg;//dialogues[0].dialogueRegular;
    }

    public override string getYesDialogue()
    {
        return dYes;//dialogues[0].dialogueYes;
    }
    public override string getNoDialogue()
    {
        return dNo;// dialogues[0].dialogueNo;
    }
}
