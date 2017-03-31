using UnityEngine;
using System.Collections.Generic;

// Inventory object for a given entity, Player or Merchant
// we should load and save these things to PlayerPrefs, probably
// well, maybe not merchants, maybe they should respawn merchandise
// I DON'T KNOW YOU FIGURE IT OUT BLAHH
public class Inventory : MonoBehaviour {

    // number of each item we have
    //private int[] numOfEachWeapItem = new int[WeaponItem.numWeaponTypes];
    //private int[] numOfEachEngItem = new int[EngineItem.numEngineTypes];

    private Dictionary<string, int> inventory;

    // the two gameobjects that display the inventory and currency of this entity
    public GameObject inventoryDisplay;
    public GameObject blueprintDisplay;
    public GameObject currencyDisplay;
    public GameObject shipDisplay;

    // Player? Merchant? TREASURE CHEST?!
    public string inventoryType;

    // unique identity, to use when loading or saving this inventory from PlayerPrefs
    public string uniqueId;

    // currently selected item
    private ItemAbstract selected;

    // currency in possession of this entity
    private int currency = 200;

    public bool engines = true;
    public bool engineBlueprints = false;
    public bool weapons = true;
    public bool weaponBlueprints = false;

    void Start()
    {
        if(inventoryType == "Player")
            currency = PlayerPrefs.GetInt("score");
        else
            currency = PlayerPrefs.GetInt("score-" + inventoryType);
        //PlayerPrefs.DeleteAll();
        List<ItemAbstract> items = ItemDefinitions.stringToItems(
        ItemDefinitions.loadItems(uniqueId));
        /*
        if(inventoryType == "Merchant")
        {
            items = new List<ItemAbstract>();
            items.Add(new EngineItem(EngineItem.EngineType.Spinjet, 4));

            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 4));
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModRange, 4));
            items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModRechargeRate, 4));
            items.Add(new WeaponItem(WeaponItem.WeaponType.MissileModAmmoCap, 4));
            items.Add(new WeaponItem(WeaponItem.WeaponType.MissileModAmmoCap, 5));
        }
        else if(inventoryType == "Player")
        {
            items = new List<ItemAbstract>();
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 1));
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 2));
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 4));
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModRange, 2));
            items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModRechargeRate, 3));
            items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModAmmoCap, 2));
            items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModAmmoCap, 2));
            items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModRechargeRate, 3));
            items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModRechargeRate, 1));
        }
        /*
        List<ItemAbstract> items = new List<ItemAbstract>();
        // hardcoding temporary item list
        items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 1));
        items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModAmmoCap, 2));
        items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModRange, 2));
        items.Add(new WeaponItem(WeaponItem.WeaponType.CrownModRechargeRate, 3));
        items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModAmmoCap, 2));
        items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModAmmoCap, 2));
        items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModRechargeRate, 3));
        items.Add(new WeaponItem(WeaponItem.WeaponType.FlameModRechargeRate, 1));
        */
        clearInventory();
        foreach (ItemAbstract item in items)
        {
            if (item.getType() != "Error")
            {
                print("adding " + item);
                //addItem(item);
                inventory[ItemDefinitions.itemToString(item)]++;
                if(item.getTier() > 3)
                {
                    if (blueprintDisplay != null)
                    {
                        blueprintDisplay.GetComponent<ItemListHandler>().
                            addItem(item);
                    }
                }
            }
        }
        saveInventory();
        //ItemDefinitions.saveItems("Player", ItemDefinitions.itemsToString(items));
        if (shipDisplay != null)
        {
            for (int i = 0; i < 6; i++)
            {
                string id = "PlayerShip" + i.ToString();
                shipDisplay.GetComponent<ShipListHandler>().addShip(ShipDefinitions.loadShip(id));
            }
        }
        reloadItems();
        updateCurrency();
    }

    public void toggleEngines()
    {
        engines = !engines;
        reloadItems();
    }

    public void toggleEngineBlueprints()
    {
        engineBlueprints = !engineBlueprints;
        reloadItems();
    }

    public void toggleWeapons()
    {
        weapons = !weapons;
        reloadItems();
    }

    public void toggleWeaponBlueprints()
    {
        weaponBlueprints = !weaponBlueprints;
        reloadItems();
    }

