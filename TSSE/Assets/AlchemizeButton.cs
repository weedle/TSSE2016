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
            inventory.addItem(item);
            print("you alchemized an item!");
            // something with scrap lol
        }
    }

    public void destroy()
    {
        inventory.removeItem(item);
        // something with scrap lol
    }

    public void itemSelected(ItemAbstract newItem)
    {
        item = newItem;
        if (blueprints.findItem(item))
        {
            enable();
        }
        else
        {
            disable();
        }
    }
}
