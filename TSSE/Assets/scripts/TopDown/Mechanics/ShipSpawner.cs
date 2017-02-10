using UnityEngine;
using System.Collections;

// This handles spawning for the combad phase.
// Initially, it spawns the ships in your initial loadout. A stupid 
// oneTime boolean hack makes sure they aren't spawned until other
// stuff is loaded (putting this in start fails for some reason,
// someone else's start needs to run first.
// I will literally treat whoever fixes this to chocolates.

 // Then, in update, we occasionally spawn enemies in waves 
 // and send the player back to the loadout screen if all their
 // ships are dead.
public class ShipSpawner : MonoBehaviour
{
    float cooldownMax = 500;
    float cooldown = 0;
    int counter = 2000;

    private int MAXENEMIES = 6;
    private int WAVEENEMIES = 3;

    bool oneTime = true;

    public UnityEngine.UI.Text countdown;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void spawnLoadout()
    {

        // Spawn the player's starting ships
        // the loudout data is saved in player preferences
        for (int i = 0; i < 6; i++)
        {
            int shipType = PlayerPrefs.GetInt("shipType" + i);
            string weaponType = PlayerPrefs.GetString("weaponType" + i);
            int engineType = PlayerPrefs.GetInt("engineType" + i);
            GameObject ship;

            if (shipType == 1)
            {
                ship = getShipRuby();
            }
            else if (shipType == 2)
            {
                ship = getShipPeacock();
            }
            else
            {
                continue;
            }

            setFaction(ship, ShipDefinitions.Faction.PlayerAffil);

            if (engineType == 1)
            {
                equipEngineLvl1(ship);
            }
            else if (engineType == 2)
            {
                equipEngineLvl2(ship);
            }

            if (weaponType == "flame")
            {
                equipFire(ship);
            }
            else if (weaponType == "laser")
            {
                equipLaser(ship);
            }
            else if (weaponType == "missile")
            {
                equipMissile(ship);
            }
            else if (weaponType == "crown")
            {
                equipCrown(ship);
            }

            spawnShip(Vector2.zero, ship);
        }
    }

    void Update()
    {
        if(oneTime)
        {
            // first update, spawn player ships
            spawnLoadout();
            oneTime = false;
        }
        countdown.text = (cooldownMax - cooldown).ToString();

        // Don't increment wave timer if paused
        if (!GameObject.Find("GameLogic").GetComponent<Pause>().getPaused())
        {
            cooldown++;
        }
        counter++;

        if (Time.frameCount % 1000 == 0)
        {
            System.GC.Collect();
        }
        if (counter >= 1000)
        {
            counter = 0;
            Resources.UnloadUnusedAssets();
        }

        // time for a new wave
        if(cooldown >= cooldownMax)
        {
            int numEnemies = 0;
            int numGoodies = 0;

            // first count how many ships of each time we have
            foreach (MainShip ship in GameObject.FindObjectsOfType<MainShip>())
            {
                if(ship.GetComponent<ShipController>().getFaction() == ShipDefinitions.Faction.Enemy)
                {
                    numEnemies++;
                }
                if (ship.GetComponent<ShipController>().getFaction() == ShipDefinitions.Faction.Player ||
                    ship.GetComponent<ShipController>().getFaction() == ShipDefinitions.Faction.PlayerAffil)
                {
                    numGoodies++;
                }
            }

            
            if(numEnemies < MAXENEMIES)
            {
                // spawn some enemies, but only if we don't already have a lot
                for (int i = 0; i <= Random.Range(0, WAVEENEMIES); i++)
                {
                    Vector3 spawnPoint;
                    Vector3 spawnRand = 8 * Random.insideUnitSphere;
                    spawnPoint = Vector3.zero;
                    Vector3 temp = new Vector3(0, Camera.main.pixelHeight / 2);
                    spawnPoint.y = Camera.main.ScreenToWorldPoint(temp).y;
                    spawnRand.z = 0;
                    float z = Random.Range(0, 8);
                    if (z <= 7.5)
                    {
                        temp = new Vector3(Camera.main.pixelWidth / 4, 0, Camera.main.nearClipPlane);
                        spawnPoint.x = Camera.main.ScreenToWorldPoint(temp).x;
                    }
                    else
                    {
                        temp = new Vector3(3 * Camera.main.pixelWidth / 4, Camera.main.nearClipPlane);
                        spawnPoint.x = Camera.main.ScreenToWorldPoint(temp).x;
                    }

                    if (z <= 2)
                        spawnFireShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
                    else if (z > 2 && z <= 4)
                        spawnCrownShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
                    else if (z > 4 && z <= 6)
                        spawnMissileShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
                    else if (z > 6 && z <= 8)
                        spawnLaserShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
                }
            }

            // all our players are dead, game over
            if (numGoodies == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            cooldown = 0;
        }
    }




    public void spawnBunch()
    {
        Vector3 temp;
        for (int i = 0; i <= 4; i++)
        {
            Vector3 spawnPoint;
            Vector3 spawnRand = 8 * Random.insideUnitSphere;
            spawnPoint = Vector3.zero;
            temp = new Vector3(0, Camera.main.pixelHeight / 2);
            spawnPoint.y = Camera.main.ScreenToWorldPoint(temp).y;
            spawnRand.z = 0;
            float z = Random.Range(0, 16);
            if (z <= 7.5)
            {
                temp = new Vector3(Camera.main.pixelWidth / 4, 0, Camera.main.nearClipPlane);
                spawnPoint.x = Camera.main.ScreenToWorldPoint(temp).x;
            }
            else
            {
                temp = new Vector3(3 * Camera.main.pixelWidth / 4, Camera.main.nearClipPlane);
                spawnPoint.x = Camera.main.ScreenToWorldPoint(temp).x;
            }

            if (z <= 2)
                spawnFireShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
            else if (z > 2 && z <= 4)
                spawnCrownShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
            else if (z > 4 && z <= 6)
                spawnMissileShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);
            else if (z > 6 && z <= 8)
                spawnLaserShip(spawnPoint + spawnRand, ShipDefinitions.Faction.Enemy);

            else if (z > 8 && z <= 10)
                spawnFireShip(spawnPoint + spawnRand, ShipDefinitions.Faction.PlayerAffil);
            else if (z > 10 && z <= 12)
                spawnCrownShip(spawnPoint + spawnRand, ShipDefinitions.Faction.PlayerAffil);
            else if (z > 12 && z <= 14)
                spawnMissileShip(spawnPoint + spawnRand, ShipDefinitions.Faction.PlayerAffil);
            else
                spawnLaserShip(spawnPoint + spawnRand, ShipDefinitions.Faction.PlayerAffil);
        }
    }

