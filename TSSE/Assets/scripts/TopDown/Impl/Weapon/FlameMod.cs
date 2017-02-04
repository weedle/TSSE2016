﻿using UnityEngine;
using System.Collections;

// The Ion Cannon firing module, in code treated as a flamethrower
// since that's initially what it was based on
public class FlameMod : MonoBehaviour, FiringModule
{
    private GameObject projectile;
    private float projectileSpeed = 20;
    private float spread = 0.03f;
    private int fireRate = 3;
    public int ammoMax = 15;
    public int ammunition = 15;
    public int ammoCooldown = 2;
    private Timer timer;

    // Use this for initialization
    void Start()
    {
        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        if(faction == ShipDefinitions.Faction.Enemy)
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getRedFlames();
        else
            projectile = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getBlueFlames();

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
            vec = new Vector3(0, (float)0.25, 0);
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

    public float getAmmoPerc()
    {
        return (float)ammunition / ammoMax;
    }

    public void applyBuff(ShipDefinitions.Item item)
    {

    }
}

