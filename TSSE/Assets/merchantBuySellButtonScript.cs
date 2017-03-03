using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// This handles the UI for the merchant scene
public class merchantBuySellButtonScript : MonoBehaviour {

    // true if buying, false if selling
    // buying: item goes from merchant to player
    //          merchant gains money, player loses money
    // selling: item goes from player to merchant
    //          merchant loses money, player gains money
    public bool buying;

    // the object displaying the merchant's or player's inventory
    private ItemListHandler merchantItemList;
    private ItemListHandler playerItemList;

    // the actual inventory of the merchant or player
    // this is stored in an empty gameobject called "Merchant" or "Player"
    private Inventory merchantInventory;
    private Inventory playerInventory;

    // each merchant has a unique id that determines their inventory
    // and cost values: this is how merchants in different areas have
    // different things to sell and prices they'll buy things at
    private string merchantId;

    // player id could change, if, hypothetically, we want the player's
    // selling or buying prices to change at any point
    // maybe he gets a discount with some merchants after finishing a sector
    private string playerId;

    // if you press buy:
    // ShipDefinitions.getCost(Item item, String inventoryType, bool buying)
    // item = merchant.getSelectedObject -> item
    // inventoryType = "merchant"
    // bool buying = true

    // if you press sell:
    // item = player.getSelectedObject -> item
    // inventoryType = "player"
    // bool buying = false

    void Start()
    {
        // set all the objects
        merchantItemList = GameObject.Find("merchantItems").GetComponent<ItemListHandler>();
        playerItemList = GameObject.Find("myItems").GetComponent<ItemListHandler>();
        merchantInventory = GameObject.Find("Merchant").GetComponent<Inventory>();
        playerInventory = GameObject.Find("Player").GetComponent<Inventory>();

        merchantId = merchantInventory.uniqueId;
        playerId = playerInventory.uniqueId;
    }

    // handle buying or selling
    public void handleItem()
    {
        GameObject itemObj = null;

        // whichever object is being bought or sold
        if(buying)
            itemObj = merchantItemList.selectedItem;
        else
            itemObj = playerItemList.selectedItem;

        // if nothing is selected, return
        if (itemObj == null)
            return;

        // convert the text from that object to an item
        // It's of the form "ObjectName (x)" where x is a quantity
        ItemDefinitions.Item item;
        UnityEngine.UI.Text indexItem = itemObj.transform.GetChild(0).
            GetComponent<UnityEngine.UI.Text>();
        Regex regex = new Regex(@"\w+");
        MatchCollection matches = regex.Matches(indexItem.text);

        item = ItemDefinitions.stringToItem(matches[0].ToString() + ":" + 
            matches[1].ToString());

        int cost;

        // handle inventory and currency changes
        if (buying)
        {
            cost = ItemDefinitions.getCost(item, merchantId, buying);
            merchantInventory.addCurrency(cost);
            playerInventory.addCurrency(-cost);
            merchantItemList.removeItem(item);
            playerItemList.addItem(item);
            if (item.tier > 3)
            {
                BlueprintDefinitions.applyBlueprint(item);
                print("applying blueprint " + ItemDefinitions.itemToString(item));
            }
        }
        else
        {
            cost = ItemDefinitions.getCost(item, playerId, buying);
            merchantInventory.addCurrency(-cost);
            playerInventory.addCurrency(cost);
            playerItemList.removeItem(item);
            merchantItemList.addItem(item);
            if (item.tier > 3)
            {
                BlueprintDefinitions.deapplyBlueprint(item);
                print("deapplying blueprint " + ItemDefinitions.itemToString(item));
            }
        }
    }

    public void exit()
    {
        List<ItemDefinitions.Item> playerInventory = new List<ItemDefinitions.Item>();
        playerInventory.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.FlameModAmmoCap, 1));
        playerInventory.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.CrownModDamage, 3));
        playerInventory.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.LaserModAmmoCap, 5));

        ItemDefinitions.saveItems("Player", ItemDefinitions.itemsToString(playerInventory));

        List<ItemDefinitions.Item> merchantInventory = merchantItemList.getAllItems();

        ItemDefinitions.saveItems(merchantId, ItemDefinitions.itemsToString(merchantInventory));
    }
}
