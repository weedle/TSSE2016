using UnityEngine;
using System.Collections;

// Firing Module that shoots damaging homing missiles
public class MissileMod : MonoBehaviour, FiringModule
{
    public int counter = 0;
    private GameObject projectile;
    public float projectileSpeed = 20;
    public int ammoMax = 3;
    public int ammunition = 3;
    public int ammoCooldown = 120;
    public int immediateCooldown = 30;
    public int immediateCooldownMax = 30;

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getMissileObject();
        projectileSpeed += Random.Range(-5, 5);
        ammoMax += Random.Range(-1, 1);
        ammoCooldown += Random.Range(-20, 20);

        GameObject firingSprite = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getFiringSpriteObject();
        firingSprite.transform.parent = gameObject.transform;
        firingSprite.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - 0.07f,
            gameObject.transform.position.z);
        firingSprite.GetComponent<FiringSprite>()
            .setSprite(faction, "crown");
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

    // Fire a missile
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
            proj.GetComponent<Particle>().setFaction(ShipDefinitions.stringToFaction(gameObject.tag));

            ammunition--;
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

    public void applyBuff(Inventory.Item item)
    {

    }
}
