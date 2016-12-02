using UnityEngine;
using System.Collections;

// HealingMissile is the projectile used by the healing
// weapon mod that increases the health of other ships
public class HealingMissile : Particle
{
    public GameObject target = null;
    public float moveSpeed = 3;
    public float rotationSpeed = 10;
    public int damage = 16;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        if (target.transform.position == gameObject.transform.position)
            target = null;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime--;
        }
        if (target == null)
        {
            target = GetComponent<TargetFinder>().getFriendly(faction);
        }

        Vector3 diff = target.transform.position - transform.position;
        if (diff == -transform.position)
        {
            return;
        }
        float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;

        if (diff.x > 0)
            targetAngle = 180 + targetAngle;
        targetAngle = (int)targetAngle;

        float shipAngle = transform.rotation.eulerAngles.z;

        //print("Target: " + targetAngle.ToString() + " Ship: " + shipAngle.ToString());

        if (ShipDefinitions.quickestRotation(shipAngle, targetAngle))
            rotate(1);
        else
            rotate(-1);


        move(1);
    }

    public float getAngle()
    {
        return (transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180;
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.CompareTag("Enemy") &&
                (faction == ShipDefinitions.Faction.Enemy) ||
             (col.CompareTag("Player") ||
              col.CompareTag("PlayerAffil")) &&
                faction == ShipDefinitions.Faction.Player ||
                faction == ShipDefinitions.Faction.PlayerAffil))
        {
            col.gameObject.GetComponent
                <ShipIntf>().isHit(-damage);
            Destroy(gameObject);
        }
    }
}
