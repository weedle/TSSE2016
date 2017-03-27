using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/* NOTES:
 * 	- can we still have, in terms of implementation, simple
 *    animations while NOT using timescales? Hmmmm >.<
 */
public class slider : MonoBehaviour {

	// refers to the panel we are animating
	public GameObject alchemyPanel;

	// button that controls animation
	private Button button;

	// variable for animator component in the respective alchemyPanel'
	private Animator anim;

	// variable for checking the panel state (on / off screen)
	private bool onScreen;



	void Start () {

		button = gameObject.GetComponent<Button> ();
		button.onClick.AddListener (superSlideHandler);
		onScreen = false;

		// recall Unity's default timeScale == 0;
		// how does this work again?
		Time.timeScale = 1;

		// get animator component from the desired panel
		anim = alchemyPanel.GetComponent<Animator> ();

		// disables the default animation
		// is this actuall necessary though?
		anim.enabled = false;
	}


	// slides the panel accordingly, depending on whether it is
	// currently on screen or not
	public void superSlideHandler() {
		if (onScreen) {
			dragUp ();
		} else {
			dragDown ();
		}
	}


	// not sure how timescale works here
	public void dragDown() {
		anim.enabled = true;
		anim.Play ("ALCHEMY-SLIDER-IN");
		onScreen = true;
		// this freezes the timescale --> the animation stops
		Time.timeScale = 0;
	}

	// not sure how timescale works here
	public void dragUp() {
		anim.Play ("ALCHEMY-SLIDER-OUT");
		onScreen = false;
		// sets the timescale back to normal
		Time.timeScale = 1;
	}
}
