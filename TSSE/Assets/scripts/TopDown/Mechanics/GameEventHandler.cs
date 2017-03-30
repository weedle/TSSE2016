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
            case "scene":
                if (full.Length != 2)
                {
                    print("ERROR: scene called but no scene given!");
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
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    private void dialogue(string character)
    {
        if(character == "CutterTheMerchant")
        {
            Character chrNext = new Character("CutterTheMerchant", 0);
            //Character chrNext = new MerchantXYZ();
        GameObject.Find("GameLogic")
            .GetComponent<DialogueManager>().
            setCharacter(chrNext);
        }
        else if (character == "vanish")
        {
            GameObject.Find("GameLogic")
                .GetComponent<DialogueManager>().toggleAllComps(false);
        }
    }

    public void printThing(string thing)
    {
        print(thing);
    }

    public void merchant(string merchantId)
    {
        PlayerPrefs.SetString("TSSE[Level][Merchant]", merchantId);
        SceneManager.LoadScene("scenes/merchantMenu", LoadSceneMode.Single);
    }

    public void combat()
    {
        SceneManager.LoadScene("scenes/arena", LoadSceneMode.Single);
    }
}
