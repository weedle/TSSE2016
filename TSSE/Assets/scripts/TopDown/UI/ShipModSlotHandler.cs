using UnityEngine;
using System.Collections;

public class ShipModSlotHandler : MonoBehaviour {
    public GameObject shipImage;
    public int totalSlots;
    public GameObject slotL01; // 1
    public GameObject slotR01; // 2
    public GameObject slotL02; // 3
    public GameObject slotR02; // 4
    public GameObject slotL03; // 5
    public GameObject slotR03; // 6

    public ShipDefinitions.ShipEntity thisEntity;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setShipImage(ShipDefinitions.ShipType type)
    {
        ImageHost imgHost = GameObject.Find("GameLogic").GetComponent<ImageHost>();
        switch(type)
        {
            case ShipDefinitions.ShipType.Ruby:
                shipImage.GetComponent<UnityEngine.UI.Image>().sprite = imgHost.shipRuby;
                break;
            case ShipDefinitions.ShipType.Peacock:
                shipImage.GetComponent<UnityEngine.UI.Image>().sprite = imgHost.shipPeacock;
                break;
        }
    }

    private GameObject getItemObj(ItemAbstract item)
    {
        /*
        ImageHost imgHost = GameObject.Find("GameLogic").GetComponent<ImageHost>();
        Sprite spr = imgHost.getImage(item);
        GameObject itemObj = GameObject.Find("GameLogic").GetComponent<PrefabHost>()
            .getIconItem();
        itemObj.GetComponent<ItemBasic>().itemString = item.toString();
        itemObj.GetComponent<ItemBasic>().itemQuantity = 1;
        itemObj.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>()
    .sprite = imgHost.getImage(item);
        return itemObj;
        */
        return null;
    }

    private void setChildItem(GameObject slotItem, ItemAbstract item)
    {
        /*
        //print(slotItem.transform.childCount);
        if (slotItem.transform.childCount == 0)
        {
            thisEntity.items.Add(item);
            GameObject.Find("Inventory").GetComponent<Inventory>()
                .removeItem(item);
            GameObject itemObj = getItemObj(item);
            itemObj.transform.SetParent(slotItem.transform);
            itemObj.transform.localScale = new Vector3(1, 1, 1);
        }
        ShipDefinitions.saveShip(thisEntity);
        print("saving ship: " + ShipDefinitions.shipToString(thisEntity));
        */
    }

    public void setShipItem(ItemAbstract item)
    {
        /*
        thisEntity.items.Add(item);
        ShipDefinitions.saveShip(thisEntity);
        */
    }

    public void setShipItem(int index, ItemAbstract item)
    {
        /*
        switch (index)
        {
            case 0:
                if (slotL01 == null)
                    return;
                setChildItem(slotL01, item);
                break;
            case 1:
                if (slotR01 == null)
                    return;
                setChildItem(slotR01, item);
                break;
            case 2:
                if (slotL02 == null)
                    return;
                setChildItem(slotL02, item);
                break;
            case 3:
                if (slotR02 == null)
                    return;
                setChildItem(slotR02, item);
                break;
            case 4:
                if (slotL03 == null)
                    return;
                setChildItem(slotL03, item);
                break;
            case 5:
                if (slotR03 == null)
                    return;
                setChildItem(slotR03, item);
                break;
        }
        */
    }

    public void setShip(ShipDefinitions.ShipEntity entity)
    {
        print("setting ship");
        //clearItemsInSlots();
        thisEntity = entity;
        setShipImage(entity.shipType);
        /*
        int index = 0;
        foreach(ItemAbstract item in entity.items)
        {
            if (item.getType() == "Error")
                continue;
            print(index + " " + item.toString());
            setShipItem(index, item);
            GameObject.Find("Inventory").GetComponent<Inventory>()
                .removeItem(item);
            index++;
            if (index > totalSlots)
                return;
        }
        */
    }

    public void clearItemsInSlot(int slot)
    {
        /*
        switch(slot)
        {
            case 0:
                if(slotL01.transform.childCount != 0)
                {
                    Destroy(slotL01.transform.GetChild(0).gameObject);
                }
                break;
            case 1:
                if (slotR01.transform.childCount != 0)
                {
                    Destroy(slotR01.transform.GetChild(0).gameObject);
                }
                break;
            case 2:
                if (slotL02.transform.childCount != 0)
                {
                    Destroy(slotL02.transform.GetChild(0).gameObject);
                }
                break;
            case 3:
                if (slotR02.transform.childCount != 0)
                {
                    Destroy(slotR02.transform.GetChild(0).gameObject);
                }
                break;
            case 4:
                if (slotL03.transform.childCount != 0)
                {
                    Destroy(slotL03.transform.GetChild(0).gameObject);
                }
                break;
            case 5:
                if (slotR03.transform.childCount != 0)
                {
                    Destroy(slotR03.transform.GetChild(0).gameObject);
                }
                break;
        }
        */
    }

    public void clearItemsInSlots()
    {
        /*
        clearItemsInSlot(0);
        clearItemsInSlot(1);
        if(slotL02 != null)
        {
            clearItemsInSlot(2);
            clearItemsInSlot(3);
        }
        if (slotL03 != null)
        {
            clearItemsInSlot(4);
            clearItemsInSlot(5);
        }
        */
    }
}
