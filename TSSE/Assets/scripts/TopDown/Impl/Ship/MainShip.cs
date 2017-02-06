using UnityEngine;
using System.Collections;

// The main script to manipulate the ship gameobject.
// Note that although this script knows how to carry out any action on a ship,
// from firing, to accelerating, to changing the text label or health bar, it
// does not know *when* to do any of these things.
// Hence the need for a ShipController! :D
public class MainShip : MonoBehaviour, ShipIntf
{
    public bool inactive;
    private string shipName;
    private GameObject health;
    private GameObject ammo;
    private GameObject text;
    public float healthPoints = 100;
    public float maxHealth = 100;
    private Vector2 velKeep;
    public int shipType;
    public bool initialized = false;

    // Use this for initialization
    void Start()
    {
        //Camera camera = Camera.main;
        //camera.orthographicSize = 640 / Screen.width * Screen.height / 2;
        shipName = ShipDefinitions.generateName();
        GameObject parent = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getEmptyObject();
        this.health = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getHealthObject();
        this.ammo = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getAmmoObject();
        this.text = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getLabelObject();
        health.transform.SetParent(parent.transform);
        ammo.transform.SetParent(parent.transform);
        text.transform.SetParent(parent.transform);
        transform.SetParent(parent.transform);
        health.GetComponent<HealthBar>().setTarget(gameObject);
        ammo.GetComponent<AmmoBar>().setTarget(gameObject);
        text.GetComponent<ShipLabel>().setTarget(gameObject);

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        //string id = shipName.Substring(shipName.Length - 4);
        parent.name = "Parent-" + shipName;
        gameObject.name = "Ship-" + shipName;
        health.name = "Health-" + shipName;
        ammo.name = "Ammo-" + shipName;
        text.name = "Text-" + shipName;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the ship is out of bounds, Bounds.getPosInBounds
        // will return a new position within bounds
        transform.position = Bounds.getPosInBounds(transform.position);
        if (GetComponent<FiringModule>() != null)
        {
            float perc = GetComponent<FiringModule>().getAmmoPerc();
            ammo.GetComponent<AmmoBar>().setAmmoPercentage(perc);
        }
    }

    public ShipDefinitions.SState getState()
    {
        return ShipDefinitions.SState.Inactive;
    }

    public float getAngle()
    {
        return (transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180;
    }

    public void brake()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = rigidbody.velocity * (float)0.90;
    }

    public void move(float vertical)
    {
        if (gameObject.GetComponentInChildren<EngineModule>() != null)
            gameObject.GetComponentInChildren<EngineModule>().move(vertical);
    }

    public void rotate(float horizontal)
    {
        if (gameObject.GetComponentInChildren<EngineModule>() != null)
            gameObject.GetComponentInChildren<EngineModule>().rotate(horizontal);
    }


    public void fire()
    {
        if(GetComponent<FiringModule>() != null)
            GetComponent<FiringModule>().fire();
    }

    public float getEffectiveDistance()
    {
        if (GetComponent<FiringModule>() != null)
            return GetComponent<FiringModule>().getEffectiveDistance();
        return 0;
    }

    public float getEffectiveAngle()
    {
        if (GetComponent<FiringModule>() != null)
            return GetComponent<FiringModule>().getEffectiveAngle();
        return 0;
    }

    public void start()
    {
        enabled = true;
    }

    public void stop()
    {
        enabled = false;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
    }

    public string getName()
    {
        return shipName;
    }

    public void isHit(float damage)
    {
        healthPoints -= damage;

        float perc = healthPoints / maxHealth;
        health.GetComponent<HealthBar>().setHealthPercentage(perc);

        if (healthPoints <= 0)
        {
            inactive = true;
            this.gameObject.GetComponent<SpriteRenderer>().
                color = Color.white;
            this.gameObject.
                GetComponent<Animator>().Play("Explode");
        }
    }

    public float getHealthPercent()
    {
        return (float)healthPoints / maxHealth;
    }

    public void setText(string newText)
    {
        text.GetComponent<ShipLabel>().setText(newText);
    }

    public bool getActive()
    {
        return !inactive;
    }

    public void pause()
    {
        inactive = true;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        velKeep = rbody.velocity;
        rbody.velocity = Vector2.zero;

    }

    public void unpause()
    {
        inactive = false;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = velKeep;
    }

    public int getShipType()
    {
        return shipType;
    }
}
