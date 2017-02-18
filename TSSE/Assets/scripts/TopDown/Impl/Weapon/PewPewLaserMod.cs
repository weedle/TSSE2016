using UnityEngine;
using System.Collections;

// space game need laser gun
public class PewPewLaserMod : MonoBehaviour, FiringModule
{
    private GameObject projectile;
    private float projectileSpeed = 40;
    private int ammoMax = 4;
    private int ammunition = 4;
    private float ammoCooldown = 1;
    private float immediateCooldownMax = 0.4f;
    private Timer timer;
    public string testItem = "";

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getLaser();
        //projectileSpeed += Random.Range(-4, 4);
        
        projectile.GetComponent<PewPewLaser>().setDamage(10);
        projectile.GetComponent<PewPewLaser>().setLifetime(25);

        if (testItem != "")
        {
            ItemDefinitions.Item testThing = ItemDefinitions.stringToItem(testItem);
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
        timer.addTimer(this.GetInstanceID() + 1);
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

    // Fire a small burst of flaes
    public void fire()
    {
        if (ammunition > 0)
        {
            if (timer.checkTimer(this.GetInstanceID() + 1, immediateCooldownMax))
            {
                Vector3 vec;
                Vector3 temp;
                Rigidbody2D proj;
                vec = new Vector3(0, (float)0.25, 0);
                vec = transform.rotation * vec;
                temp = new Vector3(transform.position.x, transform.position.y);
                proj = (Rigidbody2D)Instantiate(projectile.GetComponent<Rigidbody2D>(),
                    temp + vec, transform.rotation);
                temp = new Vector3(projectileSpeed *
                    (vec.x), projectileSpeed *
                    (vec.y), 0);
                proj.velocity = temp;

                ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
                proj.GetComponent<Particle>().setFaction(faction);
                if (faction == ShipDefinitions.Faction.PlayerAffil)
                    proj.GetComponent<SpriteRenderer>().color = Color.cyan;
                else
                    proj.GetComponent<SpriteRenderer>().color = Color.yellow;
                //proj.MoveRotation(proj.transform.rotation.eulerAngles.z
                //    + Random.Range(-15, 15));
                ammunition--;
                temp = Vector3.zero;
                vec = Vector3.zero;
            }
        }
    }

    // Effective range for the flamethrower (technically ion cannon)
    public float getEffectiveDistance()
    {
        return 8;
    }

    // Effective angle at which to fire
    public float getEffectiveAngle()
    {
        return 2;
    }

    // Whether or not the weapon has ammunition and is not on cooldown
    public bool canFire()
    {
        if (ammunition > 0) return true;
        else return false;
    }

    public float getAmmoPerc()
    {
        return (float)ammunition / ammoMax;
    }

    public void applyBuff(ItemDefinitions.Item item)
    {
        if (item.tier == 0)
            return;
        switch (item.type)
        {
            case ItemDefinitions.ItemType.LaserModDamage:
                // damage is 1 by default
                // damage is 14 with tier 1 upgrade
                // damage is 16 with tier 2 upgrade
                // damage is 24 with tier 3 upgrade
                float bonusDamage = 4f;
                switch (item.tier)
                {
                    case 2:
                        bonusDamage += 4f;
                        break;
                    case 3:
                        bonusDamage = 24;
                        break;
                }
                projectile.GetComponent<PewPewLaser>().
                    setDamage(12 + bonusDamage);
                break;
            case ItemDefinitions.ItemType.LaserModFireRate:
                // fire delay is 0.4 by default
                // fire delay is 0.3 with tier 1 upgrade
                // fire delay is 0.2 with tier 2 upgrade
                // fire delay is 0.1 with tier 3 upgrade
                immediateCooldownMax = 0.4f;
                immediateCooldownMax -= item.tier * 0.1f;
                break;
            case ItemDefinitions.ItemType.LaserModSpeed:
                // proj speed is 40 by default
                // proj speed is 50 with tier 1 upgrade
                // proj speed is 60 with tier 2 upgrade
                // proj speed is 70 with tier 3 upgrade
                projectileSpeed = 40;
                projectileSpeed += 10 * item.tier;
                break;
            case ItemDefinitions.ItemType.LaserModAmmoCap:
                // cap is 4 by default
                // cap is 6 with tier 1 upgrade
                // cap is 8 with tier 2 upgrade
                // cap is 10 with tier 3 upgrade
                ammoMax = 4;
                ammoMax += item.tier * 2;
                ammunition = ammoMax;
                break;
            case ItemDefinitions.ItemType.LaserModRechargeRate:
                // cooldown is 2 by default
                // cooldown is 1.5 with tier 1 upgrade
                // cooldown is 1 with tier 2 upgrade
                // cooldown is 0.5 with tier 3 upgrade
                ammoCooldown = 2;
                ammoCooldown -= 0.5f * item.tier;
                break;
            case ItemDefinitions.ItemType.LaserModRange:
                // lifetime is 25 by default
                // lifetime is 30 with tier 1 upgrade
                // lifetime is 35 with tier 2 upgrade
                // lifetime is 40 with tier 3 upgrade
                int newLifetime = 25;
                newLifetime += item.tier * 5;
                projectile.GetComponent<PewPewLaser>().setLifetime(newLifetime);
                break;
            default:
                return;
        }
    }
}

