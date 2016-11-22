using UnityEngine;
using System.Collections;

// The class for the flamethrower effect
// In lore, this is referred to as a plasma cluster
public class flames : ParticleAbstract
{
    float spinrate = 4;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        transform.Rotate(new Vector3(0, 0, spinrate));
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifetime--;
        }
    }

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
                <ShipIntf>().isHit(1);
        }
    }
}
