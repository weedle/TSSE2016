using UnityEngine;
using System.Collections;



/*
 * - used for laser particles/effect
 * 
 * - for reference: scripts/TopDown/INTF/Particle.cs 
 * 				  : scripts/TopDown/MECHANICS/ShipDefinitions.cs
 */ 
public class PewPewLaser : Particle
{
	/*
	 * regulates particle movement (rotation) and handles the particle lifetime
	 */ 
    void Update()
    {
        if (!active)
            return;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime--;
        }
    }


	/*
	 * when the particle collides with a ship, decreases the health of
	 * the ship under appropriate circumstances (see below)
	 * 
	 * NOTE: a ship will be damaged only if the particle that hits it 
	 * 		 is sent from a ship in the opposing faction!!!
	 */
    public void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.CompareTag("Enemy") &&
                (faction == ShipDefinitions.Faction.Player ||
                faction == ShipDefinitions.Faction.PlayerAffil)) ||
             (col.CompareTag("Player") ||
              col.CompareTag("PlayerAffil")) &&
                faction == ShipDefinitions.Faction.Enemy)
        {
            col.gameObject.GetComponent
                <ShipIntf>().isHit(8);
        }
    }
}
