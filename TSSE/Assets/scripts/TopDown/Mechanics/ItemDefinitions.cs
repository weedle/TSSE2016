using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ItemDefinitions {
    // ITEM DEFINITIONS
    // here we declare all the possible items and set all the data for them

    // Item types
    public enum ItemType
    {
        Error,
        // The Flame weapon module fires a short-range burst of projectiles
        // that dissipate quickly (short life-time). At closer range this 
        // weapon is highly effective.
        FlameModDamage,         // Increases the damage of each projectile.
        FlameModFireRate,       // Increases the number of projectiles per burst.
        FlameModSpread,         // Widens the angle range the projectiles can be shot at.
                                // This results is a wider "spray" of particles.
        FlameModAmmoCap,        // Increases the number of projectiles per clip
        FlameModRechargeRate,   // Increases delay before ammunition is fully replenished
        FlameModSpeed,          // Increases projectile velocity (and as a result, range)
        FlameModRange,          // Increases projectile lifetime (directly increasing range)

        // The Laser weapon fires a small but fast projectile with a longer lifetime 
        // than the flame weapon.
        LaserModDamage,         // Increases the damage of each projectile.
        LaserModFireRate,       // Decreases the delay between each fired projectile.
        LaserModSpeed,          // Increases projectile velocity (and as a result, range)
        LaserModAmmoCap,        // Increases the number of projectiles per clip
        LaserModRechargeRate,   // Increases rate at which ammo is regenerated.
        LaserModRange,          // Increases projectile lifetime (directly increasing range)

        // The Missile weapon fires a small homing missile that deals heavy damage. Clip
        // size is restricted, as is ammunition and firing rate.
        MissileModDamage,       // Increases the damage of each projectile.
        MissileModFireRate,     // Decreases the delay between each fired projectile.
        MissileModAmmoCap,      // Increases rate at which ammo is regenerated.
        MissileModRechargeRate, // Increases rate at which ammo is regenerated.
        MissileModSpeed,        // Increases projectile velocity (and as a result, range)
        MissileModRange,        // Increases projectile lifetime (directly increasing range)

        // The Crown weapon fires a laser at any ship within range. Also close range, no
        // accuracy is required as the weapon only fires if the angle is acceptable and
        // draws a line between the ship and the target, inflicting damage directly.
        CrownModDamage,         // Increases the damage of each shot
        CrownModAmmoCap,        // Increases the number of shots per burst
        CrownModRechargeRate,   // Increases delay before ammunition is fully replenished
        CrownModRange			// Increases maximum distance between ship and target to fire
    }

    // this is if we ever need to iterate through every item
    public static int numberOfItemTypes = 70;

    public static string getDesc(Item item)
    {
        string retStr = "";
        switch (item.type)
        {
            case ItemType.FlameModDamage:
                retStr = " Increases the damage of each projectile";
                break;
            case ItemType.FlameModFireRate:
                retStr = "Increases the number of projectiles per burst";
                break;
            case ItemType.FlameModSpread:
                retStr = "Widens the angle range the projectiles can be shot at";
                // This results is a wider "spray" of particles.
                break;
            case ItemType.FlameModAmmoCap:
                retStr = "Increases the number of projectiles per clip";
                break;
            case ItemType.FlameModRechargeRate:
                retStr = "Increases delay before ammunition is fully replenished";
                break;
            case ItemType.FlameModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case ItemType.FlameModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case ItemType.LaserModDamage:
                retStr = "Increases the damage of each projectile";
                break;
            case ItemType.LaserModFireRate:
                retStr = "Decreases the delay between each fired projectile";
                break;
            case ItemType.LaserModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case ItemType.LaserModAmmoCap:
                retStr = "Increases the number of projectiles per clip";
                break;
            case ItemType.LaserModRechargeRate:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case ItemType.LaserModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case ItemType.MissileModDamage:
                retStr = "Increases the damage of each projectile";
                break;
            case ItemType.MissileModFireRate:
                retStr = "Decreases the delay between each fired projectile";
                break;
            case ItemType.MissileModAmmoCap:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case ItemType.MissileModRechargeRate:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case ItemType.MissileModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case ItemType.MissileModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case ItemType.CrownModDamage:
                retStr = "Increases the damage of each shot";
                break;
            case ItemType.CrownModAmmoCap:
                retStr = "Increases the number of shots per burst";
                break;
            case ItemType.CrownModRechargeRate:
                retStr = "Increases delay before ammunition is fully replenished";
                break;
            case ItemType.CrownModRange:
                retStr = "Increases maximum distance between ship and target to fire";
                break;
        }
        if (item.tier > 3)
        {
            retStr = "BLUEPRINT:\n" + retStr;
        }
        return retStr;
    }

    public static string getSpec(Item item)
    {
        string retStr = "";
        string prefix = "";
        int x = item.tier;
        if(item.tier > 3)
        {
            prefix = "BLUEPRINT:\n";
            x -= 3;
        }
        switch (item.type)
        {
            case ItemType.FlameModDamage:
                switch (x)
                {
                    case 1:
                        retStr = "damage/projectile\n1 -> 1.25";
                        break;
                    case 2:
                        retStr = "damage/projectile\n1 -> 1.5";
                        break;
                    case 3:
                        retStr = "damage/projectile\n1 -> 2";
                        break;
                }
                break;
            case ItemType.FlameModFireRate:
                switch (x)
                {
                    case 1:
                        retStr = "#projectiles\n2 -> 3";
                        break;
                    case 2:
                        retStr = "#projectiles\n2 -> 4";
                        break;
                    case 3:
                        retStr = "#projectiles\n2 -> 7";
                        break;
                }
                break;
            case ItemType.FlameModSpread:
                switch (x)
                {
                    case 1:
                        retStr = "spread\n0.03 -> 0.05";
                        break;
                    case 2:
                        retStr = "spread\n0.03 -> 0.07";
                        break;
                    case 3:
                        retStr = "spread\n0.03 -> 0.09";
                        break;
                }
                break;
            case ItemType.FlameModAmmoCap:
                switch (x)
                {
                    case 1:
                        retStr = "clip size\n15 -> 20";
                        break;
                    case 2:
                        retStr = "clip size\n15 -> 25";
                        break;
                    case 3:
                        retStr = "clip size \n15 -> 30";
                        break;
                }
                break;
            case ItemType.FlameModRechargeRate:
                switch (x)
                {
                    case 1:
                        retStr = "recharge delay\n2 -> 1.75";
                        break;
                    case 2:
                        retStr = "recharge delay\n2 -> 1.5";
                        break;
                    case 3:
                        retStr = "recharge delay\n2 -> 1";
                        break;
                }
                break;
            case ItemType.FlameModSpeed:
                switch (x)
                {
                    case 1:
                        retStr = "recharge rate\n0.25 -> 0.30";
                        break;
                    case 2:
                        retStr = "recharge rate\n0.25 -> 0.40";
                        break;
                    case 3:
                        retStr = "recharge rate\n0.25 -> 0.55";
                        break;
                }
                break;
            case ItemType.FlameModRange:
                switch (x)
                {
                    case 1:
                        retStr = "projectile lifetime\n15 -> 20";
                        break;
                    case 2:
                        retStr = "projectile lifetime\n15 -> 25";
                        break;
                    case 3:
                        retStr = "projectile lifetime\n15 -> 30";
                        break;
                }
                break;
            case ItemType.LaserModDamage:
                switch (x)
                {
                    case 1:
                        retStr = "damage/shot\n12 -> 14";
                        break;
                    case 2:
                        retStr = "damage/shot\n12 -> 16";
                        break;
                    case 3:
                        retStr = "damage/shot\n12 -> 24";
                        break;
                }
                break;
            case ItemType.LaserModFireRate:
                switch (x)
                {
                    case 1:
                        retStr = "fire delay\n0.4 -> 0.3";
                        break;
                    case 2:
                        retStr = "fire delay\n0.4 -> 0.2";
                        break;
                    case 3:
                        retStr = "fire delay\n0.4 -> 0.1";
                        break;
                }
                break;
            case ItemType.LaserModSpeed:
                switch (x)
                {
                    case 1:
                        retStr = "proj speed\n40 -> 50";
                        break;
                    case 2:
                        retStr = "proj speed\n40 -> 60";
                        break;
                    case 3:
                        retStr = "proj speed\n40 -> 70";
                        break;
                }
                break;
            case ItemType.LaserModAmmoCap:
                switch (x)
                {
                    case 1:
                        retStr = "clip size\n4 -> 6";
                        break;
                    case 2:
                        retStr = "clip size\n4 -> 8";
                        break;
                    case 3:
                        retStr = "clip size\n4 -> 10";
                        break;
                }
                break;
            case ItemType.LaserModRechargeRate:
                switch (x)
                {
                    case 1:
                        retStr = "recharge delay\n2 -> 1.5";
                        break;
                    case 2:
                        retStr = "recharge delay\n2 -> 1.0";
                        break;
                    case 3:
                        retStr = "recharge delay\n2 -> 0.5";
                        break;
                }
                break;
            case ItemType.LaserModRange:
                switch (x)
                {
                    case 1:
                        retStr = "projectile lifetime\n25 -> 30";
                        break;
                    case 2:
                        retStr = "projectile lifetime\n25 -> 35";
                        break;
                    case 3:
                        retStr = "projectile lifetime\n25 -> 40";

                        break;
                }
                break;
            case ItemType.MissileModDamage:
                switch (x)
                {
                    case 1:
                        retStr = "damage/shot\n20 -> 30";
                        break;
                    case 2:
                        retStr = "damage/shot\n20 -> 40";
                        break;
                    case 3:
                        retStr = "damage/shot\n20 -> 50";
                        break;
                }
                break;
            case ItemType.MissileModFireRate:
                switch (x)
                {
                    case 1:
                        retStr = "fire delay\n1 -> 0.75";
                        break;
                    case 2:
                        retStr = "fire delay\n1 -> 0.50";
                        break;
                    case 3:
                        retStr = "fire delay\n1 -> 0.25";
                        break;
                }
                break;
            case ItemType.MissileModAmmoCap:
                switch (x)
                {
                    case 1:
                        retStr = "clip size\n2 -> 3";
                        break;
                    case 2:
                        retStr = "clip size\n2 -> 4";
                        break;
                    case 3:
                        retStr = "clip size\n2 -> 7";
                        break;
                }
                break;
            case ItemType.MissileModRechargeRate:
                switch (x)
                {
                    case 1:
                        retStr = "recharge delay\n3 -> 2.5";
                        break;
                    case 2:
                        retStr = "recharge delay\n3 -> 2.0";
                        break;
                    case 3:
                        retStr = "recharge delay\n3 -> 1.5";
                        break;
                }
                break;
            case ItemType.MissileModSpeed:
                switch (x)
                {
                    case 1:
                        retStr = "projectile speed\n3 -> 4";
                        break;
                    case 2:
                        retStr = "projectile speed\n3 -> 5";
                        break;
                    case 3:
                        retStr = "projectile speed\n3 -> 6";
                        break;
                }
                break;
            case ItemType.MissileModRange:
                switch (x)
                {
                    case 1:
                        retStr = "projectile lifetime\n25 -> 35";
                        break;
                    case 2:
                        retStr = "projectile lifetime\n25 -> 45";
                        break;
                    case 3:
                        retStr = "projectile lifetime\n25 -> 60";
                        break;
                }
                break;
            case ItemType.CrownModDamage:
                switch (x)
                {
                    case 1:
                        retStr = "damage/shot\n2 -> 3";
                        break;
                    case 2:
                        retStr = "damage/shot\n2 -> 4";
                        break;
                    case 3:
                        retStr = "damage/shot\n2 -> 6";
                        break;
                }
                break;
            case ItemType.CrownModAmmoCap:
                switch (x)
                {
                    case 1:
                        retStr = "clip size\n15 -> 20";
                        break;
                    case 2:
                        retStr = "clip size\n15 -> 25";
                        break;
                    case 3:
                        retStr = "clip size\n15 -> 30";
                        break;
                }
                break;
            case ItemType.CrownModRechargeRate:
                switch (x)
                {
                    case 1:
                        retStr = "recharge delay\n3 -> 2.5";
                        break;
                    case 2:
                        retStr = "recharge delay\n3 -> 2";
                        break;
                    case 3:
                        retStr = "recharge delay\n3 -> 1";
                        break;
                }
                break;
            case ItemType.CrownModRange:
                // range is 2 by default
                // range is 2.5 with tier 1 upgrade
                // range is 3 with tier 2 upgrade
                // range is 4 with tier 3 upgrade
                switch (x)
                {
                    case 1:
                        retStr = "range\n2 -> 2.5";
                        break;
                    case 2:
                        retStr = "range\n2 -> 3";
                        break;
                    case 3:
                        retStr = "range\n2 -> 4";
                        break;
                }
                break;
        }
        retStr = prefix + retStr;
        return retStr;
    }

    public struct Item
    {
        public ItemType type;
        public int tier;
        public Item(ItemType type, int tier)
        {
            this.type = type;
            this.tier = tier;
        }
        public Item(ItemType type)
        {
            this.type = type;
            this.tier = 1;
        }
    }

    // Used for display purposes
    public static string itemToString(Item item)
    {
        return item.type.ToString() + ":" + item.tier;
    }

    // Used to figure out what item an object is displaying
    public static Item stringToItem(String str)
    {
        ItemType itemType;

        string[] split = str.Split(':');
        try
        {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), split[0]);
        }
        catch (ArgumentException err)
        {
            itemType = ItemType.Error;
        }

        int tier = 0;
        if (split.Length == 2)
            tier = int.Parse(split[1]);
        return new Item(itemType, tier);
    }

    // used for indexing item quantities
    public static int itemToInt(Item item)
    {
        if (item.type == ItemType.Error)
            return 0;
        return (3 * (int)item.type) - 2 + item.tier;
    }

    public static Item intToItem(int index)
    {
        if (index == 0)
            return new Item(ItemType.Error);
        Item item;
        try
        {
            ItemType type = (ItemType)Enum.Parse(typeof(ItemType),
            (index / 3).ToString());
            int tier = index % 3 + 1;
            item = new Item(type, tier);
        }
        catch (ArgumentException err)
        {
            item = new Item(ItemType.Error);
        }
        return item;
    }

    // Convert an encoded string into a list of items
    public static List<ItemDefinitions.Item> stringToItems(string itemsEncoded)
    {
        List<ItemDefinitions.Item> items = new List<Item>();

        string[] itemsSeparated = itemsEncoded.Split('#');

        foreach(string itemEncoded in itemsSeparated)
        {
            items.Add(stringToItem(itemEncoded));
        }

        return items;
    }

    // convert a list of items into an encoded string
    public static string itemsToString(List<Item> items)
    {
        string ret = "";

        foreach(Item item in items)
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
        return PlayerPrefs.GetString("TSSEList[Item][" + uniqueId + "]");
    }

    // This is how we get the price value when buying or selling something
    // Item is the item to be bought or sold, inventoryId is something like
    // "Player" or "MerchantXYZ", and buying is true if we want a purchase 
    // price, and false if we want a selling price
    public static int getCost(Item item, String inventoryId, bool buying)
    {
        // Here we cleverly utilize all three parameters to determine a 
        // dynamic price
        return 10;
        // wow hardcoded placeholder value, much clever
    }
}
