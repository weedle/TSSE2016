using UnityEngine;
using System.Collections;

public class ShipSpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject ally;
    public GameObject player;
    public GameObject enemyCrown;
    public GameObject allyCrown;
    public GameObject enemyMissile;
    public GameObject allyMissile;
    public GameObject allyHealer;
    public GameObject empty;
    public GameObject health;
    public GameObject text;
    float cooldownMax = 15;
    float cooldown = 15;
    int counter = 2000;

    private Color enemyCol = new Color(1, 0.2f, 0.2f);
    private Color allyCol = new Color(0.2f, 1, 0.2f);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        //if (Camera.main.GetComponent<Pause>().getPaused()) return;
        Vector3 temp;
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
        return;
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
    }

    public void spawnBunch()
    {
        Vector3 temp;
        for (int i = 0; i <= 8; i++)
        {
            Vector3 spawnPoint;
            Vector3 spawnRand = 2 * Random.insideUnitSphere;
            spawnPoint = Vector3.zero;
            temp = new Vector3(0, Camera.main.pixelHeight / 2);
            spawnPoint.y = Camera.main.ScreenToWorldPoint(temp).y;
            spawnRand.z = 0;
            float z = Random.Range(0, 15);
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

            if (z <= 2.5)
                spawnEnemyShip(spawnPoint + spawnRand);
            else if (z > 2.5 && z <= 5)
                spawnEnemyCrown(spawnPoint + spawnRand);
            else if (z > 5 && z <= 7.5)
                spawnEnemyMissile(spawnPoint + spawnRand);
            else if (z > 7.5 && z <= 10)
                spawnAllyShip(spawnPoint + spawnRand);
            else if (z > 10 && z <= 12.5)
                spawnAllyCrown(spawnPoint + spawnRand);
            else
                spawnAllyMissile(spawnPoint + spawnRand);

        }
    }

    public void spawnFireShip(ShipDefinitions.Faction faction, Vector2 spawnPoint)
    {
        if (faction.Equals(ShipDefinitions.Faction.PlayerAffil))
            spawnAllyShip(spawnPoint);
        else
            spawnEnemyShip(spawnPoint);
    }

    public void spawnCrownShip(ShipDefinitions.Faction faction, Vector2 spawnPoint)
    {
        if (faction.Equals(ShipDefinitions.Faction.PlayerAffil))
            spawnAllyCrown(spawnPoint);
        else
            spawnEnemyCrown(spawnPoint);
    }

    public void spawnMissileShip(ShipDefinitions.Faction faction, Vector2 spawnPoint)
    {
        if (faction.Equals(ShipDefinitions.Faction.PlayerAffil))
            spawnAllyMissile(spawnPoint);
        else
            spawnEnemyMissile(spawnPoint);
    }

    void spawnAllyShip(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, ally, allyCol);
    }

    void spawnAllyCrown(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, allyCrown, allyCol);
    }

    void spawnAllyMissile(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, allyMissile, allyCol);
    }

    void spawnEnemyShip(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, enemy, enemyCol);
    }

    void spawnEnemyCrown(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, enemyCrown, enemyCol);
    }

    void spawnEnemyMissile(Vector2 spawnPoint)
    {
        spawnShip(spawnPoint, enemyMissile, enemyCol);
    }

    void spawnShip(Vector2 spawnPoint, GameObject ship, Color color)
    {
        GameObject obj = (GameObject)Instantiate(ship, spawnPoint, Quaternion.Euler(0, 0, 0));
        obj.GetComponent<SpriteRenderer>().color = color;
        cooldown = cooldownMax;

        GameObject parent = (GameObject)Instantiate(empty, spawnPoint, Quaternion.Euler(0, 0, 0));

        GameObject healthBar = (GameObject)Instantiate(health, spawnPoint, Quaternion.Euler(0, 0, 0));

        GameObject textObj = (GameObject)Instantiate(text, spawnPoint, Quaternion.Euler(0, 0, 0));

        healthBar.transform.SetParent(parent.transform);
        obj.transform.SetParent(parent.transform);
        textObj.transform.SetParent(parent.transform);

        IntfShip ctrl = obj.GetComponent<IntfShip>();
        ctrl.setHealth(healthBar);
        ctrl.setTextObj(textObj);

        HealthBar bar = healthBar.GetComponent<HealthBar>();
        bar.target = obj;

        TextShip textShip = textObj.GetComponent<TextShip>();
        textShip.target = obj;

        if (Camera.main.GetComponent<Pause>().getPaused())
        {
            ctrl.pause();
        }
    }

    public void deleteAll()
    {
        foreach (ImplMainShip ship in GameObject.FindObjectsOfType<ImplMainShip>())
        {
            Destroy(ship.transform.parent.gameObject);
        }
    }

    void setFaction(GameObject obj, ShipDefinitions.Faction faction)
    {
        obj.tag = faction.ToString();
        obj.GetComponent<IntfShipController>().setFaction(faction);
        //obj.GetComponent<IntfFiringModule>().
    }
}
