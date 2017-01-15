using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public enum Item
    {
        FlameModDamage, FlameModFireRate, FlameModeSpread
    }

    private List<Item> playerInventory = new List<Item>();

    public List<Item> getPlayerInventory()
    {
        return playerInventory;
    }

    public void addItem(Item item)
    {
        playerInventory.Add(item);
    }

    public void removeItem(Item item)
    {
        playerInventory.Remove(item);
    }
}
