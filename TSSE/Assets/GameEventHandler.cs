using UnityEngine;
using System.Collections;

public class GameEventHandler : MonoBehaviour {

	public void callEvent(string param)
    {
        string[] full = param.Split(' ');
        string eventName = full[0];
        switch(eventName)
        {
            case "print":
                printTest();
                break;
            case "dialogue":
                if(full.Length != 2)
                {
                    print("ERROR: dialogue called but no character name given!");
                    return;
                }
                dialogue(full[1]);
                break;
        }
    }

    private void printTest()
    {
        print("printTest called!");
    }

    private void dialogue(string character)
    {
        if(character == "MerchantXYZ")
        {
            Character chrNext = new MerchantXYZ();
        GameObject.Find("GameLogic")
            .GetComponent<DialogueManager>().
            setCharacter(chrNext);
        }
    }
}
