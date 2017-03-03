using UnityEngine;
using System.Collections.Generic;

// Inventory object for a given entity, Player or Merchant
// we should load and save these things to PlayerPrefs, probably
// well, maybe not merchants, maybe they should respawn merchandise
// I DON'T KNOW YOU FIGURE IT OUT BLAHH
public class Inventory : MonoBehaviour {

    // number of each item we have
    private int[] numOfEachItem = new int[ItemDefinitions.numberOfItemTypes];

    // the two gameobjects that display the inventory and currency of this entity
    public GameObject inventoryDisplay;
    public GameObject currencyDisplay;

    // Player? Merchant? TREASURE CHEST?!
    public string inventoryType;

    // unique identity, to use when loading or saving this inventory from PlayerPrefs
    public string uniqueId;

    // currently selected item
    private ItemDefinitions.Item selected;

    // currency in possession of this entity
    private int currency = 200;

    void Start()
    {
        if (inventoryType == "Player")
        {
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModDamage, 3));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModSpread, 1));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModFireRate, 2));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModDamage, 1));
        }
        if (inventoryType == "Merchant")
        {
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModDamage, 1));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModSpeed, 1));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModFireRate, 2));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModDamage, 1));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModSpeed, 2));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModFireRate, 3));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModDamage, 1));
            addItem(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModDamage, 4));
        }
        updateCurrency();
    }

    // just refresh the label
    private void updateCurrency()
    {
        if (currencyDisplay == null)
            return;
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
    public void addItem(ItemDefinitions.Item item)
    {
        inventoryDisplay.GetComponent<ItemListHandler>().
            addItem(item);

        numOfEachItem[ItemDefinitions.itemToInt(item)] += 1;
    }

    // reduce quantity, remove from list if none are left
    public void removeItem(ItemDefinitions.Item item)
    {
        if (numOfEachItem[ItemDefinitions.itemToInt(item)] == 0)
            return;
        inventoryDisplay.GetComponent<ItemListHandler>().
            removeItem(item);
        numOfEachItem[ItemDefinitions.itemToInt(item)] -= 1;
    }

    // do we have the item? maybeeee
    public bool findItem(ItemDefinitions.Item item)
    {
        return (numOfEachItem[ItemDefinitions.itemToInt(item)] != 0);
    }

    public void saveInventory()
    {
        string invSlotLabel = uniqueId + "InventorySlotLabel";
        for (int i = 0; i < ItemDefinitions.numberOfItemTypes; i++)
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
        for (int i = 0; i < ItemDefinitions.numberOfItemTypes; i++)
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

        for (int i = 0; i < ItemDefinitions.numberOfItemTypes; i++)
        {
            for (int j = 0; j < numOfEachItem[i]; j++)
            {
                print("adding item: " + i + " and num = " + numOfEachItem[i]);
                inventoryDisplay.GetComponent<ItemListHandler>().addItem(ItemDefinitions.intToItem(i));
            }
        }
    }

    public void clearInventory()
    {
        for (int i = 0; i < ItemDefinitions.numberOfItemTypes; i++)
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
