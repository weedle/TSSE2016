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
    public int rotationSpeed = 5;
    public float moveSpeed = 1;
    private string shipName;
    private GameObject health;
    private GameObject text;
    public float healthPoints = 10;
    public float maxHealth = 10;
    private Vector2 velKeep;

    // Use this for initialization
    void Start()
    {
        rotationSpeed += Random.Range(-2, 2);
        moveSpeed += Random.Range(-0.3f, 0.3f);
        //Camera camera = Camera.main;
        //camera.orthographicSize = 640 / Screen.width * Screen.height / 2;
        shipName = ShipDefinitions.generateName();
        GameObject parent = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getEmptyObject();
        this.health = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getHealthObject();
        this.text = GameObject.Find("GameLogic").GetComponent<PrefabHost>().getLabelObject();
        health.transform.SetParent(parent.transform);
        text.transform.SetParent(parent.transform);
        transform.SetParent(parent.transform);
        health.GetComponent<HealthBar>().setTarget(gameObject);
        text.GetComponent<ShipLabel>().setTarget(gameObject);

        gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        // If the ship is out of bounds, Bounds.getPosInBounds
        // will return a new position within bounds
        transform.position = Bounds.getPosInBounds(transform.position);
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
        Vector2 temp = new Vector2(Mathf.Cos(getAngle()),
                Mathf.Sin(getAngle()));
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (vertical != 0)
            rigidbody.velocity = temp * moveSpeed * (vertical + Mathf.Sign(vertical));
        temp = Vector3.zero;
    }

    public void rotate(float horizontal)
    {
        Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            transform.Rotate(temp);
        temp = Vector3.zero;
    }


    public void fire()
    {
        if(GetComponent<FiringModule>() != null)
            GetComponent<FiringModule>().fire();
    }

    public float getEffectiveDistance()
    {
        return GetComponent<FiringModule>().getEffectiveDistance();
    }

    public float getEffectiveAngle()
    {
        return GetComponent<FiringModule>().getEffectiveAngle();
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
}
