using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    public void saveLevel(Level level)
    {

    }

    public void loadLevel(string levelId)
    {
    }

    public string getCurrentLevel()
    {
        return PlayerPrefs.GetString("TSSEcurrentLevel");
    }
}
