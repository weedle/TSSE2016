using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// This is the cool scrolling itemview thing Jenne made
public class ItemListHandler : MonoBehaviour {
    // whichever item we selected, probably by clicking it
    public GameObject selectedItem = null;
    public string mode = "merchant";
    public Sprite defaultSprite;
    public Sprite fireSpriteLvl1;
    public Sprite fireSpriteLvl2;
    public Sprite fireSpriteLvl3;
    public Sprite laserSpriteLvl1;
    public Sprite laserSpriteLvl2;
    public Sprite laserSpriteLvl3;
    public Sprite crownSpriteLvl1;
    public Sprite crownSpriteLvl2;
    public Sprite crownSpriteLvl3;
    public Sprite missileSpriteLvl1;
    public Sprite missileSpriteLvl2;
    public Sprite missileSpriteLvl3;

    // if an item is selected, keep track of it
    public void itemSelected(GameObject obj)
    {
        UnityEngine.UI.Text indexItem = obj.transform.GetChild(0).
               GetComponent<UnityEngine.UI.Text>();
        ItemDefinitions.Item item = ItemDefinitions.stringToItem(
            indexItem.text.Substring(0, indexItem.text.Length - 4));
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
                GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                    sprite = fireSpriteLvl1;
                    break;
                case 2:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = fireSpriteLvl2;
                    break;
                case 3:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = fireSpriteLvl3;
                    break;
            }
        }
        else if(item.type == ItemDefinitions.ItemType.LaserModAmmoCap ||
            item.type == ItemDefinitions.ItemType.LaserModDamage ||
            item.type == ItemDefinitions.ItemType.LaserModFireRate ||
            item.type == ItemDefinitions.ItemType.LaserModRange ||
            item.type == ItemDefinitions.ItemType.LaserModRechargeRate ||
            item.type == ItemDefinitions.ItemType.LaserModSpeed)
        {
            switch (item.tier)
            {
                case 1:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = laserSpriteLvl1;
                    break;
                case 2:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = laserSpriteLvl2;
                    break;
                case 3:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = laserSpriteLvl3;
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
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = crownSpriteLvl1;
                    break;
                case 2:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = crownSpriteLvl2;
                    break;
                case 3:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = crownSpriteLvl3;
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
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = missileSpriteLvl1;
                    break;
                case 2:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = missileSpriteLvl2;
                    break;
                case 3:
                    GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                        sprite = missileSpriteLvl3;
                    break;
            }
        }
        else
        {
            GameObject.Find("itemImage").GetComponent<UnityEngine.UI.Image>().
                sprite = defaultSprite;
        }
        GameObject.Find("panelText01").GetComponent<UnityEngine.UI.Text>()
            .text = ItemDefinitions.getDesc(item);
        GameObject.Find("panelText02").GetComponent<UnityEngine.UI.Text>()
            .text = ItemDefinitions.getSpec(item);
        selectedItem = obj;
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
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getMerchantItem();
        if (mode != "merchant")
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getInventoryItem();
        }

        // Initially has 1 object
        newItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().
            text = itemText + " (1)";

        // Add to listview
        newItem.transform.SetParent(transform.GetChild(0).transform, false);

        // This is so we call itemSelected if we click an item here
        newItem.GetComponent<UnityEngine.UI.Button>()
            .onClick.AddListener(delegate () { itemSelected(newItem); });
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
}
