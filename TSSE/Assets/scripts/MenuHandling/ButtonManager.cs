using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections;

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
    // switch case 
}
