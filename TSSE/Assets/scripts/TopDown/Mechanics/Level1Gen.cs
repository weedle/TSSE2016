using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level1Gen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Level1Setup()
    {
        PlayerPrefs.SetString("TSSE[Level][Current]", "Level1");

        LevelDefinitions.Level level1 = new LevelDefinitions.Level();
        level1.uniqueId = "Level1";


        List<ShipDefinitions.ShipEntity> enemies = new List<ShipDefinitions.ShipEntity>();
        // Adding default enemies for sake of testing
        ShipDefinitions.ShipEntity enemy1 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine1, ShipDefinitions.WeaponType.Flame,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy1");
        ShipDefinitions.ShipEntity enemy2 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine1, ShipDefinitions.WeaponType.Flame,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy2");
        ShipDefinitions.ShipEntity enemy3 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine1, ShipDefinitions.WeaponType.Flame,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy3");


        ShipDefinitions.ShipEntity enemy4 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine2, ShipDefinitions.WeaponType.Crown,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy4");
        ShipDefinitions.ShipEntity enemy5 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine2, ShipDefinitions.WeaponType.Crown,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy5");
        ShipDefinitions.ShipEntity enemy6 = new ShipDefinitions.ShipEntity(ShipDefinitions.EngineType.Engine2, ShipDefinitions.WeaponType.Crown,
            ShipDefinitions.ShipType.Ruby, new List<ItemAbstract>(), ShipDefinitions.Faction.Enemy, "Enemy6");

        enemies.Add(enemy1);
        enemies.Add(enemy2);
        enemies.Add(enemy3);
        enemies.Add(enemy4);
        enemies.Add(enemy5);
        enemies.Add(enemy6);

        level1.ships = enemies;

        level1.shipSpawningTokens = new Dictionary<string, string>();
        level1.shipSpawningTokens["Enemy1"] = "wave0";
        level1.shipSpawningTokens["Enemy2"] = "wave0";
        level1.shipSpawningTokens["Enemy3"] = "wave0";
        level1.shipSpawningTokens["Enemy4"] = "wave1";
        level1.shipSpawningTokens["Enemy5"] = "wave1";
        level1.shipSpawningTokens["Enemy6"] = "wave1";

        level1.shipSpawningTokens["PlayerShip0"] = "center";
        level1.shipSpawningTokens["PlayerShip1"] = "center";
        level1.shipSpawningTokens["PlayerShip2"] = "center";
        level1.shipSpawningTokens["PlayerShip3"] = "center";
        level1.shipSpawningTokens["PlayerShip4"] = "center";
        level1.shipSpawningTokens["PlayerShip5"] = "center";

        PlayerPrefs.SetString("TSSE[Level][Current]", level1.uniqueId);

        LevelDefinitions.saveLevel(level1);
    }
}
