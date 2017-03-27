using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// This is the cool scrolling itemview thing Jenne made
public class ItemListHandler : MonoBehaviour {
    // whichever item we selected, probably by clicking it
    public GameObject selectedItem = null;
    public string mode = "merchant";
    public bool iconsMode = false;

    // if an item is selected, keep track of it
    public void itemSelected(GameObject obj)
    {
        UnityEngine.UI.Text indexItem = obj.transform.GetChild(0).
               GetComponent<UnityEngine.UI.Text>();
        ItemAbstract item = ItemDefinitions.stringToItem(
            indexItem.text.Substring(0, indexItem.text.Length - 4));

        if (!iconsMode)
        {
            GameObject imgObj = GameObject.Find("itemImage");

            UnityEngine.UI.Image img = null;
            if (imgObj != null)
                img = imgObj.GetComponent<UnityEngine.UI.Image>();
            if (img != null)
            {
                img.sprite = getImage(item);
            }
            GameObject panel1 = GameObject.Find("panelText01");
            if (panel1 != null)
                panel1.GetComponent<UnityEngine.UI.Text>()
                .text = ItemDefinitions.getDesc(item);
            GameObject panel2 = GameObject.Find("panelText02");
            if (panel2 != null)
                panel2.GetComponent<UnityEngine.UI.Text>()
                .text = ItemDefinitions.getSpec(item);
            selectedItem = obj;
        }
    }

    public Sprite getImage(ItemAbstract item)
    {
        ImageHost imgHost = GameObject.Find("GameLogic").GetComponent<ImageHost>();
        return imgHost.getImage(item);
    }
    
    // add the item
    // if it's already there, increase quantity
    public void addItem(ItemAbstract item)
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
        if (mode == "inventory")
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getInventoryItem();
        }
        else if (mode == "merchant")
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getMerchantItem();
        }
        else
        {
            newItem = GameObject.Find("GameLogic").
                GetComponent<PrefabHost>().getIconItem();
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
    public void removeItem(ItemAbstract item)
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
