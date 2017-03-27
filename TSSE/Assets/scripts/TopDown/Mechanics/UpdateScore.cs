using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int score = PlayerPrefs.GetInt("score");
        GetComponent<Text>().text = "Score: " + score;
	}
}