    public void reloadItems()
    {
        print(engines + " " + weapons + " " + engineBlueprints + " " + weaponBlueprints);
        inventoryDisplay.GetComponent<ItemListHandler>().removeAllItems();
        foreach (string key in inventory.Keys)
        {
            if (inventory[key] <= 0)
                continue;
            ItemAbstract item = ItemDefinitions.stringToItem(key);
            print(key + "->" + item.getType() + ":" + item.getTier());
            if (item.getType() == "Error")
                continue;
            if(WeaponItem.isWeaponType(item.getType()))
            {
                if(item.getTier() < 4)
                {
                    if (weapons)
                        addNumItems(item, inventory[key]);
                }
                else
                {
                    if (weaponBlueprints)
                        addNumItems(item, inventory[key]);
                }
            }
            else if(EngineItem.isEngineType(item.getType()))
            {
                if (item.getTier() < 4)
                {
                    if(engines)
                        addNumItems(item, inventory[key]);
                }
                else
                {
                    if(engineBlueprints)
                        addNumItems(item, inventory[key]);
                }
            }
        }
    }

    // just refresh the label
    public void updateCurrency()
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
    /*
    public int[] getPlayerWeapInventory()
    {
        return numOfEachWeapItem;
    }
    public int[] getPlayerEngInventory()
    {
        return numOfEachEngItem;
    }
    */
    // add an item, if we already have it just increase the quantity
    public void addItem(ItemAbstract item)
    {
        if (item.getType() == "Error")
            return;
        //print("inv adding: " + item.getType() + " " + item.getTier());
        inventoryDisplay.GetComponent<ItemListHandler>().
            addItem(item);
        if(item.getTier() > 3)
        {
            if(blueprintDisplay!= null)
            {
                blueprintDisplay.GetComponent<ItemListHandler>().
                    addItem(item);
            }
        }
        /*
        if (WeaponItem.isWeaponType(item.getType()))
        {
            numOfEachWeapItem[ItemDefinitions.itemToInt(item)] += 1;
        }
        else if(EngineItem.isEngineType(item.getType()))
        {
            numOfEachEngItem[ItemDefinitions.itemToInt(item)] += 1;
        }
        */
    }

    public void addNumItems(ItemAbstract item, int num)
    {
        //if(num > 0)
        //    print("adding item: " + num + "of " + item.getType() + ":" + item.getTier());
        for (int i = 0; i < num; i++)
        {
            addItem(item);
        }
    }



    public void addShip(ShipDefinitions.ShipEntity entity)
    {
        shipDisplay.GetComponent<ShipListHandler>().
            addShip(entity);
    }

    // reduce quantity, remove from list if none are left
    public void removeItem(ItemAbstract item)
    {
        /*
        if (WeaponItem.isWeaponType(item.getType()))
        {
            if (numOfEachWeapItem[ItemDefinitions.itemToInt(item)] == 0)
                return;
            numOfEachWeapItem[ItemDefinitions.itemToInt(item)] -= 1;
        }
        else if (EngineItem.isEngineType(item.getType()))
        {
            if (numOfEachEngItem[ItemDefinitions.itemToInt(item)] == 0)
                return;
            numOfEachEngItem[ItemDefinitions.itemToInt(item)] -= 1;
        }
        */
        inventory[ItemDefinitions.itemToString(item)]--;
        inventoryDisplay.GetComponent<ItemListHandler>().
            removeItem(item);
        if (item.getTier() > 3)
        {
            if (blueprintDisplay != null)
            {
                blueprintDisplay.GetComponent<ItemListHandler>().
                    removeItem(item);
            }
        }
    }

    // reduce quantity, remove from list if none are left
    public void removeShip(ShipDefinitions.ShipEntity entity)
    {
        inventoryDisplay.GetComponent<ShipListHandler>().
            removeShip(entity);
    }

    // do we have the item? maybeeee
    public bool findItem(ItemAbstract item)
    {
        /*
        if(WeaponItem.isWeaponType(item.getType()))
        {
            return (numOfEachWeapItem[ItemDefinitions.itemToInt(item)] != 0);
        }
        else if(EngineItem.isEngineType(item.getType()))
        {
            return (numOfEachEngItem[ItemDefinitions.itemToInt(item)] != 0);
        }
        */
        if(inventory[ItemDefinitions.itemToString(item)] > 0)
        {
            return true;
        }
        return false;
    }

