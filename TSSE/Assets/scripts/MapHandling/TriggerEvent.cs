using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TriggerEvent : MonoBehaviour {
    public GameObject PlayerShip;
	public string level;


	/*
	 * initialize variables 
	 * ("Settings" is in: scenes/Menus/PersonalInfoMenu/Settings)
	 */ 
	void Start () {
		level = "Settings";
	}


	/*
	 * loads the scene assigned to 'level' on Start (?)
	 * 
	 * COMMENT: don't know where this is used
	 */ 
    void OnTriggerEnter2D(Collider2D Collider)
    {
        if (Collider.gameObject.tag == "Player") 
        	SceneManager.LoadScene(level);
    }
		

	/*
	 * loads appropriate menu if the object we collided with has a
	 *    Collider2D component enabled
	 * 
	 * COMMENT: the Collision2D 'coll' does not appear to be used
	 */ 
    void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerShip = GameObject.FindWithTag("Player");
        if (PlayerShip)
        {
            SceneManager.LoadScene("Ships");
        }
    }


	/*
	 * loads 'Ships' scene from scenes/Menus/PersonalInfoMenu
	 * COMMENT: the string 'Ships' does not appear to be used...
	 */
    public void ShipsBtnLoad(string Ships)
    {
        SceneManager.LoadScene("Ships");
    }
}
