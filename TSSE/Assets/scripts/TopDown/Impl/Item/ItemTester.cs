using UnityEngine;
using System.Collections.Generic;
using System;

public class ItemTester : MonoBehaviour {
    private List<ItemAbstract> myItems;
    private List<ShipDefinitions.ShipEntity> myShips;
    bool once = true;
    // Use this for initialization
    void Start () {
        /*
        myItems = new List<ShipDefinitions.Item>();
        myItems.Add(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModDamage));
        myItems.Add(ShipDefinitions.stringToItem(
            ShipDefinitions.itemToString(new ShipDefinitions.Item(ShipDefinitions.ItemType.CrownModDamage))));
        myItems.Add(ShipDefinitions.intToItem(
            ShipDefinitions.itemToInt(
                new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModSpread))));
        */

        
        myShips = new List<ShipDefinitions.ShipEntity>();

        myItems = new List<ItemAbstract>();

        myShips.Add(new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine1,
            ShipDefinitions.WeaponType.Flame, ShipDefinitions.ShipType.Ruby,
            myItems, ShipDefinitions.Faction.Player, "shipX"));
        myShips.Add(new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine2,
            ShipDefinitions.WeaponType.Crown, ShipDefinitions.ShipType.Peacock,
            myItems, ShipDefinitions.Faction.Enemy, "shipY"));

        ShipDefinitions.saveShips(myShips);
    }
	
	// Update is called once per frame
	void Update () {
        /*
        UnityEngine.UI.Text text = GetComponent<UnityEngine.UI.Text>();
        text.text = "Items\n";
        foreach(ShipDefinitions.Item item in myItems)
        {
            text.text += ShipDefinitions.itemToString(item) + "\n";
        }
        */
        /*
        if (once)
        {
            List<String> ships = new List<string>();
            ships.Add("shipX");
            ships.Add("shipY");

            myShips = ShipDefinitions.loadShips(ships);

            foreach (ShipDefinitions.ShipEntity entity in myShips)
            {
                GameObject.Find("GameLogic").GetComponent<ShipSpawner>().spawnShip(entity);
            }
            once = false;
        }
        */

    }
}
