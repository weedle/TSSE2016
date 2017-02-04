using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemTester : MonoBehaviour {
    private List<ShipDefinitions.Item> myItems;
	// Use this for initialization
	void Start () {
        myItems = new List<ShipDefinitions.Item>();
        myItems.Add(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModDamage));
        myItems.Add(ShipDefinitions.stringToItem(
            ShipDefinitions.itemToString(new ShipDefinitions.Item(ShipDefinitions.ItemType.CrownModDamage))));
        myItems.Add(ShipDefinitions.intToItem(
            ShipDefinitions.itemToInt(
                new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModSpread))));
    }
	
	// Update is called once per frame
	void Update () {
        UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();
        text.text = "Items\n";
        foreach(ShipDefinitions.Item item in myItems)
        {
            text.text += ShipDefinitions.itemToString(item) + "\n";
        }
	}
}
