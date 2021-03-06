﻿using UnityEngine;
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
        public List<ItemAbstract> items;

        public ShipEntity(EngineType newEng, WeaponType newWeap,
            ShipType newShip, List<ItemAbstract> newItems, 
            Faction newFaction, string newId)
        {
            engType = newEng;
            weapType = newWeap;
            shipType = newShip;
            items = newItems;
            faction = newFaction;
            uniqueId = newId;
        }
    }

    // Encode all aspects of the ship into a string except equipped items
    // This is because we plan to save those separately
    public static string shipToString(ShipEntity entity)
    {
        string shipDetails = "";
        
        shipDetails += (int)entity.engType + ":";
        shipDetails += (int)entity.weapType + ":";
        shipDetails += (int)entity.shipType + ":";
        shipDetails += (int)entity.faction;

        //ItemDefinitions.saveItems(entity.uniqueId, ItemDefinitions.itemsToString(entity.items));

        return shipDetails;
    }

    public static ShipEntity stringtoShip(string shipEncoded)
    {
        ShipEntity entity = new ShipEntity();
        if(shipEncoded == "")
        {
            entity.engType = EngineType.None;
            entity.weapType = WeaponType.None;
            entity.shipType = ShipType.None;
            return entity;
        }
        //GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(shipEncoded);
        string[] shipDetails = shipEncoded.Split(':');
        EngineType engType = (EngineType)Enum.Parse(
            typeof(EngineType),
            shipDetails[0].ToString());

        WeaponType weapType = (WeaponType)Enum.Parse(
            typeof(WeaponType),
            shipDetails[1].ToString());

        ShipType shipType = (ShipType)Enum.Parse(
            typeof(ShipDefinitions.ShipType),
            shipDetails[2].ToString());

        Faction faction = (Faction)Enum.Parse(
            typeof(Faction),
            shipDetails[3].ToString());

        entity.engType = engType;
        entity.weapType = weapType;
        entity.shipType = shipType;
        entity.faction = faction;

        return entity;
    }

    public static void saveShip(ShipEntity entity)
    {
        PlayerPrefs.SetString("TSSE[ShipEntity][" + entity.uniqueId + "]", shipToString(entity));
        PlayerPrefs.SetString("TSSEList[Item][" + entity.uniqueId + "]", ItemDefinitions.itemsToString(entity.items));

        //GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(
        //    "TSSEList[Item][" + entity.uniqueId + "]");
    }

    public static ShipEntity loadShip(string uniqueId)
    {
        //GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(uniqueId);
        ShipEntity entity = stringtoShip(
            PlayerPrefs.GetString("TSSE[ShipEntity][" + uniqueId + "]"));
        entity.uniqueId = uniqueId;
        if (entity.shipType == ShipType.None)
        {
            return entity;
        }
        entity.items = ItemDefinitions.stringToItems(
            PlayerPrefs.GetString("TSSEList[Item][" + entity.uniqueId + "]"));
        GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(
            ItemDefinitions.itemsToString(entity.items));
        
        return entity;
    }

    public static string shipsToEntityIds(List<ShipEntity> entities)
    {
        string ids = "";

        foreach(ShipEntity entity in entities)
        {
            ids = ids + entity.uniqueId + "#";
        }
        ids = ids.Substring(0, ids.Length - 1);
        //GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(ids);
        return ids;
    }

    public static void saveShips(List<ShipEntity> entities)
    {
        foreach(ShipEntity entity in entities)
        {
            saveShip(entity);
        }
    }

    public static List<ShipEntity> loadShips(List<string> entityIds)
    {
        List<ShipEntity> ships = new List<ShipEntity>();

        foreach(string entityId in entityIds)
        {
            ships.Add(loadShip(entityId));
        }

        return ships;
    }
}
