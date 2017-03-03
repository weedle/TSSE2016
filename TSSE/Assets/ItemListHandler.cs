using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// This is the cool scrolling itemview thing Jenne made
public class ItemListHandler : MonoBehaviour {
    // whichever item we selected, probably by clicking it
    public GameObject selectedItem = null;
    public string mode = "merchant";
    private ImageHost imgHost;

    // if an item is selected, keep track of it
    public void itemSelected(GameObject obj)
    {
        UnityEngine.UI.Text indexItem = obj.transform.GetChild(0).
               GetComponent<UnityEngine.UI.Text>();
        ItemDefinitions.Item item = ItemDefinitions.stringToItem(
            indexItem.text.Substring(0, indexItem.text.Length - 4));
        GameObject imgObj = GameObject.Find("itemImage");

        UnityEngine.UI.Image img = null;
        if(imgObj != null)
            img = imgObj.GetComponent<UnityEngine.UI.Image>();
        if (img != null)
        {
            img.sprite = getImage(item);
        }
        GameObject panel1 = GameObject.Find("panelText01");
        if(panel1 != null)
            panel1.GetComponent<UnityEngine.UI.Text>()
            .text = ItemDefinitions.getDesc(item);
        GameObject panel2 = GameObject.Find("panelText02");
        if (panel2 != null)
            panel2.GetComponent<UnityEngine.UI.Text>()
            .text = ItemDefinitions.getSpec(item);
        selectedItem = obj;

    }

    public Sprite getImage(ItemDefinitions.Item item)
    {
        imgHost = GameObject.Find("GameLogic").GetComponent<ImageHost>();
        Sprite img = imgHost.defaultSprite;
        if (item.type == ItemDefinitions.ItemType.FlameModAmmoCap ||
                item.type == ItemDefinitions.ItemType.FlameModDamage ||
                item.type == ItemDefinitions.ItemType.FlameModFireRate ||
                item.type == ItemDefinitions.ItemType.FlameModRange ||
                item.type == ItemDefinitions.ItemType.FlameModRechargeRate ||
                item.type == ItemDefinitions.ItemType.FlameModSpeed ||
                item.type == ItemDefinitions.ItemType.FlameModSpread ||
                item.type == ItemDefinitions.ItemType.FlameModFireRate ||
                item.type == ItemDefinitions.ItemType.FlameModRange ||
                item.type == ItemDefinitions.ItemType.FlameModRechargeRate ||
                item.type == ItemDefinitions.ItemType.FlameModSpeed ||
                item.type == ItemDefinitions.ItemType.FlameModSpread)
        {
            switch (item.tier)
            {
                case 1:
                    img = imgHost.fireSpriteLvl1;
                    break;
                case 2:
                    img = imgHost.fireSpriteLvl2;
                    break;
                case 3:
                    img = imgHost.fireSpriteLvl3;
                    break;
                case 4:
                    img = imgHost.blueprintFireSpriteLvl1;
                    break;
                case 5:
                    img = imgHost.blueprintFireSpriteLvl2;
                    break;
                case 6:
                    img = imgHost.blueprintFireSpriteLvl3;
                    break;
            }
        }
        else if (item.type == ItemDefinitions.ItemType.LaserModAmmoCap ||
            item.type == ItemDefinitions.ItemType.LaserModDamage ||
            item.type == ItemDefinitions.ItemType.LaserModFireRate ||
            item.type == ItemDefinitions.ItemType.LaserModRange ||
            item.type == ItemDefinitions.ItemType.LaserModRechargeRate ||
            item.type == ItemDefinitions.ItemType.LaserModSpeed)
        {
            switch (item.tier)
            {
                case 1:
                    img = imgHost.laserSpriteLvl1;
                    break;
                case 2:
                    img = imgHost.laserSpriteLvl2;
                    break;
                case 3:
                    img = imgHost.laserSpriteLvl3;
                    break;
                case 4:
                    img = imgHost.blueprintLaserSpriteLvl1;
                    break;
                case 5:
                    img = imgHost.blueprintLaserSpriteLvl2;
                    break;
                case 6:
                    img = imgHost.blueprintLaserSpriteLvl3;
                    break;
            }
        }
        else if (item.type == ItemDefinitions.ItemType.CrownModAmmoCap ||
            item.type == ItemDefinitions.ItemType.CrownModDamage ||
            item.type == ItemDefinitions.ItemType.CrownModRange ||
            item.type == ItemDefinitions.ItemType.CrownModRechargeRate)
        {
            switch (item.tier)
            {
                case 1:
                    img = imgHost.crownSpriteLvl1;
                    break;
                case 2:
                    img = imgHost.crownSpriteLvl2;
                    break;
                case 3:
                    img = imgHost.crownSpriteLvl3;
                    break;
                case 4:
                    img = imgHost.blueprintCrownSpriteLvl1;
                    break;
                case 5:
                    img = imgHost.blueprintCrownSpriteLvl2;
                    break;
                case 6:
                    img = imgHost.blueprintCrownSpriteLvl3;
                    break;
            }
        }
        else if (item.type == ItemDefinitions.ItemType.MissileModAmmoCap ||
            item.type == ItemDefinitions.ItemType.MissileModDamage ||
            item.type == ItemDefinitions.ItemType.MissileModFireRate ||
            item.type == ItemDefinitions.ItemType.MissileModRange ||
            item.type == ItemDefinitions.ItemType.MissileModRechargeRate ||
            item.type == ItemDefinitions.ItemType.MissileModSpeed)
        {
            switch (item.tier)
            {
                case 1:
                    img = imgHost.missileSpriteLvl1;
                    break;
                case 2:
                    img = imgHost.missileSpriteLvl2;
                    break;
                case 3:
                    img = imgHost.missileSpriteLvl3;
                    break;
                case 4:
                    img = imgHost.blueprintMissileSpriteLvl1;
                    break;
                case 5:
                    img = imgHost.blueprintMissileSpriteLvl2;
                    break;
                case 6:
                    img = imgHost.blueprintMissileSpriteLvl3;
                    break;
            }
        }
        else
        {
            img = imgHost.defaultSprite;
        }
        return img;
    }
    
