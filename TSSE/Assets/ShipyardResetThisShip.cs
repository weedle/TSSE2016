using UnityEngine;
using System.Collections;

public class ShipyardResetThisShip : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void resetShip()
    {
        ShipDefinitions.ShipEntity entity
            = GetComponentInParent<ShipBasic>().entity;
        entity = ShipDefinitions.loadShip(entity.uniqueId);
        entity.items.Clear();
        ShipDefinitions.saveShip(entity);

        // I am so sorry
        GameObject.Find("ship-info-textbox").GetComponent<ShipyardInfoViewHandler>()
            .updateEntity();
    }
}
