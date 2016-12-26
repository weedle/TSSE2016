using UnityEngine;
using System.Collections;


/*
 * - used for healing missiles, which is a weapon mod that can increase
 *   the health of the ships that the missile hits
 * 
 * - for reference: scripts/TopDown/INTF/Particle.cs 
 * 				  : scripts/TopDown/MECHANICS/ShipDefinitions.cs
 * 				  : 
 */
public class HealingMissile : Particle
{
    public GameObject target = null;
    public float moveSpeed = 3;
    public float rotationSpeed = 10;

	/*
	 * regulates particle movement and handles particle lifetime
	 * 
	 * NOTE: refer to helper functions below!
	 */ 
    void Update()
    {
        if (!active)
            return;
        handleLifetime();

        // If the missile has no target, drift in same direction
        if (target.transform.position == gameObject.transform.position)
            target = null;

        if (target == null)
            target = GetComponent<TargetFinder>().getFriendly(faction);

        // preparatory calculations for target tracking
        Vector3 diff = target.transform.position - transform.position;
        if (diff == -transform.position)
            return;
		
        float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;

        if (diff.x > 0)
            targetAngle = 180 + targetAngle;
        targetAngle = (int)targetAngle;

        float shipAngle = transform.rotation.eulerAngles.z;

        // rotate towards the target
        if (ShipDefinitions.quickestRotation(shipAngle, targetAngle))
            rotate(1);
        else
            rotate(-1);

        move(1);
    }


    // Return the current orientation of the missile

    public float getAngle()
    {
        return (transform.rotation.eulerAngles.z + 90) * Mathf.PI / 180;
    }


    /*
    * The missile, unlike some other particles, needs its own movement
    * functionality as it needs to track and approach targets
    */
    public void move(float vertical)
    {
        Vector2 temp = new Vector2(Mathf.Cos(getAngle()),
                Mathf.Sin(getAngle()));
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (vertical != 0)
            rigidbody.velocity = temp * moveSpeed * (vertical + Mathf.Sign(vertical));
        temp = Vector3.zero;
    }


    // Rotate the missile
    public void rotate(float horizontal)
    {
        Vector3 temp = new Vector3(0, 0, -1 * rotationSpeed * horizontal);
        if (horizontal != 0)
            transform.Rotate(temp);
        temp = Vector3.zero;
    }
}
