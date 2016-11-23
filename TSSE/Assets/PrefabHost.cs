using UnityEngine;
using System.Collections;

public class PrefabHost : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject shipLabel;
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
}
