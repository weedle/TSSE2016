using UnityEngine;
using System.Collections;

// In progress weapon to heal other ships
// Fires tiny healing missiles at a decent rate
// In lore, those missiles are disposable repair drones
public class HealFiringModule : MonoBehaviour, FiringModule
{
    public int counter = 0;
    public Particle projectile;
    public float projectileSpeed = 20;
    public int ammoMax = 8;
    public int ammunition = 8;
    public int ammoCooldown = 40;
    public int immediateCooldown = 10;
    public int immediateCooldownMax = 10;

    // Use this for initialization
    void Start()
    {
        setFaction(ShipDefinitions.stringToFaction(gameObject.tag));
        projectileSpeed += Random.Range(-4, 4);
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

    // Fire missile
    public void fire()
    {
        if (ammunition > 0)
        {
            if (immediateCooldown <= immediateCooldownMax)
            {
                immediateCooldown++;
                return;
            }

            immediateCooldown = 0;
            Vector3 vec;
            Vector3 temp;
            Rigidbody2D proj;
            vec = new Vector3(0, (float)0.25, 0);
            vec = transform.rotation * vec;
            temp = new Vector3(transform.position.x, transform.position.y);
            proj = (Rigidbody2D)Instantiate(projectile.GetComponent<Rigidbody2D>(),
                temp + vec, Quaternion.Euler(0, 0, 90));
            temp = new Vector3(projectileSpeed * vec.x, projectileSpeed * vec.y, 0);
            proj.velocity = temp;
            proj.MoveRotation(transform.rotation.eulerAngles.z);
            ammunition--;
        }
    }

    // Effective distance is distance at which we should fire
    public float getEffectiveDistance()
    {
        return 4;
    }

    // Angle at which we should fire missiles
    public float getEffectiveAngle()
    {
        return 8;
    }

    // Set faction of the missiles so they know who to heal
    public void setFaction(ShipDefinitions.Faction faction)
    {
        projectile.faction = faction;
    }
    
    // can fire if we have ammunition and are not on cooldown
    public bool canFire()
    {
        if (ammunition > 0) return true;
        else return false;
    }
}

