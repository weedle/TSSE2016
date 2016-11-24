using UnityEngine;
using System.Collections;

// ParticleAbstract is the abstraction of any particle (eg, missiles)
public abstract class Particle : MonoBehaviour
{
    public int lifetime;
    private Vector2 velKeep;
    public bool active = true;
    public ShipDefinitions.Faction faction;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Pause makes this particle freeze, and record its current velocity
    // to resume upon unpausing
    public void pause()
    {
        active = false;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        velKeep = rbody.velocity;
        rbody.velocity = Vector2.zero;

        GetComponent<Animator>().Stop();
    }

    // Unpause makes this particle resume activity
    public void unpause()
    {
        active = true;

        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = velKeep;

        GetComponent<Animator>().StartPlayback();
    }

    public void setFaction(ShipDefinitions.Faction faction)
    {
        this.faction = faction;
    }
}
