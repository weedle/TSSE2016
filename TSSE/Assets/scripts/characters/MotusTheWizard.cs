using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public class MotusTheWizard
{
    public void initialize()
    {
        var serializer = new XmlSerializer(typeof(MotusTheWizard));
        var stream = new FileStream("Assets/scenes/dialogueFail/MotusTheWizard.xml", FileMode.Open);
        var container = serializer.Deserialize(stream) as MotusTheWizard;
        stream.Close();
        //dialogues = container.dialogues;
        //name = container.name;
    }

    public void handleIndex()
    {
        //if (index < 2)
        //    index++;
    }
}
