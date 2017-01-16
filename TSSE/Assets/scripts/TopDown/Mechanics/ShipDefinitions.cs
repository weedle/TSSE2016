using UnityEngine;
using System;



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
    public enum Item
    {
        FlameModDamage, FlameModFireRate, FlameModeSpread,
        LaserDamage, LaserModFireRate, LaserModeSpeed
    }

    // Used for display purposes
    public static string itemToString(Item item)
    {
        switch(item)
        {
            case Item.FlameModDamage:
                return "FlameModDamage";
            case Item.FlameModFireRate:
                return "FlameModFireRate";
            case Item.FlameModeSpread:
                return "FlameModeSpread";
            case Item.LaserDamage:
                return "LaserDamage";
            case Item.LaserModFireRate:
                return "LaserModFireRate";
            case Item.LaserModeSpeed:
                return "LaserModeSpeed";
            default:
                return "unknown";
        }
    }

    // Used to figure out what item an object is displaying
    public static Item stringToItem(String str)
    {
        switch (str)
        {
            case "FlameModDamage":
                return Item.FlameModDamage;
            case "FlameModFireRate":
                return Item.FlameModFireRate;
            case "FlameModeSpread":
                return Item.FlameModeSpread;
            case "LaserDamage":
                return Item.LaserDamage;
            case "LaserModFireRate":
                return Item.LaserModFireRate;
            case "LaserModeSpeed":
                return Item.LaserModeSpeed;
            default:
                return Item.FlameModDamage;
        }
    }

    // used for indexing item quantities
    public static int itemToInt(Item item)
    {
        switch (item)
        {
            case Item.FlameModDamage:
                return 0;
            case Item.FlameModFireRate:
                return 1;
            case Item.FlameModeSpread:
                return 2;
            case Item.LaserDamage:
                return 3;
            case Item.LaserModFireRate:
                return 4;
            case Item.LaserModeSpeed:
                return 5;
            default:
                return -1;
        }
    }

    // this is if we ever need to iterate through every item
    public static int numberOfItemTypes = 6;

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
