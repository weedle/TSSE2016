using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class responsive : MonoBehaviour {
	public InputField textField;
	public string input;
	public GameObject logbox;
	public Text logtext;
    private ShipSpawner spawner;

    public GameObject ally;
    public GameObject enemy;
	//public ShipSpawner ally;
	//public ShipSpawner enemy;



	void Start () {
		textField = gameObject.GetComponent<InputField> ();

		logbox = GameObject.Find ("Log");
		logtext = logbox.GetComponent<Text> ();

        spawner = GameObject.Find("GameLogic")
            .GetComponent<ShipSpawner>();
		//ally = GameObject.Find ("spawnLocation01").GetComponent<ShipSpawner>();
		//enemy = GameObject.Find ("spawnLocation02");

	
		var submit = new InputField.SubmitEvent ();
		submit.AddListener (textSubmit);
		textField.onEndEdit = submit;
	}


	// handles console response to text changes
	// essentially just spawn ships for now
	// maybe create some helper functions later?
	private void textSubmit(string inputText) 
	{
		changeLogText (inputText);

		string[] textParam = inputText.Split(" "[0]); 
		int paramNum = textParam.Length;

		if (textParam[0].Equals ("clear")) {
			//enemy.deleteAll ();
			spawner.deleteAll ();
		}

		if (paramNum > 1) {

			if (textParam [0].Equals ("s")) {

				if (textParam [1].Equals ("rand")) {
                    spawner.spawnBunch ();
				}

				if (paramNum > 2) {
					int num = 1;
					ShipDefinitions.Faction faction = ShipDefinitions.Faction.PlayerAffil;	

					// might be some issues here with parameter calls
					if (paramNum == 4)
						num = int.Parse (textParam [3]);
					faction = ShipDefinitions.Faction.Indep;

					if (textParam [1].Equals ("a"))
						faction = ShipDefinitions.Faction.PlayerAffil;
					if (textParam [1].Equals ("e"))
						faction = ShipDefinitions.Faction.Enemy;
					for (int i = 1; i <= num; i++) {

						Vector2 randPt = Bounds.getRandPosInBounds ();
                        if (faction == ShipDefinitions.Faction.PlayerAffil)
                        {
                            randPt = ally.transform.position;
                        }
                        else
                        {
                            randPt = enemy.transform.position;
                        }
						switch (textParam [2]) {
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
			} else if (textParam[0].Equals("n")){
				

			}

		}

		// haven't figured out to empty out the input field yet...
		// this doesn't work, ehue...
		textField.text = "";
	}


	// changes the text in the upper-left log
	// currently just displays what is entered
	private void changeLogText(string newLogText)
	{
		logtext.text = newLogText;
	}


}
