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
    public bool initialized = false;
    public ShipDefinitions.EngineType engType;
    public ShipDefinitions.WeaponType weapType;
    public ShipDefinitions.ShipType shipType;
    private EngineModule engine;
    private FiringModule weapon;

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

        GameObject engineObj;
        if (engType == ShipDefinitions.EngineType.Engine1)
        {
            engineObj = GameObject.Find("GameLogic").GetComponent<PrefabHost>()
                .getEngineLvl1Object();
        }
        else
        {
            engineObj = GameObject.Find("GameLogic").GetComponent<PrefabHost>()
                .getEngineLvl2Object();
        }
        engineObj.transform.parent = gameObject.transform;
        engineObj.transform.position = gameObject.transform.position;
        engineObj.transform.localScale = new Vector3(1.5f, 1.5f, 0);

        engine = engineObj.GetComponent<EngineModule>();

        switch (weapType)
        {
            case ShipDefinitions.WeaponType.Crown:
                weapon = gameObject.AddComponent<CrownMod>();
                break;
            case ShipDefinitions.WeaponType.Laser:
                weapon = gameObject.AddComponent<PewPewLaserMod>();
                break;
            case ShipDefinitions.WeaponType.Missile:
                weapon = gameObject.AddComponent<MissileMod>();
                break;
            case ShipDefinitions.WeaponType.Flame:
                weapon = gameObject.AddComponent<FlameMod>();
                break;
            default:
                weapon = gameObject.AddComponent<DummyFiringMod>();
                break;
        }


        ShipDefinitions.Faction faction = ShipDefinitions.stringToFaction(gameObject.tag);
        if (faction == ShipDefinitions.Faction.Enemy)
        {
            if (shipType == ShipDefinitions.ShipType.Ruby)
            {
                SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
                if(spr == null)
                    spr = gameObject.AddComponent<SpriteRenderer>();
                    spr.sprite = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipRubyPirateSprite;
                gameObject.AddComponent<Animator>().runtimeAnimatorController
                    = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipRubyPirateAnimator;
            }
            else
            {
                SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
                if (spr == null)
                    spr = gameObject.AddComponent<SpriteRenderer>();
                spr.sprite = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipPeacockPirateSprite;
                gameObject.AddComponent<Animator>().runtimeAnimatorController
                    = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipPeacockPirateAnimator;
            }
        }
        else
        {
            if (shipType == ShipDefinitions.ShipType.Ruby)
            {
                SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
                if (spr == null)
                    spr = gameObject.AddComponent<SpriteRenderer>();
                spr.sprite = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipRubySprite;
                gameObject.AddComponent<Animator>().runtimeAnimatorController
                    = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipRubyAnimator;
            }
            else
            {
                SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
                if (spr == null)
                    spr = gameObject.AddComponent<SpriteRenderer>();
                spr.sprite = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipPeacockSprite;
                gameObject.AddComponent<Animator>().runtimeAnimatorController
                    = GameObject.Find("GameLogic").GetComponent<PrefabHost>().
                    shipPeacockAnimator;
            }
        }

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
        if (engine == null)
            return;
        engine.move(vertical);
    }

    public void rotate(float horizontal)
    {
        if (engine == null)
            return;
        engine.rotate(horizontal);
    }


    public void fire()
    {
        if (weapon == null)
            return;
        weapon.fire();
    }

    public float getEffectiveDistance()
    {
        if (weapon == null)
            return 0;
        return weapon.getEffectiveDistance();
    }

    public float getEffectiveAngle()
    {
        if (weapon == null)
            return 0;
        return weapon.getEffectiveAngle();
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

    public ShipDefinitions.ShipType getShipType()
    {
        return shipType;
    }

    public void setEngineType(ShipDefinitions.EngineType type)
    {
        engType = type;
    }

    public void setWeaponType(ShipDefinitions.WeaponType type)
    {
        weapType = type;
    }

    public void setShipType(ShipDefinitions.ShipType type)
    {
        shipType = type;
    }

    public void setFaction(ShipDefinitions.Faction faction)
    {
        gameObject.tag = faction.ToString();
    }
}
