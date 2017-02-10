using UnityEngine;
using System.Collections;

// The pew pew laser weapon of this game!
// The effect is that of a flickering line, it's pretty neat
// Also, very simple to implement xD
public class CrownMod : MonoBehaviour, FiringModule
{
    public Color color1 = Color.blue;
    public Color color2 = Color.cyan;
    private ShipDefinitions.Faction faction;
    private int ammoMax = 15;
    private int ammunition = 15;
    private float ammoCooldown = 3;
    private float damage = 2;
    private float distance = 2;
    Timer timer;
    public string testItem = "";

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<TargetFinder>();
        faction = ShipDefinitions.stringToFaction(gameObject.tag);

        // bad guys use red lasers, good guys use blue?
        if(faction == ShipDefinitions.Faction.Enemy)
        {
            color1 = Color.red;
            color2 = Color.yellow;
        }

        if (testItem != "")
        {
            ShipDefinitions.Item testThing = ShipDefinitions.stringToItem(testItem);
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

    // Firing your laser
    public void fire()
    {
        GameObject target = GetComponent<TargetFinder>().getTarget(faction);
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector3.zero;

        if (target == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) > getEffectiveDistance())
        {
            return;
        }

        if (ammunition > 0)
        {
            Vector3 firePoint = transform.position;
            ShipDefinitions.DrawLine(firePoint, target.transform.position, color1, 0.1f);
            ShipDefinitions.DrawLine(firePoint, target.transform.position, color2, 0.12f);
            ShipDefinitions.DrawLine(firePoint, target.transform.position, color1, 0.14f);
            ShipDefinitions.DrawLine(firePoint, target.transform.position, color2, 0.1f);
            target.GetComponent<ShipIntf>().isHit(damage);
            ammunition--;
        }
        target = null;
    }

    // Effective distance is the range at which we will fire
    // Note that given the way this works, this is also the actual range of the weapon
    public float getEffectiveDistance()
    {
        return distance;
    }
    
    // Ship should vaguely point towards target before firing
    public float getEffectiveAngle()
    {
        return 8;
    }

    // Returns true if this weapon has ammunition in clip and isn't on cooldown
    public bool canFire()
    {
        if (ammunition > 0) return true;
        else return false;
    }

    public float getAmmoPerc()
    {
        return (float) ammunition / ammoMax;
    }

    public void applyBuff(ShipDefinitions.Item item)
    {
        if (item.tier == 0)
            return;
        switch (item.type)
        {
            case ShipDefinitions.ItemType.CrownModDamage:
                // damage is 2 by default
                // damage is 3 with tier 1 upgrade
                // damage is 4 with tier 2 upgrade
                // damage is 6 with tier 3 upgrade
                float bonusDamage = 0;
                switch (item.tier)
                {
                    case 1:
                        bonusDamage = 1;
                        break;
                    case 2:
                        bonusDamage = 2;
                        break;
                    case 3:
                        bonusDamage = 4;
                        break;
                }
                damage = 2 + bonusDamage;
                break;
            case ShipDefinitions.ItemType.CrownModAmmoCap:
                // cap is 15 by default
                // cap is 20 with tier 1 upgrade
                // cap is 25 with tier 2 upgrade
                // cap is 30 with tier 3 upgrade
                ammoMax = 15;
                ammoMax += item.tier * 5;
                ammunition = ammoMax;
                break;
            case ShipDefinitions.ItemType.CrownModRechargeRate:
                // cooldown is 3 by default
                // cooldown is 2.5 with tier 1 upgrade
                // cooldown is 2 with tier 2 upgrade
                // cooldown is 1 with tier 3 upgrade
                ammoCooldown = 3;
                ammoCooldown -= 0.5f * item.tier;
                break;
            case ShipDefinitions.ItemType.CrownModRange:
                // range is 2 by default
                // range is 2.5 with tier 1 upgrade
                // range is 3 with tier 2 upgrade
                // range is 4 with tier 3 upgrade
                float newDistance = 2.5f;
                if (item.tier == 2 || item.tier == 3)
                    newDistance = item.tier + 1;
                distance = newDistance;
                break;
            default:
                return;
        }
    }
}
