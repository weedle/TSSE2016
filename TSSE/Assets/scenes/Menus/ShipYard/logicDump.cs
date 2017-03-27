using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class logicDump : MonoBehaviour {
	
	public GameObject hiddenPopUp;

	void Start () {
		hiddenPopUp.SetActive (false);					// makes the ship-mod hidden on start up
	}	
}
