using UnityEngine;
using System;
using System.Collections.Generic;



/*
 * - currently used to define necessary settings, states,
 *   and/or features that are part of a ship
 * 
 */
public class ShipDefinitions
{
    static string[] names = new string[] {
        "Alice", "Bob", "Carol", "Daisy", "Eliza",
        "Fern", "Greg", "Harry", "Iris", "Jenne",
        "Jen", "Jo", "Kevin", "Lucy", "Mark",
        "Nancy", "Orpheus", "Pete", "Quark", "Steph"};
    // SHIP DEFINITIONS

    public enum SState
    {
        Inactive, Searching, Aiming, Firing, Cooling
    }

    // General rules for interactions between Factions
    // Player and PlayerAffil will target Enemy and Rogue
    // Enemy will target Player and PlayerAffil
    // Rogue will fire on all targets except possibly other Rogues
    // Indep will fire on no targets unless fired upon
    // Relationships are entity-dependent, and can be dynamically
    // assigned during gameplay
    public enum Faction
    {
        Player, PlayerAffil, Enemy, Rogue, Indep
    }

    // return cursor position in world spacec
    public static Vector3 getCursor()
    {
        Vector3 v3 = Input.mousePosition;
        v3.z = -Camera.main.transform.position.z;
        v3 = Camera.main.ScreenToWorldPoint(v3);

        return v3;
    }

    // Draw a line! Parameters are self-explanatory
    // IT'S SELF-DOCUMENTING CODE :DDD
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f, float width = 0.075f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(width, width);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        if(duration != 0)
            GameObject.Destroy(myLine, duration);
    }

    // Use the DrawLine method to draw a square
    public static void DrawSquare(Vector3 bottomLeft, Vector3 topRight, Color color, float duration = 0.2f, float width = 0.075f)
    {
        Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y);
        Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y);
        ShipDefinitions.DrawLine(bottomLeft, bottomRight, color, duration, width);
        ShipDefinitions.DrawLine(bottomLeft, topLeft, color, duration, width);
        ShipDefinitions.DrawLine(topRight, bottomRight, color, duration, width);
        ShipDefinitions.DrawLine(topRight, topLeft, color, duration, width);
    }

    // find quickest path for thing at angle1 to reach angle2
    // if true, turn clockwise, otherwise turn counterclockwise
    public static bool quickestRotation(float angle1, float angle2)
    {
        if (angle1 > 180)
        {
            if (angle2 > angle1 ||
                (angle2 < angle1 - 180))
                return false;
            else
                return true;
        }
        else
        {
            if (angle2 > angle1 &&
                (angle2 < angle1 + 180))
                return false;
            else
                return true;
        }
    }

    // You know, there has to be a better way to use Tags as Factions
    public static Faction stringToFaction(String str)
    {
        switch (str)
        {
            case "Enemy":
                return Faction.Enemy;
            case "Indep":
                return Faction.Indep;
            case "Player":
                return Faction.Player;
            case "PlayerAffil":
                return Faction.PlayerAffil;
            case "Rogue":
                return Faction.Rogue;
            default:
                return Faction.Player;
        }
    }

    // Randomly generate a name for a ship
    // This will be in the format of nameXXXX, where X are digits
    public static string generateName()
    {
        int x = (int)Math.Floor((double)UnityEngine.Random.Range(1, 3000));
        string prefix = names[(int)Math.Floor((double)UnityEngine.Random.Range(0, names.Length))];
        if (x > 999)
            return prefix + x.ToString();
        else if (x > 99)
            return prefix + "0" + x.ToString();
        else
            return prefix + "00" + x.ToString();
    }

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

    public static string getDesc(Item item)
    {
        string retStr = "";
        switch(item.type)
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
        return retStr;
    }

    public static string getSpec(Item item)
    {
        string retStr = "";
        switch (item.type)
        {
            case ItemType.FlameModDamage:
                switch(item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
                switch (item.tier)
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
        return (3 * (int) item.type) - 2 + item.tier;
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
    // this is if we ever need to iterate through every item
    public static int numberOfItemTypes = 70;

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

    public enum EngineType
    {
        Engine1, Engine2, None
    }

    public enum WeaponType
    {
        Crown, Laser, Flame, Missile, None
    }

    public enum ShipType
    {
        Ruby, Peacock, None
    }

    public struct ShipEntity
    {
        public EngineType engType;
        public WeaponType weapType;
        public ShipType shipType;
        public Faction faction;
        public string uniqueId;

        public ShipEntity(EngineType newEng, WeaponType newWeap,
            ShipType newShip, Faction newFaction, string newId)
        {
            engType = newEng;
            weapType = newWeap;
            shipType = newShip;
            faction = newFaction;
            uniqueId = newId;
        }
    }

    public static void saveShip(ShipEntity entity, int index)
    {
        string shipDetails = "";
        shipDetails += (int)entity.engType + ":";
        shipDetails += (int)entity.shipType + ":";
        shipDetails += (int)entity.weapType + ":";
        shipDetails += (int)entity.faction;

        PlayerPrefs.SetString(
            entity.uniqueId + "[" + index + "]" + "ship", shipDetails);
    }

    public static void saveShips(List<ShipEntity> entities)
    {
        int index = 0;
        foreach (ShipEntity entity in entities)
        {
            saveShip(entity, index);
            index++;
        }
    }

    public static ShipEntity loadShip(string uniqueId, int index)
    {
        ShipEntity entity = new ShipEntity();

        string ship = PlayerPrefs.GetString(
            uniqueId + "[" + index + "]" + "ship");
        string[] shipDetails = ship.Split(':');

        EngineType engType = (EngineType)Enum.Parse(
            typeof(EngineType),
            shipDetails[0].ToString());

        ShipType shipType = (ShipType)Enum.Parse(
            typeof(ShipDefinitions.ShipType),
            shipDetails[1].ToString());

        WeaponType weapType = (WeaponType)Enum.Parse(
            typeof(WeaponType),
            shipDetails[2].ToString());

        Faction faction = (Faction)Enum.Parse(
            typeof(Faction),
            shipDetails[3].ToString());

        entity.engType = engType;
        entity.shipType = shipType;
        entity.weapType = weapType;
        entity.faction = faction;
        entity.uniqueId = uniqueId;

        return entity;
    }
    public static List<ShipEntity> loadShips(List<String> entityIds)
    {
        int index = 0;
        List<ShipEntity> entities = new List<ShipEntity>();
        foreach (String entityId in entityIds)
        {
            ShipEntity entity = new ShipEntity();
            entity = loadShip(entityId, index);
            entities.Add(entity);
            index++;
        }
        return entities;
    }
}
