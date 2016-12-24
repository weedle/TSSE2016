using UnityEngine;
using System.Collections;


/*
 * used as an abstraction of various particles (e.g.- missiles, flames)
 * reference: see   scripts/TopDown/IMPL/PARTICLE
 * 			: see	scripts/TopDown/MECHANICS/ShipComponents/ShipDefinitions.cs 
 */ 
public abstract class Particle : MonoBehaviour
{
    public int lifetime;
    private Vector2 velKeep;
    public bool active = true;
    public ShipDefinitions.Faction faction;

    
	/*
	 * allows the particle to freeze; records its current velocity
	 * to resume upon unpausing
	 */
    public void pause()
    {
        active = false;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        velKeep = rbody.velocity;
        rbody.velocity = Vector2.zero;
        if(GetComponent<Animator>() != null)
            GetComponent<Animator>().Stop();
    }


	/*
	 * allows the particule to resume its previous activity
	 */ 
    public void unpause()
    {
        active = true;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = velKeep;
        if(GetComponent<Animator>() != null)
            GetComponent<Animator>().StartPlayback();
    }


	/*
	 * sets the ship faction
	 */ 
    public void setFaction(ShipDefinitions.Faction faction)
    {
        this.faction = faction;
    }
}
