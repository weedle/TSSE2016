using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelDefinitions {
    public struct Level
    {
        public string uniqueId;
        public List<ShipDefinitions.ShipEntity> ships;
        //public Dictionary<string, string> shipSpawningTokens;
        public LevelType type;
    }

    public enum LevelType
    {
        Wave, World
    }

    public static void saveLevel(Level level)
    {
        PlayerPrefs.SetString("TSSE[Level][" + level.uniqueId + "][Ships]", ShipDefinitions.shipsToEntityIds(level.ships));
        ShipDefinitions.saveShips(level.ships);
        PlayerPrefs.SetInt("TSSE[Level][" + level.uniqueId + "][Type]", (int) level.type);
    }

    public static Level loadLevel(string uniqueId)
    {
        Level level = new Level();
        level.ships = new List<ShipDefinitions.ShipEntity>();

        string shipIdsEncoded = PlayerPrefs.GetString("TSSE[Level][" + uniqueId + "][Ships]");
        string[] shipIds = shipIdsEncoded.Split('#');
        foreach (string shipId in shipIds)
        {
            level.ships.Add(ShipDefinitions.loadShip(shipId));
        }

        int levelType = PlayerPrefs.GetInt("TSSE[Level][" + uniqueId + "][Type]");

        level.type = (LevelType)Enum.Parse(
            typeof(LevelType),
            levelType.ToString());

        return level;
    }

    public string getCurrentLevel()
    {
        return PlayerPrefs.GetString("TSSEcurrentLevel");
    }
}
