using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

public class ButtonManager : MonoBehaviour {

    // Load new scenes after pressing the appropriate buttons 
    public void SettingsBtn (string Settings)
    {
        SceneManager.LoadScene(Settings); 
    }

    public void BackBtn (string PersonalInfo)
    {
        SceneManager.LoadScene(PersonalInfo);
    }

    public void QuestsBtn (string Quests)
    {
        SceneManager.LoadScene(Quests);
    }

    public void MyItemsBtn (string MyItems)
    {
        SceneManager.LoadScene(MyItems); 
    }

    public void ShipsBtn (string Ships)
    {
        SceneManager.LoadScene(Ships);
    }

    public void ShipsBtnLoad(string Ships)
    {
        SceneManager.LoadScene("Ships");
    }

    // Refactored - Much Simplier and fewer lines of code for loading scenes 
    // Must add open scenes onto build settings for onClick to work 
    public void LoadScenes(string scenePath)
    {
        SceneManager.LoadScene(scenePath);  
    }
}
