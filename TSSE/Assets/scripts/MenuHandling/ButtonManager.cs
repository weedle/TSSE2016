using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;


/*
 * - used to load appropriate scenes after clicking related buttons
 * - all scenes can be found under scenes/Menus/PersonalInfoMenu
 */
public class ButtonManager : MonoBehaviour {

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



	/*
	 * a refactored version of the above with the same functionality (?)
	 * 
	 * NOTE: this has been refactored -> simpler, fewer lines of code
	 * WARNING: must add open scenes onto build settings for onClick to work!!!
	 */
    public void LoadScenes(string scenePath)
    {
        SceneManager.LoadScene(scenePath);  
    }
}
