using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// This is the cool scrolling itemview thing Jenne made
public class ShipListHandler : MonoBehaviour
{
    public bool shipyard = false;
    public GameObject slotsTwo;
    public GameObject slotsFour;
    public GameObject slotsSix;
    public GameObject shipInfoHandler;
    void Start()
    {
    }

    void Update()
    {
        if(selectedShip != null)
        {
        }
    }

    // selected ship
    public GameObject selectedShip = null;

    // if an item is selected, keep track of it
    public void shipSelected(GameObject obj)
    {
        print("selected ship!");
        selectedShip = obj;
        ShipDefinitions.ShipEntity updatedEntity =
            ShipDefinitions.loadShip(obj.GetComponent<ShipBasic>().entity.uniqueId);
        shipInfoHandler.GetComponent<ShipyardInfoViewHandler>()
            .setShipInfo(updatedEntity);
    }

    // add the ship
    public void addShip(ShipDefinitions.ShipEntity entity)
    {
        print("adding ship: " + entity.uniqueId);
        // We haven't returned earlier, so this must be a new item
        // So, we create a new thing and add to the listview
        // We have an example item in the PrefabHost
        GameObject newShip;
        if (shipyard)
        {
            newShip = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getShipyardDisplayObject();
            newShip.GetComponent<ShipBasic>().entity = entity;
            switch(entity.shipType)
            {
                case ShipDefinitions.ShipType.Ruby:
                    newShip.transform.GetChild(1).GetComponent<makePop>().popUp = slotsTwo;
                    break;
                case ShipDefinitions.ShipType.Peacock:
                    newShip.transform.GetChild(1).GetComponent<makePop>().popUp = slotsFour;
                    break;
            }
        }
        else {
            newShip = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getShipDisplayObject();
            newShip.GetComponent<ShipBasic>().entity = entity;
        }
        GameObject newEngine = null;


        switch(entity.shipType)
        {
            case ShipDefinitions.ShipType.Ruby:
                newShip.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>()
                    .sprite = GameObject.Find("GameLogic").GetComponent<ImageHost>().shipRuby;
                break;
            case ShipDefinitions.ShipType.Peacock:
                newShip.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>()
                    .sprite = GameObject.Find("GameLogic").GetComponent<ImageHost>().shipPeacock;
                break;
            case ShipDefinitions.ShipType.None:
                return;
        }
        /*
        switch(entity.engType)
        {
            case ShipDefinitions.EngineType.Engine1:
                newEngine = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getEngine1HD();

                newEngine.transform.SetParent(newShip.transform, false);
                newEngine.transform.localPosition = new Vector3(184, 80, 0);
                newEngine.transform.localScale = new Vector3(100, 100, 0);

                newEngine.transform.GetComponent<SpriteRenderer>().enabled = true;
                newEngine.transform.GetComponent<Animator>().Play("Active");
                break;
            case ShipDefinitions.EngineType.Engine2:
                newEngine = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getEngine2HD();

                newEngine.transform.SetParent(newShip.transform, false);
                newEngine.transform.localPosition = new Vector3(184, 47, 0);
                newEngine.transform.localScale = new Vector3(100, 100, 0);

                newEngine.transform.GetComponent<SpriteRenderer>().enabled = true;
                newEngine.transform.GetComponent<Animator>().Play("Active");
                break;
        }
        */
        // Add to listview
        newShip.transform.SetParent(transform.GetChild(0).transform, false);

        // This is so we call itemSelected if we click an item here
        newShip.GetComponent<UnityEngine.UI.Button>()
            .onClick.AddListener(delegate () { shipSelected(newShip); });

        GetComponent<UnityEngine.UI.ScrollRect>().verticalNormalizedPosition = 1;
    }

    // reduce quantity if present, otherwise remove item
    public void removeShip(ShipDefinitions.ShipEntity entity)
    {
    }

    public void removeAllItems()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public List<ItemAbstract> getAllItems()
    {
        List<ItemAbstract> retItems = new List<ItemAbstract>();

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            string labelItemText = indexItem.text.Substring(0, indexItem.text.Length - 4);
            int numItems = int.Parse(indexItem.text.Substring(indexItem.text.Length - 2, 1));
            ItemAbstract item = ItemDefinitions.stringToItem(labelItemText);
            for (int j = 0; j < numItems; j++)
            {
                retItems.Add(item);
            }
        }

        return retItems;
    }
}
