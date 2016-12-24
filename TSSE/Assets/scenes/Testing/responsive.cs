using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/* 
 * - Used to implement the console text-box for testing
 * - [ essentially parses text input and initiates appropriate action ]
 */

public class responsive : MonoBehaviour {
	public InputField textField;
	public string input;
	public GameObject logbox;
	public Text logtext;

    private ShipSpawner spawner;

    public GameObject ally;
    public GameObject enemy;



	/* 
	 * assign variables and adds a listener to the console to detect input
	 */ 
	void Start () {
		textField = gameObject.GetComponent<InputField> ();
		logbox = GameObject.Find ("Log");
		logtext = logbox.GetComponent<Text> ();

        spawner = GameObject.Find("GameLogic")
            .GetComponent<ShipSpawner>();

		var submit = new InputField.SubmitEvent ();
		submit.AddListener (textSubmit);
		textField.onEndEdit = submit;
	}



	/*
	 * handles console input and does appropriate actions
	 * NOTE: see 'console manual' for more info
	 * NOTE: this mainly just spawn ships right now
	 */
    private void textSubmit(string inputText)
    {
        changeLogText(inputText);

        string[] textParam = inputText.Split(" "[0]);
        int paramNum = textParam.Length;


		if (textParam [0].Equals ("clear")) 
			spawner.deleteAll ();

        if (textParam[0].Equals("reset"))
            PlayerPrefs.SetInt("score", 0);

        if (textParam[0].Equals("loadout"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        if (paramNum > 1)
        {
            if (textParam[0].Equals("s"))
            {

                if (textParam[1].Equals("rand"))
                    spawner.spawnBunch();

                if (paramNum > 2)
                {
                    int num = 1;
                    ShipDefinitions.Faction faction = ShipDefinitions.Faction.PlayerAffil;

                    if (paramNum == 4)
                        num = int.Parse(textParam[3]);
					
                    faction = ShipDefinitions.Faction.Indep;

					// reads input for which faction you want to spawn ships of
                    if (textParam[1].Equals("a"))
                        faction = ShipDefinitions.Faction.PlayerAffil;
                    if (textParam[1].Equals("e"))
                        faction = ShipDefinitions.Faction.Enemy;
					
                    for (int i = 1; i <= num; i++)
                    {
						// gets a random point to generate enemy/ally ships from
                        Vector2 randPt = Bounds.getRandPosInBounds();
                        if (faction == ShipDefinitions.Faction.PlayerAffil)
                        {
                            randPt = ally.transform.position;
                        }
                        else
                        {
                            randPt = enemy.transform.position;
                        }

                        switch (textParam[2])
                        {
                            case "f":
                                spawner.spawnFireShip(randPt, faction);
                                break;
                            case "c":
                                spawner.spawnCrownShip(randPt, faction);
                                break;
                            case "m":
                                spawner.spawnMissileShip(randPt, faction);
                                break;
                            case "l":
                                spawner.spawnLaserShip(randPt, faction);
                                break;
                        }
                    }
                }
            }

			// tries to find the desired ship name and provide manual control of the ship
            else if (textParam[0].Equals("n"))
            {
                foreach (MainShip ship in GameObject.FindObjectsOfType<MainShip>())
                {
                    if (ship.getName().Equals(textParam[1]) ||
                        ship.getName().Substring(ship.getName().Length - 4).Equals(textParam[1]))
                    {
                        Camera.main.GetComponent<Pause>()
                            .requestManualControl(ship.gameObject);
                        break;
                    }
                }
            }
            else if (textParam[0].Equals("scrap"))
            {
                PlayerPrefs.SetInt("score", int.Parse(textParam[1]));
            }

			// handles input for selecting the specified ship that is to be targetted by another specified ship
			if (paramNum > 2) {
				if (textParam [0].Equals ("t")) {
					ShipController ship1 = null;
					GameObject ship2 = null;
					foreach (MainShip ship in GameObject.FindObjectsOfType<MainShip>()) {
						if (ship.getName ().Equals (textParam [1]) ||
						                      ship.getName ().Substring (ship.getName ().Length - 4).Equals (textParam [1])) {
							ship1 = ship.gameObject.GetComponent<ShipController> ();
						} else if (ship.getName ().Equals (textParam [2]) ||
						                           ship.getName ().Substring (ship.getName ().Length - 4).Equals (textParam [2])) {
							ship2 = ship.gameObject;
						}
					}

					if (ship1 != null && ship2 != null) {
						ship1.setTarget (ship2);
					}
				}
			}

			// clears the console / input field
            textField.text = "";
        }
    }



	/*
	 * changes the text in the upper-left log/display to what was
	 * just entered into the console
	 */
	private void changeLogText(string newLogText)
	{
		logtext.text = newLogText;
	}



	/* 
	 * allows for 'auto-focus' onto the console by pressing a key
	 */
    void Update()
    {
        if (!textField.isFocused)
        {
            if (Input.GetButtonDown("Console"))
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(textField.gameObject, null);
            }
        }
    }

}
