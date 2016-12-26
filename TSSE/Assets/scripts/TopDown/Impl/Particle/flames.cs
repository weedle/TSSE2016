using UnityEngine;
using System.Collections;


/* 
 * - used for the flamethrower effect
 * - in lore, this is reffered to as a 'plasma cluster'
 * 
 * - for reference: scripts/TopDown/INTF/Particle.cs 
 * 				  : scripts/TopDown/MECHANICS/ShipDefinitions.cs
 */
public class Flames : Particle
{
    float spinrate = 4;

	/*
	 * regulates particle movement (rotation) and handles the particle lifetime
	 */ 
    void Update()
    {
        if (!active)
            return;
        handleLifetime();
        transform.Rotate(new Vector3(0, 0, spinrate));
    }
}
