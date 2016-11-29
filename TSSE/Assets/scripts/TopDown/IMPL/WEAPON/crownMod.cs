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
    public int counter = 0;
    public int ammoMax = 15;
    public int ammunition = 15;
    public int ammoCooldown = 120;

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

        ammoMax += Random.Range(-4, 4);
        ammoCooldown += Random.Range(-20, 20);
    }

    // Update is called once per frame
    void Update()
    {
        counter++;
        if (counter >= ammoCooldown)
        {
            if (ammunition < ammoMax)
            {
                ammunition = ammoMax;
                counter = 0;
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
            target.GetComponent<ShipIntf>().isHit(2);
            ammunition--;
        }
        target = null;
    }

    // Effective distance is the range at which we will fire
    // Note that given the way this works, this is also the actual range of the weapon
    public float getEffectiveDistance()
    {
        return 3;
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
}