    public void saveInventory()
    {
        List<ItemAbstract> items = new List<ItemAbstract>();
        foreach(string key in inventory.Keys)
        {
            ItemAbstract item = ItemDefinitions.stringToItem(key);
            for(int i = 0; i < inventory[key]; i++)
            {
                items.Add(item);
            }
        }
        ItemDefinitions.saveItems(uniqueId, ItemDefinitions.itemsToString(items));
        /*
        string invSlotLabel = uniqueId + "InventorySlotLabel[Weapon]";
        for (int i = 0; i < WeaponItem.numWeaponTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            print("Saving weapon item: " + i + " and num = " + numOfEachWeapItem[i]);
            UnityEngine.PlayerPrefs.SetInt(currLabel, numOfEachWeapItem[i]);
        }
        invSlotLabel = uniqueId + "InventorySlotLabel[Engine]";
        for (int i = 0; i < EngineItem.numEngineTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            print("Saving engine item: " + i + " and num = " + numOfEachEngItem[i]);
            UnityEngine.PlayerPrefs.SetInt(currLabel, numOfEachEngItem[i]);
        }
        */
    }

    public void loadInventory()
    {
        clearInventory();
        List<ItemAbstract> items = ItemDefinitions.stringToItems(
            ItemDefinitions.loadItems(uniqueId));
        foreach(ItemAbstract item in items)
        {
            addItem(item);
            inventory[ItemDefinitions.itemToString(item)]++;
        }
        /*
        string invSlotLabel = uniqueId + "InventorySlotLabel[Weapon]";
        for (int i = 0; i < WeaponItem.numWeaponTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            numOfEachWeapItem[i] = 0;
            if (UnityEngine.PlayerPrefs.HasKey(currLabel))
            {
                numOfEachWeapItem[i] = UnityEngine.PlayerPrefs.GetInt(currLabel);
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < WeaponItem.numWeaponTypes; i++)
        {
            for (int j = 0; j < numOfEachWeapItem[i]; j++)
            {
                print("adding weapon item: " + i + " and num = " + numOfEachWeapItem[i]);
                inventoryDisplay.GetComponent<ItemListHandler>().addItem(ItemDefinitions.intToItem(i));
            }
        }

        invSlotLabel = uniqueId + "InventorySlotLabel[Engine]";
        for (int i = 0; i < EngineItem.numEngineTypes; i++)
        {
            string currLabel = invSlotLabel + i.ToString();
            numOfEachEngItem[i] = 0;
            if (UnityEngine.PlayerPrefs.HasKey(currLabel))
            {
                numOfEachEngItem[i] = UnityEngine.PlayerPrefs.GetInt(currLabel);
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < EngineItem.numEngineTypes; i++)
        {
            if (i == 0)
                print("adding engine item: " + i + " and num = " + numOfEachEngItem[i]);
            for (int j = 0; j < numOfEachEngItem[i]; j++)
            {
                inventoryDisplay.GetComponent<ItemListHandler>().addItem(ItemDefinitions.intToItem(i));
            }
        }
        */
    }

    public void clearInventory()
    {
        inventory = new Dictionary<string, int>();
        for (int i = 0; i < WeaponItem.numWeaponTypes; i++)
        {
            ItemAbstract item = ItemDefinitions.intToItem(i);
            inventory[ItemDefinitions.itemToString(item)] = 0;
        }
        for (int i = 0; i < EngineItem.numEngineTypes; i++)
        {
            ItemAbstract item = ItemDefinitions.intToItem(i + WeaponItem.numWeaponTypes);
            inventory[ItemDefinitions.itemToString(item)] = 0;
        }
        inventoryDisplay.GetComponent<ItemListHandler>().removeAllItems();
    }

    // just use a negative param to remove from currency
    public void addCurrency(int toAdd)
    {
        currency += toAdd;
        updateCurrency();
    }

    public void saveCurrency()
    {
        if (inventoryType == "Player")
            PlayerPrefs.SetInt("score", currency);
        else
            PlayerPrefs.SetInt("score-" + inventoryType, currency);
    }
}
