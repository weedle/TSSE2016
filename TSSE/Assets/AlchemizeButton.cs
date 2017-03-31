using UnityEngine;
using System.Collections;

public class AlchemizeButton : MonoBehaviour {
    public ItemListHandler blueprints;
    public Inventory inventory;
    public ItemAbstract item;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void enable()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = true;
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().color
            = Color.white;
    }

    public void disable()
    {
        gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
        transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().color
            = Color.grey;
    }

    public void alchemize()
    {
        if(blueprints.findItem(item))
        {
            int scrap = PlayerPrefs.GetInt("score");
            int cost = ItemDefinitions.getScrapCost(item);
            if (cost <= scrap)
            {
                inventory.addItem(item);
                inventory.addCurrency(-cost);
                inventory.updateCurrency();
            }
            //print("you alchemized an item!");
            // something with scrap lol
        }
    }

    public void destroy()
    {
        int cost = ItemDefinitions.getScrapCost(item);
        inventory.removeItem(item);
        inventory.addCurrency(cost);
        inventory.updateCurrency();
        // something with scrap lol
    }

    public void itemSelected(ItemAbstract newItem)
    {
        item = newItem;
        if (blueprints.findItem(item))
        {
            int scrap = PlayerPrefs.GetInt("score");
            if (ItemDefinitions.getScrapCost(item) <= scrap)
            {
                enable();
                return;
            }
        }
        disable();
    }
}
