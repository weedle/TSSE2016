using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public class CutterTheMerchant
{
    public void initialize()
    {
        var serializer = new XmlSerializer(typeof(CutterTheMerchant));
        var stream = new FileStream("Assets/scenes/dialogueFail/CutterTheMerchant.xml", FileMode.Open);
        var container = serializer.Deserialize(stream) as CutterTheMerchant;
        stream.Close();
        //dialogues = container.dialogues;
        //name = container.name;
    }

    public void handleIndex()
    {
        //if(index < 2)
        //    index++;
        //else
        //    index = 0;
    }
}
