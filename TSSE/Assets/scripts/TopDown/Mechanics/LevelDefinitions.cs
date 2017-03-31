using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelDefinitions {
    public struct Level
    {
        public string uniqueId;
        public List<ShipDefinitions.ShipEntity> ships;
        public Dictionary<string, string> shipSpawningTokens;
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
        string dictEncoded = "";
        foreach(string key in level.shipSpawningTokens.Keys)
        {
            GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing(level.shipSpawningTokens[key]);
            dictEncoded = dictEncoded + key + ":" + level.shipSpawningTokens[key] + "#";
        }

        GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing("saving dict: " + dictEncoded);


        string s = "TSSE[Level][" + level.uniqueId + "][Dictionary]";
        GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing("saving with key " + s);
        PlayerPrefs.SetString(s, dictEncoded);
    }

    public static Level loadLevel(string uniqueId)
    {
        Level level = new Level();
        level.uniqueId = uniqueId;
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

        string s = "TSSE[Level][" + level.uniqueId + "][Dictionary]";
        string dictEncoded = PlayerPrefs.GetString(s);
        
        GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing("loading with key " + s);
        GameObject.Find("GameLogic").GetComponent<GameEventHandler>().printThing("loading dict: " + dictEncoded);

        string[] dictSeparated = dictEncoded.Split('#');

        level.shipSpawningTokens = new Dictionary<string, string>();

        foreach(string keyVal in dictSeparated)
        {
            if(keyVal != "")
            {
                string key = keyVal.Split(':')[0];
                string val = keyVal.Split(':')[1];

                level.shipSpawningTokens[key] = val;
            }
        }

        return level;
    }

    public string getCurrentLevel()
    {
        return PlayerPrefs.GetString("TSSE[Level][Current]");
    }
}
