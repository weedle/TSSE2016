using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// This is the cool scrolling itemview thing Jenne made
public class ItemListHandler : MonoBehaviour {
    // whichever item we selected, probably by clicking it
    public GameObject selectedItem = null;

    // if an item is selected, keep track of it
    public void itemSelected(GameObject obj)
    {
        UnityEngine.UI.Text indexItem = obj.transform.GetChild(0).
               GetComponent<UnityEngine.UI.Text>();
        selectedItem = obj;
    }
    
    // add the item
    // if it's already there, increase quantity
    public void addItem(ShipDefinitions.Item item)
    {
        // First, convert item to a string
        string itemText = ShipDefinitions.itemToString(item);

        // Now, search through items we have for a matching one
        // It's in the format "ObjectName (x)", so we trim the (x) part
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            Regex regex = new Regex(@"\w+");
            MatchCollection matches = regex.Matches(indexItem.text);
            // if this is the item:
            if (matches[0].ToString() + ":" + matches[1].ToString() == itemText)
            {
                // increase the quantity and update the label
                // then return cause we're done
                print(matches[1]);
                int x = int.Parse(matches[2].ToString());
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
    public void removeItem(ShipDefinitions.Item item)
    {
        // like in addItem, first we search for the item
        string itemText = ShipDefinitions.itemToString(item);
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            UnityEngine.UI.Text indexItem = transform.GetChild(0).GetChild(i).GetChild(0).
                GetComponent<UnityEngine.UI.Text>();
            Regex regex = new Regex(@"\w+");
            MatchCollection matches = regex.Matches(indexItem.text);
            print(matches[0].ToString() + " - " + matches[1].ToString());
            print("Text: " + itemText);
            // if it matches, we found it
            if (matches[0].ToString() + ":" + matches[1].ToString() == itemText)
            {
                // first figure out how many we now have
                int x = int.Parse(matches[2].ToString());
                print(x);
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
