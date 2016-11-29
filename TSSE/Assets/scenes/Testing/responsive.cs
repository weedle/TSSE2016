using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class responsive : MonoBehaviour {
	public InputField textField;
	public string input;
	public GameObject logbox;
	public Text logtext;
	public ShipSpawner ally;
	//public ShipSpawner enemy;



	void Start () {
		textField = gameObject.GetComponent<InputField> ();

		logbox = GameObject.Find ("Log");
		logtext = logbox.GetComponent<Text> ();

		ally = GameObject.Find ("spawnLocation01").GetComponent<ShipSpawner>();
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
			ally.deleteAll ();
		}

		if (paramNum > 1) {

			if (textParam [0].Equals ("s")) {

				if (textParam [1].Equals ("rand")) {
					ally.spawnBunch ();
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

					for (int i = 0; i <= num; i++) {

						Vector2 randPt = Bounds.getRandPosInBounds ();
						switch (textParam [2]) {
							
						case "f":
							ally.spawnFireShip(faction, randPt);
							break;
						case "c":
							ally.spawnCrownShip(faction, randPt);
							break;
						case "m":
							ally.spawnMissileShip(faction, randPt);
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
