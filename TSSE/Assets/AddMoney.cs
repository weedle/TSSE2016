using UnityEngine;
using System.Collections;

public class AddMoney : MonoBehaviour {
    public int money = 200;
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
