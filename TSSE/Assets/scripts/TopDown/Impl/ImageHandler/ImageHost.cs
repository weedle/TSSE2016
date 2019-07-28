using UnityEngine;
using System.Collections;

// Stores the various images we'll need to access, such as ship and item icons
public class ImageHost : MonoBehaviour {
    public Sprite defaultSprite;
    public Sprite fireSpriteLvl1;
    public Sprite fireSpriteLvl2;
    public Sprite fireSpriteLvl3;
    public Sprite laserSpriteLvl1;
    public Sprite laserSpriteLvl2;
    public Sprite laserSpriteLvl3;
    public Sprite crownSpriteLvl1;
    public Sprite crownSpriteLvl2;
    public Sprite crownSpriteLvl3;
    public Sprite missileSpriteLvl1;
    public Sprite missileSpriteLvl2;
    public Sprite missileSpriteLvl3;

    public Sprite blueprintFireSpriteLvl1;
    public Sprite blueprintFireSpriteLvl2;
    public Sprite blueprintFireSpriteLvl3;
    public Sprite blueprintLaserSpriteLvl1;
    public Sprite blueprintLaserSpriteLvl2;
    public Sprite blueprintLaserSpriteLvl3;
    public Sprite blueprintCrownSpriteLvl1;
    public Sprite blueprintCrownSpriteLvl2;
    public Sprite blueprintCrownSpriteLvl3;
    public Sprite blueprintMissileSpriteLvl1;
    public Sprite blueprintMissileSpriteLvl2;
    public Sprite blueprintMissileSpriteLvl3;

    public Sprite engineStandardLvl1;
    public Sprite engineStandardLvl2;
    public Sprite engineStandardLvl3;
    public Sprite engineSpinjetLvl1;
    public Sprite engineSpinjetLvl2;
    public Sprite engineSpinjetLvl3;
    public Sprite engineThrusterLvl1;
    public Sprite engineThrusterLvl2;
    public Sprite engineThrusterLvl3;

    public Sprite blueprintEngineStandardLvl1;
    public Sprite blueprintEngineStandardLvl2;
    public Sprite blueprintEngineStandardLvl3;
    public Sprite blueprintEngineSpinjetLvl1;
    public Sprite blueprintEngineSpinjetLvl2;
    public Sprite blueprintEngineSpinjetLvl3;
    public Sprite blueprintEngineThrusterLvl1;
    public Sprite blueprintEngineThrusterLvl2;
    public Sprite blueprintEngineThrusterLvl3;

    public Sprite shipRuby;
    public Sprite shipPeacock;

