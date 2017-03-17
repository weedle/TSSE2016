using UnityEngine;
using System.Collections;

// Firing Module that shoots damaging homing missiles
public class MissileMod : MonoBehaviour, FiringModule
{
    private GameObject projectile;
    private int ammoMax = 3;
    private int ammunition = 3;
    private float ammoCooldown = 3;
    private float immediateCooldownMax = 1;
    private Timer timer;
    public string testItem = "";

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getMissileObject();

        projectile.GetComponent<HomingMissile>().setDamage(20);
        projectile.GetComponent<HomingMissile>().setLifetime(25);

        if (testItem != "")
        {
            ItemAbstract testThing = ItemDefinitions.stringToItem(testItem);
            applyBuff(testThing);
        }

        /*
        GameObject firingSprite = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getFiringSpriteObject();
        firingSprite.transform.parent = gameObject.transform;
        firingSprite.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - 0.07f,
            gameObject.transform.position.z);
        firingSprite.GetComponent<FiringSprite>()
            .setSprite(faction, "crown");
        */

        timer = GameObject.Find("GameLogic").GetComponent<Timer>();
        timer.addTimer(this.GetInstanceID());
        timer.addTimer(this.GetInstanceID()+1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer)
            return;
        if (timer.checkTimer(this.GetInstanceID(), ammoCooldown))
        {
            if (ammunition < ammoMax)
            {
                ammunition++;
            }
        }
    }

    // Fire a missile
    public void fire()
    {
        if (ammunition > 0)
        {
            if (timer.checkTimer(this.GetInstanceID()+1, immediateCooldownMax))
            {
                Vector3 vec;
                Vector3 temp;
                Rigidbody2D proj;
                vec = new Vector3(0, (float)0.25, 0);
                vec = transform.rotation * vec;
                temp = new Vector3(transform.position.x, transform.position.y);
                proj = (Rigidbody2D)Instantiate(projectile.GetComponent<Rigidbody2D>(),
                    temp + vec, Quaternion.Euler(0, 0, 90));
                temp = new Vector3(vec.x, vec.y, 0);
                proj.velocity = temp;
                proj.MoveRotation(transform.rotation.eulerAngles.z);
                proj.GetComponent<Particle>().setFaction(ShipDefinitions.stringToFaction(gameObject.tag));

                ammunition--;
            }
        }
    }

    // Distance at which to fire
    public float getEffectiveDistance()
    {
        return 3;
    }

    // Angle at which to fire
    public float getEffectiveAngle()
    {
        return 6;
    }

    // can fire if have ammunition and not on cooldown
    public bool canFire()
    {
        if (ammunition > 0) return true;
        else return false;
    }

    public float getAmmoPerc()
    {
        return (float)ammunition / ammoMax;
    }

    public void applyBuff(ItemAbstract itemA)
    {
        WeaponItem item = (WeaponItem)itemA;
        if (!WeaponItem.isWeaponType(item.getType()))
            return;
        print("Item is: " + ItemDefinitions.itemToString(item));
        if (item.tier == 0)
            return;
        if(projectile == null)
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getMissileObject();
        switch (item.type)
        {
            case WeaponItem.WeaponType.MissileModDamage:
                // damage is 20 by default
                // damage is 30 with tier 1 upgrade
                // damage is 40 with tier 2 upgrade
                // damage is 50 with tier 3 upgrade
                int damage = 20;
                damage += item.tier * 10;
                projectile.GetComponent<HomingMissile>().
                    setDamage(damage);
                break;
            case WeaponItem.WeaponType.MissileModFireRate:
                // rate is 1 by default
                // rate is 0.75 with tier 1 upgrade
                // rate is 0.5 with tier 2 upgrade
                // rate is 0.25 with tier 3 upgrade
                immediateCooldownMax = 1;
                immediateCooldownMax -= item.tier * 0.25f;
                break;
            case WeaponItem.WeaponType.MissileModAmmoCap:
                // cap is 2 by default
                // cap is 3 with tier 1 upgrade (1^1 = 1, ceil(1/2) = 1, 2+1 = 3)
                // cap is 4 with tier 2 upgrade (2^1 = 4, ceil(4/2) = 2, 2+2 = 4)
                // cap is 7 with tier 3 upgrade (3^1 = 9, ceil(9/2) = 5, 2+5 = 7)
                ammoMax = 3 + (int)Mathf.Ceil((item.tier * item.tier) / 2);
                ammunition = ammoMax;
                break;
            case WeaponItem.WeaponType.MissileModRechargeRate:
                // cooldown is 3 by default
                // cooldown is 2.5 with tier 1 upgrade
                // cooldown is 2 with tier 2 upgrade
                // cooldown is 1.5 with tier 3 upgrade
                ammoCooldown = 3;
                ammoCooldown -= item.tier * 0.5f;
                break;
            case WeaponItem.WeaponType.MissileModSpeed:
                // proj speed is 3 by default
                // proj speed is 4 with tier 1 upgrade
                // proj speed is 5 with tier 2 upgrade
                // proj speed is 6 with tier 3 upgrade
                int newSpeed = 3;
                newSpeed += item.tier;
                projectile.GetComponent<HomingMissile>().setMoveSpeed(newSpeed);
                break;
            case WeaponItem.WeaponType.MissileModRange:
                // lifetime is 25 by default
                // lifetime is 35 with tier 1 upgrade
                // lifetime is 45 with tier 2 upgrade
                // lifetime is 60 with tier 3 upgrade
                int newLifetime = 25;
                newLifetime += 10;
                if (item.tier == 2)
                    newLifetime += 10;
                else if (item.tier == 3)
                    newLifetime += 25;
                projectile.GetComponent<HomingMissile>().setLifetime(newLifetime);
                break;
            default:
                return;
        }
    }
}
