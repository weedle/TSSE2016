using UnityEngine;
using System.Collections;
using System;

// This is an implementation of the ItemAbstract class
public class EngineItem : ItemAbstract
{
    public EngineType type;
    public int tier;

    public const int numEngineTypes = 12;
    public enum EngineType
    {
        Thruster,   // Provides strong forward acceleration but 
                    // decreased turning
        Spinjet,    // Allows for increased turning at the cost of
                    // decreased acceleration
        Standard ,   // A balanced engine
        Error
    }

    public override string getDesc()
    {
        string retStr = "";
        switch (type)
        {
            case EngineType.Thruster:
                retStr = "Increased acceleration, decreased maneuverability";
                break;
            case EngineType.Spinjet:
                retStr = "Increased maneuverability, decreased acceleration";
                break;
            case EngineType.Standard:
                retStr = "An engine with balanced stats";
                break;
            case EngineType.Error:
                retStr = "Error: oh no how are you reading this";
                break;
        }
        if (tier > 3)
        {
            retStr = "BLUEPRINT:\n" + retStr;
        }
        return retStr;
    }

    public override string getSpec()
    {
        string retStr = "";
        string prefix = "";
        int x = tier;
        if (tier > 3)
        {
            prefix = "BLUEPRINT:\n";
            x -= 3;
        }
        switch (type)
        {
            case EngineType.Thruster:
                switch (x)
                {
                    case 1:
                        retStr = "infothruster1";
                        break;
                    case 2:
                        retStr = "infothruster2";
                        break;
                    case 3:
                        retStr = "infothruster3";
                        break;
                }
                break;
            case EngineType.Spinjet:
                switch (x)
                {
                    case 1:
                        retStr = "infospin1";
                        break;
                    case 2:
                        retStr = "infospin2";
                        break;
                    case 3:
                        retStr = "infospin3";
                        break;
                }
                break;
            case EngineType.Standard:
                switch (x)
                {
                    case 1:
                        retStr = "infostandard1";
                        break;
                    case 2:
                        retStr = "infostandard2";
                        break;
                    case 3:
                        retStr = "infostandard3";
                        break;
                }
                break;
            case EngineType.Error:
                retStr = "(╯°□°）╯︵ ┻━┻";
                break;
        }
        retStr = prefix + retStr;
        return retStr;
    }

    public override string getType()
    {
        return type.ToString();
    }

    public override int getTier()
    {
        return tier;
    }

    public static EngineType getEngineType(string str)
    {
        EngineType type;
        try
        {
            type = (EngineType)Enum.Parse(typeof(EngineType), str);
        }
        catch (ArgumentException err)
        {
            type = EngineType.Error;
        }

        return type;
    }

    public static Boolean isEngineType(string str)
    {
        if (getEngineType(str) == EngineType.Error)
        {
            return false;
        }
        return true;
    }

    public override string toString()
    {
        return type.ToString() + ":" + tier.ToString();
    }

    public EngineItem()
    {
        type = EngineType.Error;
        this.tier = 0;
    }

    public EngineItem(EngineType type)
    {
        this.type = type;
        this.tier = 0;
    }

    public EngineItem(EngineType type, int tier)
    {
        this.type = type;
        this.tier = tier;
    }

    public EngineItem(String type)
    {
        this.type = getEngineType(type);
        this.tier = 0;
    }

    public EngineItem(String type, int tier)
    {
        this.type = getEngineType(type);
        this.tier = tier;
    }
}
