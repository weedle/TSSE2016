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
            case "trigger":
                {
                    if (full.Length != 2)
                    {
                        print("ERROR: trigger called but no scene given!");
                        return;
                    }
                    triggerLevel(full[1]);
                    break;
                }
            case "money":
                {
                    if (full.Length != 2)
                    {
                        print("ERROR: money called but no value given!");
                        return;
                    }
                    money(int.Parse(full[1]));
                    break;
                }
            case "addmoney":
                {
                    if (full.Length != 2)
                    {
                        print("ERROR: money called but no value given!");
                        return;
                    }
                    addmoney(int.Parse(full[1]));
                    break;
                }
        }
    }

    private void triggerLevel(string levelId)
    {
        if(levelId == "Level1")
        {
            GetComponent<Level1Gen>().Level1Setup();
            loadScene("prebattle");
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
    {if (character == "vanish")
        {
            GameObject.Find("GameLogic")
                .GetComponent<DialogueManager>().toggleAllComps(false);
        }
        else
        {
            Character chrNext = new Character(character, 0);
            //Character chrNext = new MerchantXYZ();
            GameObject.Find("GameLogic")
                .GetComponent<DialogueManager>().
                setCharacter(chrNext);
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

    public void loadHomeScene()
    {
        SceneManager.LoadScene("scenes/Level0", LoadSceneMode.Single);
    }

    public void money(int newMoney)
    {
        PlayerPrefs.SetInt("score", newMoney);
    }

    public void addmoney(int newMoney)
    {
        int money = PlayerPrefs.GetInt("score");
        PlayerPrefs.SetInt("score", money + newMoney);
    }
}
