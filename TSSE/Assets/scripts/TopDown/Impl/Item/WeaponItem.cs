using UnityEngine;
using System.Collections;
using System;

// This is an implementation of the ItemAbstract class
// This class describes every possible item that provides weapon buffs
// Currently, we have 24 different types and 3 different tiers, allowing
// for 72 different possible items
public class WeaponItem : ItemAbstract {
    public WeaponType type;
    public int tier;

    public const int numWeaponTypes = 72;

    public enum WeaponType
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
    
    public override string getDesc()
    {
        string retStr = "";
        switch (type)
        {
            case WeaponType.FlameModDamage:
                retStr = "Increases the damage of each projectile";
                break;
            case WeaponType.FlameModFireRate:
                retStr = "Increases the number of projectiles per burst";
                break;
            case WeaponType.FlameModSpread:
                retStr = "Widens the angle range the projectiles can be shot at";
                // This results is a wider "spray" of particles.
                break;
            case WeaponType.FlameModAmmoCap:
                retStr = "Increases the number of projectiles per clip";
                break;
            case WeaponType.FlameModRechargeRate:
                retStr = "Increases delay before ammunition is fully replenished";
                break;
            case WeaponType.FlameModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case WeaponType.FlameModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case WeaponType.LaserModDamage:
                retStr = "Increases the damage of each projectile";
                break;
            case WeaponType.LaserModFireRate:
                retStr = "Decreases the delay between each fired projectile";
                break;
            case WeaponType.LaserModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case WeaponType.LaserModAmmoCap:
                retStr = "Increases the number of projectiles per clip";
                break;
            case WeaponType.LaserModRechargeRate:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case WeaponType.LaserModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case WeaponType.MissileModDamage:
                retStr = "Increases the damage of each projectile";
                break;
            case WeaponType.MissileModFireRate:
                retStr = "Decreases the delay between each fired projectile";
                break;
            case WeaponType.MissileModAmmoCap:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case WeaponType.MissileModRechargeRate:
                retStr = "Increases rate at which ammo is regenerated";
                break;
            case WeaponType.MissileModSpeed:
                retStr = "Increases projectile velocity (and as a result, range)";
                break;
            case WeaponType.MissileModRange:
                retStr = "Increases projectile lifetime (directly increasing range)";
                break;
            case WeaponType.CrownModDamage:
                retStr = "Increases the damage of each shot";
                break;
            case WeaponType.CrownModAmmoCap:
                retStr = "Increases the number of shots per burst";
                break;
            case WeaponType.CrownModRechargeRate:
                retStr = "Increases delay before ammunition is fully replenished";
                break;
            case WeaponType.CrownModRange:
                retStr = "Increases maximum distance between ship and target to fire";
                break;
            case WeaponType.Error:
                retStr = "Error: unspecified item, probably Kevin's fault, man, that guy";
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
            case WeaponType.FlameModDamage:
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
            case WeaponType.FlameModFireRate:
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
            case WeaponType.FlameModSpread:
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
            case WeaponType.FlameModAmmoCap:
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
            case WeaponType.FlameModRechargeRate:
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
            case WeaponType.FlameModSpeed:
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
            case WeaponType.FlameModRange:
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
            case WeaponType.LaserModDamage:
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
            case WeaponType.LaserModFireRate:
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
            case WeaponType.LaserModSpeed:
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
            case WeaponType.LaserModAmmoCap:
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
            case WeaponType.LaserModRechargeRate:
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
            case WeaponType.LaserModRange:
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
            case WeaponType.MissileModDamage:
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
            case WeaponType.MissileModFireRate:
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
            case WeaponType.MissileModAmmoCap:
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
            case WeaponType.MissileModRechargeRate:
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
            case WeaponType.MissileModSpeed:
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
            case WeaponType.MissileModRange:
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
            case WeaponType.CrownModDamage:
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
            case WeaponType.CrownModAmmoCap:
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
            case WeaponType.CrownModRechargeRate:
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
            case WeaponType.CrownModRange:
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
            case WeaponType.Error:
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

    public static WeaponType getWeaponType(string str)
    {
        WeaponType type;
        try
        {
            type = (WeaponType)Enum.Parse(typeof(WeaponType), str);
        }
        catch (ArgumentException err)
        {
            type = WeaponType.Error;
        }

        return type;
    }

    public static Boolean isWeaponType(string str)
    {
        if (getWeaponType(str) == WeaponType.Error)
        {
            return false;
        }
        return true;
    }

    public override string toString()
    {
        return type.ToString() + ":" + tier.ToString();
    }

    public WeaponItem()
    {
        type = WeaponType.Error;
        this.tier = 0;
    }

    public WeaponItem(WeaponType type)
    {
        this.type = type;
        this.tier = 0;
    }

    public WeaponItem(WeaponType type, int tier)
    {
        this.type = type;
        this.tier = tier;
    }

    public WeaponItem(String type)
    {
        this.type = getWeaponType(type);
        this.tier = 0;
    }

    public WeaponItem(String type, int tier)
    {
        this.type = getWeaponType(type);
        this.tier = tier;
    }
}
