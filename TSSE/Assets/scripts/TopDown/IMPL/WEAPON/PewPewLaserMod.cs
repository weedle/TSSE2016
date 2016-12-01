using UnityEngine;
using System.Collections;

// space game need laser gun
public class PewPewLaserMod : MonoBehaviour, FiringModule
{
    private int counter = 0;
    private GameObject projectile;
    public float projectileSpeed = 40;
    public int ammoMax = 4;
    public int ammunition = 4;
    public int ammoCooldown = 30;

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getLaser();
        //projectileSpeed += Random.Range(-4, 4);
        ammoMax += Random.Range(-1, 1);
        ammoCooldown += Random.Range(-10, 10);

        GameObject firingSprite = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getFiringSpriteObject();
        firingSprite.transform.parent = gameObject.transform;
        firingSprite.transform.position = new Vector3(
            gameObject.transform.position.x,
            gameObject.transform.position.y - 0.07f,
            gameObject.transform.position.z);
        firingSprite.GetComponent<FiringSprite>()
            .setSprite(faction, "laser");
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

    // Fire a small burst of flaes
    public void fire()
    {
        if (ammunition > 0)
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
}

