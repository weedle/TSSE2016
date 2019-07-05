using UnityEngine;
using System.Collections;
using System;

public class PrefabHost : MonoBehaviour
{
    private GameObject healthBar;
    private GameObject ammoBar;
    private GameObject shipLabel;
    private GameObject particleRedFire;
    private GameObject particleBlueFire;
    private GameObject missile;
    private GameObject laser;
    private GameObject empty;

    private GameObject shipRuby;
    private GameObject shipRubyPirate;
    private GameObject shipPeacock;
    private GameObject shipPeacockPirate;
    private GameObject engineLvl1;
    private GameObject engineLvl2;

    private GameObject merchantItem;
    private GameObject inventoryItem;
    private GameObject shipyardInventoryItem;
    private GameObject iconItem;
    private GameObject questItem;
    private GameObject questEmptyItem;
    private GameObject eventItem;
    private GameObject eventEmptyItem;

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
        if (!healthBar)
            healthBar = (GameObject)Resources.Load("prefabs/HealthBar", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(healthBar, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getAmmoObject()
    {
        if (!ammoBar)
            ammoBar = (GameObject)Resources.Load("prefabs/AmmoBar", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(ammoBar, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getLabelObject()
    {
        if (!shipLabel)
            shipLabel = (GameObject)Resources.Load("prefabs/TextObj", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(shipLabel, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEmptyObject()
    {
        if (!empty)
            empty = (GameObject)Resources.Load("prefabs/EmptyObj", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(empty, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipRubyObject()
    {
        if (!shipRuby)
            shipRuby = (GameObject)Resources.Load("prefabs/shipRuby", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(shipRuby, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipRubyPirateObject()
    {
        if (!shipRubyPirate)
            shipRubyPirate = (GameObject)Resources.Load("prefabs/shipRubyPirate", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(shipRubyPirate, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipPeacockObject()
    {
        if (!shipPeacock)
            shipPeacock = (GameObject)Resources.Load("prefabs/shipPeacock", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(shipPeacock, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getShipPeacockPirateObject()
    {
        if (!shipPeacockPirate)
            shipPeacockPirate = (GameObject)Resources.Load("prefabs/shipPeacockPirate", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(shipPeacockPirate, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngineLvl1Object()
    {
        if (!engineLvl1)
            engineLvl1 = (GameObject)Resources.Load("prefabs/engineLvl1", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(engineLvl1, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEngineLvl2Object()
    {
        if (!engineLvl2)
            engineLvl2 = (GameObject)Resources.Load("prefabs/engineLvl2", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(engineLvl2, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getMerchantItem()
    {
        if (!merchantItem)
            merchantItem = (GameObject)Resources.Load("prefabs/shopItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(merchantItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getInventoryItem()
    {
        if (!inventoryItem)
            inventoryItem = (GameObject)Resources.Load("prefabs/inventoryItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(inventoryItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getIconItem()
    {
        if (!iconItem)
            iconItem = (GameObject)Resources.Load("prefabs/iconItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(iconItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getQuestItem()
    {
        if (!questItem)
            questItem = (GameObject)Resources.Load("prefabs/questItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(questItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }


    public GameObject getQuestEmptyItem()
    {
        if (!questEmptyItem)
            questEmptyItem = (GameObject)Resources.Load("prefabs/questEmptyItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(questEmptyItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEventItem()
    {
        if (!eventItem)
            eventItem = (GameObject)Resources.Load("prefabs/eventItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(eventItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getEventEmptyItem()
    {
        if (!eventEmptyItem)
            eventEmptyItem = (GameObject)Resources.Load("prefabs/eventEmptyItem", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(eventEmptyItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    // Do not instantiate temporary objects here
    public GameObject getMissileObject()
    {
        if (!missile)
            missile = (GameObject)Resources.Load("prefabs/missile", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(missile, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getRedFlames()
    {
        if (!particleRedFire)
            particleRedFire = (GameObject)Resources.Load("prefabs/FlameRed", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(particleRedFire, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    internal GameObject getBlueFlames()
    {
        if (!particleBlueFire)
            particleBlueFire = (GameObject)Resources.Load("prefabs/FlameBlue", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(particleBlueFire, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
    }

    public GameObject getLaser()
    {
        if (!laser)
            laser = (GameObject)Resources.Load("prefabs/laser", typeof(GameObject));

        GameObject obj = (GameObject)Instantiate(laser, Vector3.zero, Quaternion.Euler(0, 0, 0));
        return obj;
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
