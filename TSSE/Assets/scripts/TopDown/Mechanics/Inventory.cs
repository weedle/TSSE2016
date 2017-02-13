﻿using UnityEngine;
using System.Collections.Generic;

// Inventory object for a given entity, Player or Merchant
// we should load and save these things to PlayerPrefs, probably
// well, maybe not merchants, maybe they should respawn merchandise
// I DON'T KNOW YOU FIGURE IT OUT BLAHH
public class Inventory : MonoBehaviour {

    // number of each item we have
    private int[] numOfEachItem = new int[ShipDefinitions.numberOfItemTypes];

    // the two gameobjects that display the inventory and currency of this entity
    public GameObject inventoryDisplay;
    public GameObject currencyDisplay;

    // Player? Merchant? TREASURE CHEST?!
    public string inventoryType;

    // unique identity, to use when loading or saving this inventory from PlayerPrefs
    public string uniqueId;

    // currently selected item
    private ShipDefinitions.Item selected;

    // currency in possession of this entity
    private int currency = 200;

    void Start()
    {
        if (inventoryType == "Player")
        {  
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModDamage));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModSpread));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModFireRate));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.FlameModDamage));
        }
        if (inventoryType == "Merchant")
        {
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModDamage));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModSpeed));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModFireRate));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModDamage));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModSpeed));
            addItem(new ShipDefinitions.Item(ShipDefinitions.ItemType.LaserModFireRate));
        }
        updateCurrency();
    }

    // just refresh the label
    private void updateCurrency()
    {
        currencyDisplay.GetComponent<UnityEngine.UI.Text>().text
            = uniqueId + ": " + currency + " QUIDS";
    }

    // returns the list of items
    // it's mostly encapsulated for sake of consistency, we don't really
    // care if people hack this imo
    // it's not multiplayer, right?
    public int[] getPlayerInventory()
    {
        return numOfEachItem;
    }

    // add an item, if we already have it just increase the quantity
    public void addItem(ShipDefinitions.Item item)
    {
        inventoryDisplay.GetComponent<ItemListHandler>().
            addItem(item);
        numOfEachItem[ShipDefinitions.itemToInt(item)] += 1;
    }

    // reduce quantity, remove from list if none are left
    public void removeItem(ShipDefinitions.Item item)
    {
        if (numOfEachItem[ShipDefinitions.itemToInt(item)] == 0)
            return;
        inventoryDisplay.GetComponent<ItemListHandler>().
            removeItem(item);
        numOfEachItem[ShipDefinitions.itemToInt(item)] -= 1;
    }

    // do we have the item? maybeeee
    public bool findItem(ShipDefinitions.Item item)
    {
        return (numOfEachItem[ShipDefinitions.itemToInt(item)] != 0);
    }

    public void saveInventory()
    {
        string invSlotLabel = uniqueId + "InventorySlotLabel";
        for (int i = 0; i < ShipDefinitions.numberOfItemTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            print("Saving item: " + i + " and num = " + numOfEachItem[i]);
            UnityEngine.PlayerPrefs.SetInt(currLabel, numOfEachItem[i]);
        }
    }

    public void loadInventory()
    {
        clearInventory();
        string invSlotLabel = uniqueId + "InventorySlotLabel";
        for (int i = 0; i < ShipDefinitions.numberOfItemTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            numOfEachItem[i] = 0;
            if (UnityEngine.PlayerPrefs.HasKey(currLabel))
            {
                numOfEachItem[i] = UnityEngine.PlayerPrefs.GetInt(currLabel);
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < ShipDefinitions.numberOfItemTypes; i++)
        {
            for (int j = 0; j < numOfEachItem[i]; j++)
            {
                print("adding item: " + i + " and num = " + numOfEachItem[i]);
                inventoryDisplay.GetComponent<ItemListHandler>().addItem(ShipDefinitions.intToItem(i));
            }
        }
    }

    public void clearInventory()
    {
        for (int i = 0; i < ShipDefinitions.numberOfItemTypes; i++)
        {
                numOfEachItem[i] = 0;
        }
        inventoryDisplay.GetComponent<ItemListHandler>().removeAllItems();
    }

    // just use a negative param to remove from currency
    public void addCurrency(int toAdd)
    {
        currency += toAdd;
        updateCurrency();
    }
}