using UnityEngine;
using System.Collections;
using System;

public class ManualController : MonoBehaviour, ShipController
{
    private ShipDefinitions.SState state = ShipDefinitions.SState.Searching;
    public ShipIntf ship;
    private ShipDefinitions.Faction faction;

    // does nothing since player targets manually
    public GameObject getTarget()
    {
        return null;
    }

    public void getNextState()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            state = ShipDefinitions.SState.Searching;
            ship.move(vertical);
        }
        if (horizontal != 0)
        {
            state = ShipDefinitions.SState.Searching;
            ship.rotate(horizontal);
        }
        if (Input.GetButton("Fire1"))
        {
            state = ShipDefinitions.SState.Firing;
            ship.fire();

            if (GetComponent<FiringModule>().canFire() == false)
            {
                state = ShipDefinitions.SState.Cooling;
            }
        }
        if (Input.GetButton("Fire2"))
        {
            state = ShipDefinitions.SState.Searching;
            ship.brake();
        }
    }

    // Use this for initialization
    void Start()
    {
        faction = ShipDefinitions.stringToFaction(gameObject.tag);
        ship = GetComponent<ShipIntf>();

        if(GetComponent<FiringModule>() == null)
        {
            gameObject.AddComponent<DummyFiringMod>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ship.setText(ship.getName() +
            ": " + "manual");
        if (!ship.getActive()) return;
        getNextState();
    }

    public ShipDefinitions.Faction getFaction()
    {
        return faction;
    }

    public void setFaction(ShipDefinitions.Faction faction)
    {
        this.faction = faction;
    }

    public string getName()
    {
        return ship.getName();
    }

    public ShipDefinitions.SState getState()
    {
        return ShipDefinitions.SState.Aiming;
    }

    public void pause()
    {
        ship.pause();
    }

    public void unpause()
    {
        ship.unpause();
    }
}
