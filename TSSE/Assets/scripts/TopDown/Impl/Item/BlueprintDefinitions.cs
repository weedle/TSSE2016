using UnityEngine;
using System.Collections;

public class BlueprintDefinitions
{
    public static void applyBlueprint(ItemAbstract item)
    {
        PlayerPrefs.SetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]", 1);
    }

    public static void deapplyBlueprint(ItemAbstract item)
    {
        PlayerPrefs.SetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]", 0);
    }

    public static bool isBlueprintApplied(ItemAbstract item)
    {
        int applied = PlayerPrefs.GetInt("TSSEList[Blueprint][" + ItemDefinitions.itemToString(item) + "]");
        if (applied == 1)
            return true;
        return false;
    }
}
