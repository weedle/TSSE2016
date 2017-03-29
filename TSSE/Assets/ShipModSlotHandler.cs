using UnityEngine;
using System.Collections;

public class ShipModSlotHandler : MonoBehaviour {
    public GameObject shipImage;
    public int totalSlots;
    public GameObject slotR01; // 0
    public GameObject slotR02; // 1
    public GameObject slotR03; // 2
    public GameObject slotL01; // 3
    public GameObject slotL02; // 4
    public GameObject slotL03; // 5

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

    public void setShipItem(int index, ItemAbstract item)
    {
        ImageHost imgHost = GameObject.Find("GameLogic").GetComponent<ImageHost>();
        Sprite spr = imgHost.getImage(item);

        switch(index)
        {
            case 0:
                if (slotR01 == null)
                    return;
                slotR01.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
            case 2:
                if (slotL01 == null)
                    return;
                slotL01.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
            case 3:
                if (slotR02 == null)
                    return;
                slotR02.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
            case 4:
                if (slotL02 == null)
                    return;
                slotL02.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
            case 5:
                if (slotR03 == null)
                    return;
                slotR03.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
            case 6:
                if (slotL03 == null)
                    return;
                slotL03.GetComponent<UnityEngine.UI.Image>().sprite = spr;
                break;
        }
    }

    public void setShip(ShipDefinitions.ShipEntity entity)
    {
        setShipImage(entity.shipType);
        int index = 0;
        foreach(ItemAbstract item in entity.items)
        {
            setShipItem(index, item);
            index++;
            if (index >= totalSlots)
                return;
        }
    }
}
