using UnityEngine;
using System.Collections;

// The Ion Cannon firing module, in code treated as a flamethrower
// since that's initially what it was based on
public class FlameMod : MonoBehaviour, FiringModule
{
    private GameObject projectile;
    private float projectileSpeed = 20;
    private float spread = 0.03f;
    private int fireRate = 2;
    private float projVel = 0.25f;
    private int ammoMax = 15;
    private int ammunition = 15;
    private float ammoCooldown = 2;
    private Timer timer;
    public string testItem;

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        if(faction == ShipDefinitions.Faction.Enemy)
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getRedFlames();
        else
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getBlueFlames();

        projectile.GetComponent<Flames>().setDamage(1.5f);
        projectile.GetComponent<Flames>().setLifetime(25);

        ShipDefinitions.Item testThing = ShipDefinitions.stringToItem(testItem);
        applyBuff(testThing);

        GameObject firingSprite = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getFiringSpriteObject();
        firingSprite.transform.parent = gameObject.transform;
        firingSprite.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - 0.07f,
            gameObject.transform.position.z);
        firingSprite.GetComponent<FiringSprite>()
            .setSprite(faction, "flame");
        timer = GameObject.Find("GameLogic").GetComponent<Timer>();
        timer.addTimer(this.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if (!timer)
            return;
        if (ammunition < ammoMax)
        {
            if (timer.checkTimer(this.GetInstanceID(), ammoCooldown))
            {
                ammunition = ammoMax;
            }
        }
    }

    // Fire a small burst of flaes
    public void fire()
    {
        if (ammunition > 0)
        {
            Vector3 vec;
            Vector3 temp;
            GameObject proj;
            vec = new Vector3(0, projVel, 0);
            vec = transform.rotation * vec;
            for (int i = 0; i <= fireRate; i++)
            {
                temp = new Vector3(transform.position.x, transform.position.y);
                proj = (GameObject)Instantiate(projectile,
                    temp + vec, Quaternion.Euler(0, 0, 90));
                proj.GetComponent<Particle>().setFaction(
                    ShipDefinitions.stringToFaction(
                        gameObject.GetComponent<ShipController>().getFaction().ToString()));
                temp = new Vector3(projectileSpeed * 
                    (vec.x + Random.Range(-spread, spread)), projectileSpeed * 
                    (vec.y + Random.Range(-spread, spread)), 0);
                proj.GetComponent<Rigidbody2D>().velocity = temp;
            }
            ammunition--;
            temp = Vector3.zero;
            vec = Vector3.zero;
        }
    }

    // Effective range for the flamethrower (technically ion cannon)
    public float getEffectiveDistance()
    {
        return 2;
    }

    // Effective angle at which to fire
    public float getEffectiveAngle()
    {
        return 4;
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

    public void applyBuff(ShipDefinitions.Item item)
    {
        print("Item is: " + ShipDefinitions.itemToString(item));
        if (item.tier == 0)
            return;
        switch(item.type)
        {
            case ShipDefinitions.ItemType.FlameModDamage:
                // damage is 1 by default
                // damage is 1.25 with tier 1 upgrade
                // damage is 1.5 with tier 2 upgrade
                // damage is 2 with tier 3 upgrade
                float bonusDamage = 0.25f;
                switch(item.tier)
                {
                    case 2:
                        bonusDamage += 0.25f;
                        break;
                    case 3:
                        bonusDamage = 1;
                        break;
                }
                projectile.GetComponent<Flames>().
                    setDamage(0.75f + bonusDamage);
                break;
            case ShipDefinitions.ItemType.FlameModFireRate:
                // rate is 2 by default
                // rate is 3 with tier 1 upgrade (1^1 = 1, ceil(1/2) = 1, 2+1 = 3)
                // rate is 4 with tier 2 upgrade (2^1 = 4, ceil(4/2) = 2, 2+2 = 4)
                // rate is 7 with tier 3 upgrade (3^1 = 9, ceil(9/2) = 5, 2+5 = 7)
                fireRate = 3 + (int)Mathf.Ceil((item.tier * item.tier) / 2);
                break;
            case ShipDefinitions.ItemType.FlameModSpread:
                // spread is 0.03 by default
                // spread is 0.05 with tier 1 upgrade
                // spread is 0.07 with tier 2 upgrade
                // spread is 0.09 with tier 3 upgrade
                spread = 0.03f + item.tier * 0.02f;
                break;
            case ShipDefinitions.ItemType.FlameModAmmoCap:
                // cap is 15 by default
                // cap is 20 with tier 1 upgrade
                // cap is 25 with tier 2 upgrade
                // cap is 30 with tier 3 upgrade
                ammoMax = 15 + 5 * item.tier;
                ammunition = ammoMax;
                break;
            case ShipDefinitions.ItemType.FlameModRechargeRate:
                // cooldown is 2 by default
                // cooldown is 1.75 with tier 1 upgrade
                // cooldown is 1.5 with tier 2 upgrade
                // cooldown is 1 with tier 3 upgrade
                float cooldownSaved = 0.25f;
                switch (item.tier)
                {
                    case 2:
                        cooldownSaved += 0.5f;
                        break;
                    case 3:
                        cooldownSaved = 1;
                        break;
                }
                ammoCooldown = 2 - cooldownSaved;
                break;
            case ShipDefinitions.ItemType.FlameModSpeed:
                // proj speed is 0.25 by default
                // proj speed is 0.3 with tier 1 upgrade
                // proj speed is 0.4 with tier 2 upgrade
                // proj speed is 0.55 with tier 3 upgrade
                float bonusSpeed = 0.05f;
                switch (item.tier)
                {
                    case 2:
                        bonusSpeed += 0.1f;
                        break;
                    case 3:
                        bonusSpeed += 0.25f;
                        break;
                }
                projVel = 0.25f + bonusSpeed;
                break;
            case ShipDefinitions.ItemType.FlameModRange:
                // lifetime is 15 by default
                // lifetime is 20 with tier 1 upgrade
                // lifetime is 25 with tier 2 upgrade
                // lifetime is 30 with tier 3 upgrade
                int bonusLife = 10 * item.tier;
                projectile.GetComponent<Flames>().setLifetime(15 + bonusLife);
                break;
            default:
                return;
        }
    }
}

