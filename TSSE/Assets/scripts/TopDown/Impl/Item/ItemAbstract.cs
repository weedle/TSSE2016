﻿using UnityEngine;
using System.Collections;

public abstract class ItemAbstract {
    public abstract string getDesc();

    public abstract string getSpec();

    public abstract string getType();

    public abstract int getTier();

    public abstract string toString();

    public static ItemAbstract newItem()
    {
        return new WeaponItem(WeaponItem.WeaponType.Error);
    }

    public static ItemAbstract newItem(string type)
    {
        if(WeaponItem.isWeaponType(type))
        {
            return new WeaponItem(type);
        }
        else if (EngineItem.isEngineType(type))
        {
            return new EngineItem(type);
        }
        return new WeaponItem(WeaponItem.WeaponType.Error);
    }

    public static ItemAbstract newItem(string type, int tier)
    {
        if (WeaponItem.isWeaponType(type))
        {
            return new WeaponItem(type, tier);
        }
        else if(EngineItem.isEngineType(type))
        {
            return new EngineItem(type, tier);
        }
        return new WeaponItem(WeaponItem.WeaponType.Error, 0);
    }

    public static ItemAbstract newItem(WeaponItem.WeaponType type)
    {
        return new WeaponItem(type);
    }

    public static ItemAbstract newItem(EngineItem.EngineType type)
    {
        return new EngineItem(type);
    }

    public static ItemAbstract newItem(WeaponItem.WeaponType type, int tier)
    {
        return new WeaponItem(type, tier);
    }

    public static ItemAbstract newItem(EngineItem.EngineType type, int tier)
    {
        return new EngineItem(type, tier);
    }
}
