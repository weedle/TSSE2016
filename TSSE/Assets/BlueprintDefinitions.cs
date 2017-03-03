using UnityEngine;
using System.Collections;

public class BlueprintDefinitions
{

    // For the demo blueprints will just be items with an invalid tier


    public static void applyBlueprint(ItemDefinitions.Item item)
    {
        PlayerPrefs.SetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]", 1);
    }

    public static void deapplyBlueprint(ItemDefinitions.Item item)
    {
        PlayerPrefs.SetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]", 0);
    }

    public static bool isBlueprintApplied(ItemDefinitions.Item item)
    {
        int applied = PlayerPrefs.GetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]");
        if (applied == 1)
            return true;
        return false;
    }

}
