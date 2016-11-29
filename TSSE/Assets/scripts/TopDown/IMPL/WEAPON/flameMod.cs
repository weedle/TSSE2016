using UnityEngine;
using System.Collections;

// The Ion Cannon firing module, in code treated as a flamethrower
// since that's initially what it was based on
public class FlameMod : MonoBehaviour, FiringModule
{
    public int counter = 0;
    private GameObject projectile;
    public float projectileSpeed = 20;
    public int ammoMax = 15;
    public int ammunition = 15;
    public int ammoCooldown = 80;

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        if(faction == ShipDefinitions.Faction.Enemy)
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getRedFlames();
        else
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getBlueFlames();
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

    // Fire a small burst of flaes
    public void fire()
    {
        if (ammunition > 0)
        {
            Vector3 vec;
            Vector3 temp;
            GameObject proj;
            vec = new Vector3(0, (float)0.25, 0);
            vec = transform.rotation * vec;
            for (int i = 0; i <= 3; i++)
            {
                temp = new Vector3(transform.position.x, transform.position.y);
                proj = (GameObject)Instantiate(projectile,
                    temp + vec, Quaternion.Euler(0, 0, 90));
                proj.GetComponent<Particle>().setFaction(
                    ShipDefinitions.stringToFaction(
                        gameObject.GetComponent<ShipController>().getFaction().ToString()));
                temp = new Vector3(projectileSpeed * 
                    (vec.x + Random.Range(-0.03f, 0.03f)), projectileSpeed * 
                    (vec.y + Random.Range(-0.03f, 0.03f)), 0);
                proj.GetComponent<Rigidbody2D>().velocity = temp;
                //proj.MoveRotation(proj.transform.rotation.eulerAngles.z
                //    + Random.Range(-15, 15));
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
}

