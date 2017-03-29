using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class logicDump : MonoBehaviour {
	
	public GameObject hiddenPopUpTwoSlot;
    public GameObject hiddenPopUpFourSlot;
    public GameObject hiddenPopUpSixSlot;

    void Start () {
		hiddenPopUpTwoSlot.SetActive(false);                  // makes the ship-mod hidden on start up
        hiddenPopUpFourSlot.SetActive(false);
        hiddenPopUpSixSlot.SetActive(false);
    }	
}
