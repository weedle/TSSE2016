using UnityEngine;
using System.Collections;
using System;

public class TargetFinder : MonoBehaviour {

    private GameObject GetClosestObject(String[] tags)
    {
        GameObject closest = null;
        foreach (String tag in tags)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in list)
            {
                if (closest == null || !gameObject.Equals(obj))
                    closest = obj;

                if(closest)
                    if (Vector3.Distance(transform.position, obj.transform.position) <=
                        Vector3.Distance(transform.position, closest.transform.position))
                    {
                        closest = obj;
                    }
            }
        }
        if (gameObject.Equals(closest)) return null;
        return closest;
    }
    public GameObject getTarget(ShipDefinitions.Faction faction)
    {
        if (faction == ShipDefinitions.Faction.Enemy)
        {
            string[] tags = { "Player", "PlayerAffil" };
            GameObject obj = GetClosestObject(tags);
            if (obj)
                return obj;
        }
        else if (faction == ShipDefinitions.Faction.PlayerAffil ||
                 faction == ShipDefinitions.Faction.Player)
        {
            string[] tags = { "Enemy" };
            GameObject obj = GetClosestObject(tags);
            if (obj)
                return obj;
        }
        return null;
    }

    public GameObject getFriendly(ShipDefinitions.Faction faction)
    {
        if (faction == ShipDefinitions.Faction.PlayerAffil ||
            faction == ShipDefinitions.Faction.Player)
        {
            string[] tags = { "Player", "PlayerAffil" };
            GameObject obj = GetClosestObject(tags);
            if (obj)
                return obj;
        }
        else if (faction == ShipDefinitions.Faction.Enemy)
        {
            string[] tags = { "Enemy" };
            GameObject obj = GetClosestObject(tags);
            if (obj)
                return obj;
        }
        return null;
    }
}