    // add the item
    // if it's already there, increase quantity
    public void addItem(ItemDefinitions.Item item)
    {
        // First, convert item to a string
        string itemText = ItemDefinitions.itemToString(item);

        // Now, search through items we have for a matching one
        // It's in the format "ObjectName (x)", so we trim the (x) part
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            string labelItemText = indexItem.text.Substring(0, indexItem.text.Length-4);
            int numItems = int.Parse(indexItem.text.Substring(indexItem.text.Length - 2, 1));
            // if this is the item:
            if (labelItemText == itemText)
            {
                // increase the quantity and update the label
                // then return cause we're done
                int x = numItems;
                x++;

                indexItem.text = itemText + " (" + x + ")";
                return;
            }
        }

        // We haven't returned earlier, so this must be a new item
        // So, we create a new thing and add to the listview
        // We have an example item in the PrefabHost
        GameObject newItem;
        if (mode != "merchant")
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getInventoryItem();
        }
        else
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getMerchantItem();
        }

        // Initially has 1 object
        newItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().
            text = itemText + " (1)";

        // Add to listview
        newItem.transform.SetParent(transform.GetChild(0).transform, false);

        // This is so we call itemSelected if we click an item here
        newItem.GetComponent<UnityEngine.UI.Button>()
            .onClick.AddListener(delegate () { itemSelected(newItem); });

        GetComponent<UnityEngine.UI.ScrollRect>().verticalNormalizedPosition = 1;
    }

    // reduce quantity if present, otherwise remove item
    public void removeItem(ItemDefinitions.Item item)
    {
        // like in addItem, first we search for the item
        string itemText = ItemDefinitions.itemToString(item);
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            string labelItemText = indexItem.text.Substring(0, indexItem.text.Length - 4);
            int numItems = int.Parse(indexItem.text.Substring(indexItem.text.Length - 2, 1));
            // if it matches, we found it
            if (labelItemText == itemText)
            {
                // first figure out how many we now have
                int x = numItems;
                x--;

                // we have none left, so remove this item
                if(x == 0)
                {
                    GameObject.Destroy(indexItem.transform.parent.gameObject);
                    return;
                }

                // refresh the label
                indexItem.text = itemText + " (" + x + ")";
                return;
            }
        }
    }

    // reduce quantity if present, otherwise remove item
    public void removeAllItems()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }

    public List<ItemDefinitions.Item> getAllItems()
    {
        List<ItemDefinitions.Item> retItems = new List<ItemDefinitions.Item>();

        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            string labelItemText = indexItem.text.Substring(0, indexItem.text.Length - 4);
            int numItems = int.Parse(indexItem.text.Substring(indexItem.text.Length - 2, 1));
            ItemDefinitions.Item item = ItemDefinitions.stringToItem(labelItemText);
            for (int j = 0; j < numItems; j++)
            {
                retItems.Add(item);
            }
        }

        return retItems;
    }
}
