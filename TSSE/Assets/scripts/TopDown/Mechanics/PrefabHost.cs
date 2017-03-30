using UnityEngine;
using System.Collections;
using System;

public class PrefabHost : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject ammoBar;
    public GameObject shipLabel;
    public GameObject particleRedFire;
    public GameObject particleBlueFire;
    public GameObject missile;
    public GameObject laser;
    public GameObject empty;

    public GameObject shipRuby;
    public GameObject shipRubyPirate;
    public GameObject shipPeacock;
    public GameObject shipPeacockPirate;
    public GameObject engineLvl1;
    public GameObject engineLvl2;

    public GameObject merchantItem;
    public GameObject inventoryItem;
    public GameObject shipyardInventoryItem;
    public GameObject iconItem;
    public GameObject questItem;
    public GameObject questEmptyItem;
    public GameObject eventItem;
    public GameObject eventEmptyItem;

    public Sprite shipRubyPirateSprite;
    public Sprite shipPeacockPirateSprite;
    public Sprite shipRubySprite;
    public Sprite shipPeacockSprite;

    public RuntimeAnimatorController shipRubyPirateAnimator;
    public RuntimeAnimatorController shipPeacockPirateAnimator;
    public RuntimeAnimatorController shipRubyAnimator;
    public RuntimeAnimatorController shipPeacockAnimator;

    public GameObject shipDisplayObject;
    public GameObject shipyardDisplayObject;
    public GameObject engine1HD;
    public GameObject engine2HD;

    public GameObject shipyardTwoSlots;
    public GameObject shipyardFourSlots;
    public GameObject shipyardSixSlots;

    public GameObject shipyardBlueprint;

    public GameObject getHealthObject()
    {
        GameObject obj = (GameObject)Instantiate(healthBar, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getAmmoObject()
    {
        GameObject obj = (GameObject)Instantiate(ammoBar, Vector3.zero, Quaternion.Euler(0, 0, 0));
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

    public GameObject getShipRubyPirateObject()
    {
        GameObject obj = (GameObject)Instantiate(shipRubyPirate, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipPeacockObject()
    {
        GameObject obj = (GameObject)Instantiate(shipPeacock, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipPeacockPirateObject()
    {
        GameObject obj = (GameObject)Instantiate(shipPeacockPirate, Vector3.zero, Quaternion.Euler(0, 0, 0));
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

    public GameObject getMerchantItem()
    {
        GameObject obj = (GameObject)Instantiate(merchantItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getInventoryItem()
    {
        GameObject obj = (GameObject)Instantiate(inventoryItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getIconItem()
    {
        GameObject obj = (GameObject)Instantiate(iconItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getQuestItem()
    {
        GameObject obj = (GameObject)Instantiate(questItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }


    public GameObject getQuestEmptyItem()
    {
        GameObject obj = (GameObject)Instantiate(questEmptyItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEventItem()
    {
        GameObject obj = (GameObject)Instantiate(eventItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEventEmptyItem()
    {
        GameObject obj = (GameObject)Instantiate(eventEmptyItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
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
        return laser;
    }

    public GameObject getShipDisplayObject()
    {
        GameObject obj = (GameObject)Instantiate(shipDisplayObject, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngine1HD()
    {
        GameObject obj = (GameObject)Instantiate(engine1HD, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngine2HD()
    {
        GameObject obj = (GameObject)Instantiate(engine2HD, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipyardDisplayObject()
    {
        GameObject obj = (GameObject)Instantiate(shipyardDisplayObject, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipyardTwoSlots()
    {
        GameObject obj = (GameObject)Instantiate(shipyardTwoSlots, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipyardFourSlots()
    {
        GameObject obj = (GameObject)Instantiate(shipyardFourSlots, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipyardSixSlots()
    {
        GameObject obj = (GameObject)Instantiate(shipyardSixSlots, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getBlueprintItem()
    {
        GameObject obj = (GameObject)Instantiate(shipyardBlueprint, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }
}
