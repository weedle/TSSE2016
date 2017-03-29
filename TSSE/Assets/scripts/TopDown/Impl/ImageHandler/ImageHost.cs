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

    public Sprite shipRuby;
    public Sprite shipPeacock;

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
        return img;
    }
}
