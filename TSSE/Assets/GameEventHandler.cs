using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
            case "loadScene":
                if (full.Length != 2)
                {
                    print("ERROR: loadScene called but no scene given!");
                    return;
                }
                loadScene(full[1]);
                break;
        }
    }

    private void printTest()
    {
        print("printTest called!");
    }

    private void loadScene(string scene)
    {
        print("loading scene: " + scene);
        //SceneManager.LoadScene(scene, LoadSceneMode.Additive);
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