    void Start()
    {

        if (!fireSpriteLvl1)
            fireSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/fireIconLvl1", typeof(Sprite));
        if (!fireSpriteLvl2)
            fireSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/fireIconLvl2", typeof(Sprite));
        if (!fireSpriteLvl3)
            fireSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/fireIconLvl3", typeof(Sprite));
        if (!laserSpriteLvl1)
            laserSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/laserIconLvl1", typeof(Sprite));
        if (!laserSpriteLvl2)
            laserSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/laserIconLvl2", typeof(Sprite));
        if (!laserSpriteLvl3)
            laserSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/laserIconLvl3", typeof(Sprite));
        if (!crownSpriteLvl1)
            crownSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/crownIconLvl1", typeof(Sprite));
        if (!crownSpriteLvl2)
            crownSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/crownIconLvl2", typeof(Sprite));
        if (!crownSpriteLvl3)
            crownSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/crownIconLvl3", typeof(Sprite));
        if (!missileSpriteLvl1)
            missileSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/missileIconLvl1", typeof(Sprite));
        if (!missileSpriteLvl2)
            missileSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/missileIconLvl2", typeof(Sprite));
        if (!missileSpriteLvl3)
            missileSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/missileIconLvl3", typeof(Sprite));
        if (!blueprintFireSpriteLvl1)
            blueprintFireSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/flameBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintFireSpriteLvl2)
            blueprintFireSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/flameBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintFireSpriteLvl3)
            blueprintFireSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/flameBlueprintIconLvl3", typeof(Sprite));
        if (!blueprintLaserSpriteLvl1)
            blueprintLaserSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/laserBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintLaserSpriteLvl2)
            blueprintLaserSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/laserBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintLaserSpriteLvl3)
            blueprintLaserSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/laserBlueprintIconLvl3", typeof(Sprite));
        if (!blueprintCrownSpriteLvl1)
            blueprintCrownSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/crownBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintCrownSpriteLvl2)
            blueprintCrownSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/crownBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintCrownSpriteLvl3)
            blueprintCrownSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/crownBlueprintIconLvl3", typeof(Sprite));
        if (!blueprintMissileSpriteLvl1)
            blueprintMissileSpriteLvl1 = (Sprite)Resources.Load("sprites/weapon/missileBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintMissileSpriteLvl2)
            blueprintMissileSpriteLvl2 = (Sprite)Resources.Load("sprites/weapon/missileBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintMissileSpriteLvl3)
            blueprintMissileSpriteLvl3 = (Sprite)Resources.Load("sprites/weapon/missileBlueprintIconLvl3", typeof(Sprite));
        if (!engineStandardLvl1)
            engineStandardLvl1 = (Sprite)Resources.Load("sprites/weapon/engineStandardLvl1", typeof(Sprite));
        if (!engineStandardLvl2)
            engineStandardLvl2 = (Sprite)Resources.Load("sprites/weapon/engineStandardLvl2", typeof(Sprite));
        if (!engineStandardLvl3)
            engineStandardLvl3 = (Sprite)Resources.Load("sprites/weapon/engineStandardLvl3", typeof(Sprite));
        if (!engineSpinjetLvl1)
            engineSpinjetLvl1 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetLvl1", typeof(Sprite));
        if (!engineSpinjetLvl2)
            engineSpinjetLvl2 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetLvl2", typeof(Sprite));
        if (!engineSpinjetLvl3)
            engineSpinjetLvl3 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetLvl3", typeof(Sprite));
        if (!engineThrusterLvl1)
            engineThrusterLvl1 = (Sprite)Resources.Load("sprites/weapon/engineThrusterLvl1", typeof(Sprite));
        if (!engineThrusterLvl2)
            engineThrusterLvl2 = (Sprite)Resources.Load("sprites/weapon/engineThrusterLvl2", typeof(Sprite));
        if (!engineThrusterLvl3)
            engineThrusterLvl3 = (Sprite)Resources.Load("sprites/weapon/engineThrusterLvl3", typeof(Sprite));
        if (!blueprintEngineStandardLvl1)
            blueprintEngineStandardLvl1 = (Sprite)Resources.Load("sprites/weapon/engineStandardBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintEngineStandardLvl2)
            blueprintEngineStandardLvl2 = (Sprite)Resources.Load("sprites/weapon/engineStandardBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintEngineStandardLvl3)
            blueprintEngineStandardLvl3 = (Sprite)Resources.Load("sprites/weapon/engineStandardBlueprintIconLvl3", typeof(Sprite));
        if (!blueprintEngineSpinjetLvl1)
            blueprintEngineSpinjetLvl1 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintEngineSpinjetLvl2)
            blueprintEngineSpinjetLvl2 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintEngineSpinjetLvl3)
            blueprintEngineSpinjetLvl3 = (Sprite)Resources.Load("sprites/weapon/engineSpinjetBlueprintIconLvl3", typeof(Sprite));
        if (!blueprintEngineThrusterLvl1)
            blueprintEngineThrusterLvl1 = (Sprite)Resources.Load("sprites/weapon/engineThrusterBlueprintIconLvl1", typeof(Sprite));
        if (!blueprintEngineThrusterLvl2)
            blueprintEngineThrusterLvl2 = (Sprite)Resources.Load("sprites/weapon/engineThrusterBlueprintIconLvl2", typeof(Sprite));
        if (!blueprintEngineThrusterLvl3)
            blueprintEngineThrusterLvl3 = (Sprite)Resources.Load("sprites/weapon/engineThrusterBlueprintIconLvl3", typeof(Sprite));

        if (!shipRuby)
            shipRuby = (Sprite)Resources.Load("sprites/weapon/engineStandardBlueprintIconLvl2", typeof(Sprite));
        if (!shipPeacock)
            shipPeacock = (Sprite)Resources.Load("sprites/weapon/engineStandardBlueprintIconLvl3", typeof(Sprite));
    }

    // getImage will return the appropriate icon for a given item
    public Sprite getImage(ItemAbstract item)
    {
        Sprite img = defaultSprite;
        if (WeaponItem.isWeaponType(item.getType()))
        {
            WeaponItem itemW = (WeaponItem)item;
            if (itemW.type == WeaponItem.WeaponType.FlameModAmmoCap ||
                    itemW.type == WeaponItem.WeaponType.FlameModDamage ||
                    itemW.type == WeaponItem.WeaponType.FlameModFireRate ||
                    itemW.type == WeaponItem.WeaponType.FlameModRange ||
                    itemW.type == WeaponItem.WeaponType.FlameModRechargeRate ||
                    itemW.type == WeaponItem.WeaponType.FlameModSpeed ||
                    itemW.type == WeaponItem.WeaponType.FlameModSpread ||
                    itemW.type == WeaponItem.WeaponType.FlameModFireRate ||
                    itemW.type == WeaponItem.WeaponType.FlameModRange ||
                    itemW.type == WeaponItem.WeaponType.FlameModRechargeRate ||
                    itemW.type == WeaponItem.WeaponType.FlameModSpeed ||
                    itemW.type == WeaponItem.WeaponType.FlameModSpread)
            {
                switch (itemW.tier)
                {
                    case 1:
                        img = fireSpriteLvl1;
                        break;
                    case 2:
                        img = fireSpriteLvl2;
                        break;
                    case 3:
                        img = fireSpriteLvl3;
                        break;
                    case 4:
                        img = blueprintFireSpriteLvl1;
                        break;
                    case 5:
                        img = blueprintFireSpriteLvl2;
                        break;
                    case 6:
                        img = blueprintFireSpriteLvl3;
                        break;
                }
            }
            else if (itemW.type == WeaponItem.WeaponType.LaserModAmmoCap ||
                itemW.type == WeaponItem.WeaponType.LaserModDamage ||
                itemW.type == WeaponItem.WeaponType.LaserModFireRate ||
                itemW.type == WeaponItem.WeaponType.LaserModRange ||
                itemW.type == WeaponItem.WeaponType.LaserModRechargeRate ||
                itemW.type == WeaponItem.WeaponType.LaserModSpeed)
            {
                switch (itemW.tier)
                {
                    case 1:
                        img = laserSpriteLvl1;
                        break;
                    case 2:
                        img = laserSpriteLvl2;
                        break;
                    case 3:
                        img = laserSpriteLvl3;
                        break;
                    case 4:
                        img = blueprintLaserSpriteLvl1;
                        break;
                    case 5:
                        img = blueprintLaserSpriteLvl2;
                        break;
                    case 6:
                        img = blueprintLaserSpriteLvl3;
                        break;
                }
            }
            else if (itemW.type == WeaponItem.WeaponType.CrownModAmmoCap ||
                itemW.type == WeaponItem.WeaponType.CrownModDamage ||
                itemW.type == WeaponItem.WeaponType.CrownModRange ||
                itemW.type == WeaponItem.WeaponType.CrownModRechargeRate)
            {
                switch (itemW.tier)
                {
                    case 1:
                        img = crownSpriteLvl1;
                        break;
                    case 2:
                        img = crownSpriteLvl2;
                        break;
                    case 3:
                        img = crownSpriteLvl3;
                        break;
                    case 4:
                        img = blueprintCrownSpriteLvl1;
                        break;
                    case 5:
                        img = blueprintCrownSpriteLvl2;
                        break;
                    case 6:
                        img = blueprintCrownSpriteLvl3;
                        break;
                }
            }
            else if (itemW.type == WeaponItem.WeaponType.MissileModAmmoCap ||
                itemW.type == WeaponItem.WeaponType.MissileModDamage ||
                itemW.type == WeaponItem.WeaponType.MissileModFireRate ||
                itemW.type == WeaponItem.WeaponType.MissileModRange ||
                itemW.type == WeaponItem.WeaponType.MissileModRechargeRate ||
                itemW.type == WeaponItem.WeaponType.MissileModSpeed)
            {
                switch (itemW.tier)
                {
                    case 1:
                        img = missileSpriteLvl1;
                        break;
                    case 2:
                        img = missileSpriteLvl2;
                        break;
                    case 3:
                        img = missileSpriteLvl3;
                        break;
                    case 4:
                        img = blueprintMissileSpriteLvl1;
                        break;
                    case 5:
                        img = blueprintMissileSpriteLvl2;
                        break;
                    case 6:
                        img = blueprintMissileSpriteLvl3;
                        break;
                }
            }
            else
            {
                img = defaultSprite;
            }
        }
        else
        if (EngineItem.isEngineType(item.getType()))
        {
            EngineItem itemE = (EngineItem)item;
            if(itemE.type == EngineItem.EngineType.Standard)
            {
                switch(itemE.tier)
                {
                    case 1:
                        img = engineStandardLvl1;
                        break;
                    case 2:
                        img = engineStandardLvl2;
                        break;
                    case 3:
                        img = engineStandardLvl3;
                        break;
                    case 4:
                        img = blueprintEngineStandardLvl1;
                        break;
                    case 5:
                        img = blueprintEngineStandardLvl2;
                        break;
                    case 6:
                        img = blueprintEngineStandardLvl3;
                        break;
                }
            }
            else if (itemE.type == EngineItem.EngineType.Spinjet)
            {
                switch (itemE.tier)
                {
                    case 1:
                        img = engineSpinjetLvl1;
                        break;
                    case 2:
                        img = engineSpinjetLvl2;
                        break;
                    case 3:
                        img = engineSpinjetLvl3;
                        break;
                    case 4:
                        img = blueprintEngineSpinjetLvl1;
                        break;
                    case 5:
                        img = blueprintEngineSpinjetLvl2;
                        break;
                    case 6:
                        img = blueprintEngineSpinjetLvl3;
                        break;
                }
            }
            else if (itemE.type == EngineItem.EngineType.Thruster)
            {
                switch (itemE.tier)
                {
                    case 1:
                        img = engineThrusterLvl1;
                        break;
                    case 2:
                        img = engineThrusterLvl2;
                        break;
                    case 3:
                        img = engineThrusterLvl3;
                        break;
                    case 4:
                        img = blueprintEngineThrusterLvl1;
                        break;
                    case 5:
                        img = blueprintEngineThrusterLvl2;
                        break;
                    case 6:
                        img = blueprintEngineThrusterLvl3;
                        break;
                }
            }
        }
        return img;
    }
}
