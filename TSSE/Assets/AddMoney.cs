using UnityEngine;
using System.Collections;

public class AddMoney : MonoBehaviour {
    int money = 5000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addMoney()
    {
        PlayerPrefs.SetInt("score", money);
    }
}
