using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;




public class TriggerEvent : MonoBehaviour {
    public GameObject PlayerShip;
    // Use this for initialization
    public string level = "Settings";

    // Use this for initialization
    void OnTriggerEnter2D(Collider2D Colider)
    {
        if (Colider.gameObject.tag == "Player") 
        SceneManager.LoadScene(level);
    }




    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerShip = GameObject.FindWithTag("Player");
        // If the Collider2D component is enabled on the object we collided with
        if (PlayerShip)
        {
            SceneManager.LoadScene("Ships");
        }
    }

    public void ShipsBtnLoad(string Ships)
    {
        SceneManager.LoadScene("Ships");
    }



}
