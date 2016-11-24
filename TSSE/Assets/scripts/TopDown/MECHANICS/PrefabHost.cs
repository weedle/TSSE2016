using UnityEngine;
using System.Collections;

public class PrefabHost : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject shipLabel;
    public GameObject particleRedFire;
    public GameObject particleBlueFire;
    public GameObject missile;
    public GameObject empty;

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

    public GameObject getMissileObject()
    {
        GameObject obj = (GameObject)Instantiate(missile, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getRedFlames()
    {
        //GameObject obj = (GameObject)Instantiate(particleRedFire, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return particleRedFire;
    }
}
