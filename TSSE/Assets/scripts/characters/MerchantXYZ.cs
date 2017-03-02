using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("DialogueCollection")]
public class MerchantXYZ
{
    

    public void initialize()
    {
        var serializer = new XmlSerializer(typeof(MerchantXYZ));
        var stream = new FileStream("Assets/scenes/dialogueFail/MerchantXYZ.xml", FileMode.Open);
        var container = serializer.Deserialize(stream) as MerchantXYZ;
        stream.Close();
        //dialogues = container.dialogues;
        //name = container.name;
    }

    public void handleIndex()
    {
        //if (index > 0)
        //    index = 0;
    }
}
