using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour
{
    float cooldownMax = 500;
    float cooldown = 0;
    int counter = 2000;

    public UnityEngine.UI.Text countdown;

    //private Color enemyCol = new Color(1, 0.2f, 0.2f);
    //private Color allyCol = new Color(0.2f, 1, 0.2f);
    // Use this for initialization
    void Start()
    {
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
            else if(shipType == 2)
            {
                ship = getShipPeacock();
            }
            else
            {
                continue;
            }

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
            /*
            if (shipInfo[i].shipType == 1)
                PlayerPrefs.SetInt("shipType" + i, 1);
            else if (shipInfo[i].shipType == 2)
                PlayerPrefs.SetInt("shipType" + i, 2);

            if (shipInfo[i].weaponType == "flame")
                PlayerPrefs.SetString("weaponType" + i, "flame");
            else if (shipInfo[i].weaponType == "crown")
                PlayerPrefs.SetString("weaponType" + i, "crown");
            else if (shipInfo[i].weaponType == "missile")
                PlayerPrefs.SetString("weaponType" + i, "missile");
            else if (shipInfo[i].weaponType == "laser")
                PlayerPrefs.SetString("weaponType" + i, "laser");
                */
        }
    }

    // Update is called once per frame

    void Update()
    {
        countdown.text = (cooldownMax - cooldown).ToString();
        if(!GameObject.Find("GameLogic").GetComponent<Pause>().getPaused())
            cooldown++;
        //if (Camera.main.GetComponent<Pause>().getPaused()) return;
        //Vector3 temp;
        if (Time.frameCount % 1000 == 0)
        {
            System.GC.Collect();
        }
        if (counter >= 1000)
        {
            counter = 0;
            Resources.UnloadUnusedAssets();
        }
        counter++;

        if(cooldown >= cooldownMax)
        {
            int numEnemies = 0;
            int numGoodies = 0;
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

            if(numEnemies < 6)
            {

                for (int i = 0; i <= Random.Range(0, 3); i++)
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

            if (numGoodies == 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            cooldown = 0;
        }
        /*
        if (cooldown <= 0)
        {
            Vector2 cursorPoint = new Vector2(ShipDefinitions.getCursor().x,
                    ShipDefinitions.getCursor().y);
            if (Input.GetButton("z"))
            {
                spawnAllyShip(cursorPoint);
            }
            if (Input.GetButton("x"))
            {
                spawnAllyCrown(cursorPoint);
            }
            if (Input.GetButton("c"))
            {
                spawnAllyMissile(cursorPoint);
            }
            if (Input.GetButton("v"))
            {
                spawnEnemyShip(cursorPoint);
            }
            if (Input.GetButton("b"))
            {
                spawnEnemyCrown(cursorPoint);
            }
            if (Input.GetButton("n"))
            {
                spawnEnemyMissile(cursorPoint);
            }
            if (Input.GetButton("m"))
            {
                spawnBunch();
            }
            foreach (Touch touch in Input.touches)
            {
                Vector3 vec = Camera.main.ScreenToWorldPoint(touch.position);
                //Vector3 bound = Camera.main.
                //    ScreenToWorldPoint(new Vector3(Screen.width,
                //    Screen.height, 0));
                if (touch.phase == TouchPhase.Began)
                {
                    temp = new Vector3(vec.x, vec.y);
                    if (vec.x < 0)
                    {
                        if (vec.y < 0)
                        {
                            spawnEnemyShip(temp);
                        }
                        else
                        {
                            spawnEnemyCrown(temp);
                        }
                    }
                    else
                    {
                        if (vec.y < 0)
                        {
                            spawnAllyShip(temp);
                        }
                        else
                        {
                            spawnAllyCrown(temp);
                        }
                    }

                    if (touch.position.x < Screen.width / 10 &&
                        touch.position.y < Screen.height / 10)
                    {
                        deleteAll();
                    }
                    if (touch.position.x > Screen.width * 0.9 &&
    touch.position.y > Screen.height * 0.9)
                    {
                        spawnBunch();
                    }
                }
            }
        }
        else
        {
            cooldown--;
        }
        temp = Vector3.zero;
        */
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

    void addAIComponents(GameObject ship)
    {
        ship.AddComponent<AIController>();
        ship.AddComponent<TargetFinder>();
    }

    GameObject getShipRuby()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipRubyObject();
        addAIComponents(ship);
        return ship;
    }

    GameObject getShipRubyPirate()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipRubyPirateObject();
        addAIComponents(ship);
        return ship;
    }
    
    GameObject getShipPeacock()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipPeacockObject();
        addAIComponents(ship);
        return ship;
    }

    GameObject getShipPeacockPirate()
    {
        GameObject ship = GameObject.Find("GameLogic")
            .GetComponent<PrefabHost>().getShipPeacockPirateObject();
        addAIComponents(ship);
        return ship;
    }

    void equipEngineLvl1(GameObject ship)
    {
        GameObject engine = GameObject.Find("GameLogic")
                .GetComponent<PrefabHost>().getEngineLvl1Object();
        engine.transform.parent = ship.transform;
    }

    void equipEngineLvl2(GameObject ship)
    {
        GameObject engine = GameObject.Find("GameLogic")
                .GetComponent<PrefabHost>().getEngineLvl2Object();
        engine.transform.parent = ship.transform;
    }

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

    void equipFire(GameObject ship)
    {
        ship.AddComponent<FlameMod>();
    }

    void equipCrown(GameObject ship)
    {
        ship.AddComponent<CrownMod>();
    }

    void equipMissile(GameObject ship)
    {
        ship.AddComponent<MissileMod>();
    }

    void equipLaser(GameObject ship)
    {
        ship.AddComponent<PewPewLaserMod>();
    }

    public void spawnFireShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipFire(ship);
        spawnShip(spawnPoint, ship);
    }

    public void spawnCrownShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipCrown(ship);
        spawnShip(spawnPoint, ship);
    }

    public void spawnMissileShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipMissile(ship);
        spawnShip(spawnPoint, ship);
    }

    public void spawnLaserShip(Vector3 spawnPoint, ShipDefinitions.Faction faction)
    {
        GameObject ship = getShip(faction);
        setFaction(ship, faction);
        equipEngineLvl1(ship);
        equipLaser(ship);
        spawnShip(spawnPoint, ship);
    }

    void spawnShip(Vector2 spawnPoint, GameObject ship)
    {
        //GameObject obj = (GameObject)Instantiate(ship, spawnPoint, Quaternion.Euler(0, 0, 0));
        GameObject obj = ship;
        obj.transform.position = spawnPoint;
        //obj.GetComponent<SpriteRenderer>().color = color;

        //GameObject empty = GameObject.Find("GameLogic")
        //    .GetComponent<PrefabHost>().getEmptyObject();
        //GameObject parent = (GameObject)Instantiate(empty, spawnPoint, Quaternion.Euler(0, 0, 0));

        //GameObject healthBar = (GameObject)Instantiate(health, spawnPoint, Quaternion.Euler(0, 0, 0));

        //GameObject textObj = (GameObject)Instantiate(text, spawnPoint, Quaternion.Euler(0, 0, 0));

        //healthBar.transform.SetParent(parent.transform);
        //obj.transform.SetParent(parent.transform);
        //textObj.transform.SetParent(parent.transform);

        ShipIntf ctrl = obj.GetComponent<ShipIntf>();
        //ctrl.setHealth(healthBar);
        //ctrl.setTextObj(textObj);

        //HealthBar bar = healthBar.GetComponent<HealthBar>();
        //bar.target = obj;

        //ShipLabel ShipLabel = textObj.GetComponent<ShipLabel>();
        //ShipLabel.target = obj;

        if (GameObject.Find("GameLogic").GetComponent<Pause>().getPaused())
        {
            ctrl.pause();
        }
    }
    
    public void deleteAll()
    {
        foreach (MainShip ship in GameObject.FindObjectsOfType<MainShip>())
        {
            Destroy(ship.transform.parent.gameObject);
        }
    }

    void setFaction(GameObject obj, ShipDefinitions.Faction faction)
    {
        obj.tag = faction.ToString();
        obj.GetComponent<ShipController>().setFaction(faction);
        //obj.GetComponent<IntfFiringModule>().
    }
}
