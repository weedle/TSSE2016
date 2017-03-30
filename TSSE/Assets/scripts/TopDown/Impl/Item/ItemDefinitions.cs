using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ItemDefinitions {
    // ITEM DEFINITIONS
    // This encompasses the various types of items we may encounter

    // this is if we ever need to iterate through every item
    //public static int numberOfItemTypes = 70;

    public static string getDesc(ItemAbstract item)
    {
        return item.getDesc();
    }

    public static string getSpec(ItemAbstract item)
    {
        return item.getSpec();
    }

    // Used for display purposes
    public static string itemToString(ItemAbstract item)
    {
        return item.getType() + ":" + item.getTier();
    }

    // Used to figure out what item an object is displaying
    public static ItemAbstract stringToItem(String str)
    {
        string[] split = str.Split(':');

        int tier = 0;
        if (split.Length == 2)
            tier = int.Parse(split[1]);
        return ItemAbstract.newItem(split[0], tier);
    }

    // used for indexing item quantities
    public static int itemToInt(ItemAbstract item)
    {
        if (WeaponItem.isWeaponType(item.getType()))
        {
            WeaponItem itemW = (WeaponItem) item;
            if (itemW.type == WeaponItem.WeaponType.Error)
                return 0;
            return (6 * (int) itemW.type) - 5 + itemW.tier;
        }
        else if (EngineItem.isEngineType(item.getType()))
        {
            EngineItem itemE = (EngineItem)item;
            if (itemE.type == EngineItem.EngineType.Error)
                return 0;
            return (6 * (int)itemE.type) - 5 + itemE.tier + WeaponItem.numWeaponTypes;
        }
        return 0;
    }

    public static ItemAbstract intToItem(int index)
    {
        if (index < WeaponItem.numWeaponTypes)
        {
            if (index == 0)
                return new WeaponItem(WeaponItem.WeaponType.Error);
            WeaponItem item;
            try
            {
                WeaponItem.WeaponType type = (WeaponItem.WeaponType)Enum.Parse(typeof(WeaponItem.WeaponType),
                (index / 6).ToString());
                int tier = index % 6 + 1;
                item = new WeaponItem(type, tier);
            }
            catch (ArgumentException err)
            {
                item = new WeaponItem(WeaponItem.WeaponType.Error);
            }
            return item;
        }
        else if (index <= WeaponItem.numWeaponTypes + EngineItem.numEngineTypes)
        {
            index -= WeaponItem.numWeaponTypes;
            if (index == 0)
                return new EngineItem(EngineItem.EngineType.Error);
            EngineItem item;
            try
            {
                EngineItem.EngineType type = (EngineItem.EngineType)Enum.Parse(typeof(EngineItem.EngineType),
                (index / 6).ToString());
                int tier = index % 6 + 1;
                item = new EngineItem(type, tier);
            }
            catch (ArgumentException err)
            {
                item = new EngineItem(EngineItem.EngineType.Error);
            }
            return item;
        }
        return new WeaponItem(WeaponItem.WeaponType.Error);
    }

    // Convert an encoded string into a list of items
    public static List<ItemAbstract> stringToItems(string itemsEncoded)
    {
        List<ItemAbstract> items = new List<ItemAbstract>();

        string[] itemsSeparated = itemsEncoded.Split('#');

        foreach(string itemEncoded in itemsSeparated)
        {
            items.Add(stringToItem(itemEncoded));
        }

        return items;
    }

    // convert a list of items into an encoded string
    public static string itemsToString(List<ItemAbstract> items)
    {
        string ret = "";

        foreach(ItemAbstract item in items)
        {
            ret = ret + "#" + itemToString(item);
        }

        return ret;
    }
    // save a string of items to PlayerPrefs
    public static void saveItems(string uniqueId, string itemsEncoded)
    {
        //TSSEList[Type][UniqueId]
        PlayerPrefs.SetString("TSSEList[Item][" + uniqueId + "]", itemsEncoded);
    }

    // load a string of items from PlayerPrefs
    public static string loadItems(string uniqueId)
    {
        return PlayerPrefs.GetString("TSSEList[Item][" + uniqueId + "]").TrimStart('#');
    }

    // This is how we get the price value when buying or selling something
    // Item is the item to be bought or sold, inventoryId is something like
    // "Player" or "MerchantXYZ", and buying is true if we want a purchase 
    // price, and false if we want a selling price
    public static int getCost(ItemAbstract item, String inventoryId, bool buying)
    {
        // Here we cleverly utilize all three parameters to determine a 
        // dynamic price
        return 10;
        // wow hardcoded placeholder value, much clever
    }
}
