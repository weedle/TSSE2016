using UnityEngine;
using System.Collections.Generic;

// Inventory object for a given entity, Player or Merchant
// we should load and save these things to PlayerPrefs, probably
// well, maybe not merchants, maybe they should respawn merchandise
// I DON'T KNOW YOU FIGURE IT OUT BLAHH
public class Inventory : MonoBehaviour {

    // the list of items in this entity's possession
    private List<ShipDefinitions.Item> myInventory = new List<ShipDefinitions.Item>();

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
            addItem(ShipDefinitions.Item.FlameModDamage);
            addItem(ShipDefinitions.Item.FlameModeSpread);
            addItem(ShipDefinitions.Item.FlameModFireRate);
            addItem(ShipDefinitions.Item.FlameModDamage);
        }
        if (inventoryType == "Merchant")
        {
            addItem(ShipDefinitions.Item.LaserDamage);
            addItem(ShipDefinitions.Item.LaserModeSpeed);
            addItem(ShipDefinitions.Item.LaserModFireRate);
            addItem(ShipDefinitions.Item.LaserDamage);
            addItem(ShipDefinitions.Item.LaserModeSpeed);
            addItem(ShipDefinitions.Item.LaserModFireRate);
        }
        updateCurrency();
    }

    // just refresh the label
    private void updateCurrency()
    {
        currencyDisplay.GetComponent<UnityEngine.UI.Text>().text
            = currency + " QUIDS";
    }

    // returns the list of items
    // it's mostly encapsulated for sake of consistency, we don't really
    // care if people hack this imo
    // it's not multiplayer, right?
    public List<ShipDefinitions.Item> getPlayerInventory()
    {
        return myInventory;
    }

    // add an item, if we already have it just increase the quantity
    public void addItem(ShipDefinitions.Item item)
    {
        myInventory.Add(item);
        inventoryDisplay.GetComponent<ItemListHandler>().
            addItem(item);
        numOfEachItem[ShipDefinitions.itemToInt(item)] += 1;
    }

    // reduce quantity, remove from list if none are left
    public void removeItem(ShipDefinitions.Item item)
    {
        if (numOfEachItem[ShipDefinitions.itemToInt(item)] == 0)
            return;
        myInventory.Remove(item);
        inventoryDisplay.GetComponent<ItemListHandler>().
            removeItem(item);
        numOfEachItem[ShipDefinitions.itemToInt(item)] -= 1;
    }

    // do we have the item? maybeeee
    public bool findItem(ShipDefinitions.Item item)
    {
        return myInventory.Contains(item);
    }

    public void saveInventory(List<ShipDefinitions.Item> toSave)
    {

    }

    public void loadInventory()
    {

    }

    // just use a negative param to remove from currency
    public void addCurrency(int toAdd)
    {
        currency += toAdd;
        updateCurrency();
    }
}