    // add components required for ship AI to function
    void addAIComponents(GameObject ship)
    {
        ship.AddComponent<AIController>();
        ship.AddComponent<TargetFinder>();
    }

    // get a ruby ship of the player faction
    GameObject getShipRuby()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipRubyObject();
        addAIComponents(ship);
        return ship;
    }

    // get a ruby ship of the pirate faction
    GameObject getShipRubyPirate()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipRubyPirateObject();
        addAIComponents(ship);
        return ship;
    }

    // get a peacock ship of the player faction
    GameObject getShipPeacock()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipPeacockObject();
        addAIComponents(ship);
        return ship;
    }

    // get a peacock ship of the pirate faction
    GameObject getShipPeacockPirate()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipPeacockPirateObject();
        addAIComponents(ship);
        return ship;
    }

    // equip this ship with a lvl 1 engine
    void equipEngineLvl1(GameObject ship)
    {
        /*
        GameObject engine = GameObject.Find("GameLogic")
                .GetComponent<PrefabHost>().getEngineLvl1Object();
        engine.transform.parent = ship.transform;
        */
        ship.GetComponent<MainShip>().setEngineType(ShipDefinitions.EngineType.Engine1);
    }

    // equip this ship with a lvl 2 engine
    void equipEngineLvl2(GameObject ship)
    {
        /*
        GameObject engine = GameObject.Find("GameLogic")
                .GetComponent<PrefabHost>().getEngineLvl2Object();
        engine.transform.parent = ship.transform;
        */
        ship.GetComponent<MainShip>().setEngineType(ShipDefinitions.EngineType.Engine2);
    }

    // get a ship of a certain faction, small probability of higher level
    GameObject getShip(ShipDefinitions.Faction faction)
    {
        GameObject ship;
        if (Random.Range(0, 10) < 2)
        {
            if (faction == ShipDefinitions.Faction.Enemy)
                ship = getShipPeacockPirate();
            else
                ship = getShipPeacock();
        }
        else
        {
            if (faction == ShipDefinitions.Faction.Enemy)
                ship = getShipRubyPirate();
            else
                ship = getShipRuby();
        }
        return ship;
    }

    // equip this ship with a flamethrower
    void equipFire(GameObject ship)
    {
        ship.GetComponent<MainShip>().setWeaponType(ShipDefinitions.WeaponType.Flame);
    }

    // equip this ship with a short-range auto-targeting laser
    void equipCrown(GameObject ship)
    {
        ship.GetComponent<MainShip>().setWeaponType(ShipDefinitions.WeaponType.Crown);
    }

    // equip this ship with a short range missile launcher
    void equipMissile(GameObject ship)
    {
        ship.GetComponent<MainShip>().setWeaponType(ShipDefinitions.WeaponType.Missile);
    }

    // equip this ship with a long range laser cannon
    void equipLaser(GameObject ship)
    {
        ship.GetComponent<MainShip>().setWeaponType(ShipDefinitions.WeaponType.Laser);
    }

    // spawn a firethrower ship
    public void spawnFireShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipFire(ship);
        spawnShip(spawnPoint, ship);
    }

    // spawn a crown laser ship
    public void spawnCrownShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipCrown(ship);
        spawnShip(spawnPoint, ship);
    }

    // spawn a missile ship
    public void spawnMissileShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipMissile(ship);
        spawnShip(spawnPoint, ship);
    }

    // spawn a laser cannon ship
    public void spawnLaserShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipLaser(ship);
        spawnShip(spawnPoint, ship);
    }

    // spawn the given ship at the given location
    void spawnShip(Vector2 spawnPoint, GameObject ship)
    {
        GameObject obj = ship;
        obj.transform.position = spawnPoint;

        ShipIntf ctrl = obj.GetComponent<ShipIntf>();

        if (GameObject.Find("GameLogic").GetComponent<Pause>().getPaused())
        {
            ctrl.pause();
        }
    }
    
    // remove all ships on screen
    public void deleteAll()
    {
        foreach (MainShip ship in GameObject.FindObjectsOfType<MainShip>())
        {
            Destroy(ship.transform.parent.gameObject);
        }
    }

    // set the faction of the given ship
    void setFaction(GameObject obj, ShipDefinitions.Faction faction)
    {
        obj.tag = faction.ToString();
        obj.GetComponent<ShipController>().setFaction(faction);
    }
}
