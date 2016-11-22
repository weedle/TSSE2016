using UnityEngine;
using System.Collections;

// Interface for a ShipController, that gives directions to a Ship
public interface ShipController
{
    ShipDefinitions.Faction getFaction();

    void setFaction(ShipDefinitions.Faction faction);

    string getName();

    ShipDefinitions.SState getState();

    void pause();

    void unpause();

    GameObject getTarget();
}
