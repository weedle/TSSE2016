using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* NOTES:
 *   03/16/2017 - not going to do any transitions / animations yet 
 * 	 - gameObject is a button	
 * 	 
 */
public class makePop : MonoBehaviour {
	public GameObject popUp;		
	private Button button;
	private bool isActive;

	private GameObject shipper;

	void Start () {
		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (displayNewPop);
		isActive = false;

		//shipper = gameObject.GetComponentInParent<GameObject> ();
	}



	public void displayNewPop() {
		setUpMenu ();
		popUp.SetActive (!isActive);
		isActive = !isActive;
	}

	// makes necessary changes to the hidden pop-up menu
	public void setUpMenu() {

		// set image




	}
}
