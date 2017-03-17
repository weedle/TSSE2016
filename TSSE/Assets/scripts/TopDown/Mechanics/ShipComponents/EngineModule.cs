using UnityEngine;
using System.Collections;

public class EngineModule : MonoBehaviour {
    private ShipIntf ship;
    public float moveSpeed = 1;
    public float rotationSpeed = 5;
    // Use this for initialization
    void Start () {
        ship = gameObject.transform.parent.GetComponent<ShipIntf>();
        moveSpeed += Random.Range(-0.3f, 0.3f);
        rotationSpeed += Random.Range(-2, 2);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        if(ship.getShipType() == ShipDefinitions.ShipType.Peacock)
        {
            moveSpeed *= 0.8f;
            rotationSpeed *= 0.8f;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rigidbody2D rigidbody = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        if (rigidbody.velocity.sqrMagnitude < 0.01f)
            GetComponent<SpriteRenderer>().enabled = false;
    }

    public void move(float vertical)
    {
        Vector2 temp = new Vector2(Mathf.Cos(ship.getAngle()),
        Mathf.Sin(ship.getAngle()));
        Rigidbody2D rigidbody = gameObject.transform.parent.GetComponent<Rigidbody2D>();
        if (vertical != 0)
            rigidbody.velocity = temp * moveSpeed * (vertical + Mathf.Sign(vertical));
        temp = Vector3.zero;

        if (vertical > 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().Play("Active");
        }
        else if (vertical < 0)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Animator>().Play("Reverse");
        }
    }

    public void rotate(float horizontal)
    {
        Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            gameObject.transform.parent.transform.Rotate(temp);
        temp = Vector3.zero;
    }

    public void applyBuff(ItemAbstract item)
    {
        if(!EngineItem.isEngineType(item.getType()))
        {
            return;
        }
        //process item
    }
}
