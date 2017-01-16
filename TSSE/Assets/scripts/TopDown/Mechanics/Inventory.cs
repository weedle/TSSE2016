using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    private List<ShipDefinitions.Item> myInventory = new List<ShipDefinitions.Item>();
    private int[] numOfEachItem = new int[3];
    public GameObject inventoryDisplay;
    public string inventoryType;
    void Start()
    {
        if (inventoryType == "Player")
        {
            print("printing player stuff");
            inventoryDisplay.GetComponent<ItemListHandler>().
                addItem("playerItem1");
            inventoryDisplay.GetComponent<ItemListHandler>().
                addItem("playerItem2");
        }
        {
            if (inventoryType == "Merchant")
            {
                inventoryDisplay.GetComponent<ItemListHandler>().
                    addItem("merchantItem1");
                inventoryDisplay.GetComponent<ItemListHandler>().
                    addItem("merchantItem2");
            }
        }
    }

    public List<ShipDefinitions.Item> getPlayerInventory()
    {
        return myInventory;
    }

    public void addItem(ShipDefinitions.Item item)
    {
        myInventory.Add(item);
    }

    public void removeItem(ShipDefinitions.Item item)
    {
        myInventory.Remove(item);
    }

    public bool findItem(ShipDefinitions.Item item)
    {
        return myInventory.Contains(item);
    }
}
