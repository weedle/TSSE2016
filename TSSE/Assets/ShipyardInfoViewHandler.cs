using UnityEngine;
using System.Collections;

public class ShipyardInfoViewHandler : MonoBehaviour {
    private ShipDefinitions.ShipEntity thisEntity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setShipInfo(ShipDefinitions.ShipEntity entity)
    {
        thisEntity = entity;
        UnityEngine.UI.Text textThing = transform.GetChild(0).GetComponent<UnityEngine.UI.Text>();
        string info = "Current Ship: " + entity.uniqueId + "\n";
        info += "Ship type: " + entity.shipType.ToString() + "\n";
        info += "Engine type: " + entity.engType.ToString() + "\n";
        info += "Weapon type: " + entity.weapType.ToString() + "\n";
        info += "Items: ";
        foreach(ItemAbstract item in entity.items)
        {
            if (item.getType() == "Error")
                continue;
            info += item.toString() + " ";
        }
        textThing.text = info;
    }

    public void updateEntity()
    {
        thisEntity = ShipDefinitions.loadShip(thisEntity.uniqueId);
        setShipInfo(thisEntity);
    }
}
