using UnityEngine;
using System.Collections;
using System;

public class PrefabHost : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject shipLabel;
    public GameObject particleRedFire;
    public GameObject particleBlueFire;
    public GameObject missile;
    public GameObject laser;
    public GameObject empty;

    public GameObject shipRuby;
    public GameObject engineLvl1;
    public GameObject engineLvl2;

    public GameObject getHealthObject()
    {
        GameObject obj = (GameObject)Instantiate(healthBar, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getLabelObject()
    {
        GameObject obj = (GameObject)Instantiate(shipLabel, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEmptyObject()
    {
        GameObject obj = (GameObject)Instantiate(empty, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipRubyObject()
    {
        GameObject obj = (GameObject)Instantiate(shipRuby, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngineLvl1Object()
    {
        GameObject obj = (GameObject)Instantiate(engineLvl1, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngineLvl2Object()
    {
        GameObject obj = (GameObject)Instantiate(engineLvl2, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    // Do not instantiate temporary objects here
    public GameObject getMissileObject()
    {
        //GameObject obj = (GameObject)Instantiate(missile, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return missile;
    }

    public GameObject getRedFlames()
    {
        //GameObject obj = (GameObject)Instantiate(particleRedFire, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return particleRedFire;
    }

    internal GameObject getBlueFlames()
    {
        return particleBlueFire;
    }

    public GameObject getLaser()
    {
        //GameObject obj = (GameObject)Instantiate(particleRedFire, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return laser;
    }
}
