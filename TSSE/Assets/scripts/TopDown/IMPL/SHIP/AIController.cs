using UnityEngine;
using System.Collections;
using System;

// The basic AI script for a ship (enemy or player affiliated).
// This ship gives the orders to a ship gameobject
// The AI is as follows: if there's an enemy ship, try to get within
// firing range. If within firing range and pointing vaguely at it, 
// fire main gun!. If there are no enemies, go hang out with a friendly
// ship. If no one's around, sit and contemplate the nature of solitude.
public class AIController : MonoBehaviour, ShipController
{
    private ShipDefinitions.SState state = ShipDefinitions.SState.Searching;
    public ShipIntf ship;
    private string tagReserve;
    private ShipDefinitions.Faction faction;
    private GameObject target;
    private Vector3 badVector;

    // For this guy; 
    // Aiming is when a target has been acquired and we're 
    // trying to get into firing position (and range)

    // Cooling... let's try running away until we can fire again

    // Firing is when we're in the middle of a clip

    // Searching is when we're looking for a target
    public void getNextState()
    {

        //        Inactive, Searching, Aiming, Firing, Cooling

        /*
        Vector3 target = Vector3.zero;
        GameObject obj = GetComponent<TargetFinder>().getTarget(faction);
        if (obj) target = obj.transform.position;
        Vector3 diff = target - transform.position;
        if (diff == -transform.position)
        {
            ship.brake();
            return;
        }
        float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;

        if (diff.x > 0)
            targetAngle = 180 + targetAngle;
        targetAngle = (int)targetAngle;

        float shipAngle = transform.rotation.eulerAngles.z;

        //print("Target: " + targetAngle.ToString() + " Ship: " + shipAngle.ToString());

        if (ShipDefinitions.quickestRotation(shipAngle, targetAngle))
            ship.rotate(0.5f);
        else
            ship.rotate(-0.5f);


        ship.move(1);

        IntfShip shipObject = GetComponent<IntfShip>();
        if ((shipAngle + shipObject.getEffectiveAngle() > targetAngle &&
            shipAngle - shipObject.getEffectiveAngle() < targetAngle) &&
                (Vector3.Distance(transform.position,
                    target) < shipObject.getEffectiveDistance()))
            ship.fire();
        shipObject = null;
        obj = null;
        */
    }

    private void handleState()
    {
        if (state == ShipDefinitions.SState.Searching)
        {
            // purpose of state is to find a target
            // if target is found, switch to state aiming
            Vector3 target = badVector;
            GameObject obj = GetComponent<TargetFinder>().getTarget(faction);
            /*
            if ((GetComponent<FiringModule>().GetType().
                Equals(typeof(HealMod))))
            {
                obj = GetComponent<TargetFinder>().getFriendly(faction);
            }
            */
            this.target = obj;
            if (obj)
            {
                target = obj.transform.position;
            }
            else
            {
                obj = GetComponent<TargetFinder>().getFriendly(faction);
                if (obj)
                {
                    if (!obj.Equals(gameObject))
                    {
                        target = obj.transform.position;
                    }
                }
            }

            if (target != badVector)
            {
                this.target = obj;
                state = ShipDefinitions.SState.Aiming;
            }
            else
            {
                ship.brake();
            }
        }
        else if (state == ShipDefinitions.SState.Aiming)
        {
            bool move = true;
            if (target == null)
            {
                state = ShipDefinitions.SState.Searching;
                return;
            }

            Vector3 diff = target.transform.position - transform.position;
            //print(diff);
            float targetAngle = Mathf.Atan(diff.y / diff.x) * 180 / Mathf.PI + 90;
            if (diff.x > 0)
                targetAngle = 180 + targetAngle;
            targetAngle = (int)targetAngle;
            float shipAngle = transform.rotation.eulerAngles.z;

            if (ShipDefinitions.quickestRotation(shipAngle, targetAngle))
            {
                ship.rotate(0.5f);
            }
            else
            {
                ship.rotate(-0.5f);
            }


            ShipIntf shipObject = GetComponent<ShipIntf>();
            if ((shipAngle + shipObject.getEffectiveAngle() > targetAngle &&
                shipAngle - shipObject.getEffectiveAngle() < targetAngle))
            {
                if (Vector3.Distance(transform.position,
                        target.transform.position) < shipObject.getEffectiveDistance())
                {
                    if (target.GetComponent<ShipController>()
                        .getFaction() != faction)
                        state = ShipDefinitions.SState.Firing;
                    else
                    {
                        ship.brake();
                        move = false;
                        state = ShipDefinitions.SState.Searching;
                    }

                    if ((GetComponent<FiringModule>().GetType().
                        Equals(typeof(HealMod))))
                    {
                        if (target.GetComponent<ShipIntf>().getHealthPercent() < 0.95)
                            state = ShipDefinitions.SState.Firing;
                    }
                }
            }
            shipObject = null;
            if (move)
                ship.move(1);
            else
                ship.brake();
        }
        else if (state == ShipDefinitions.SState.Firing)
        {
            ship.fire();

            if (GetComponent<FiringModule>().canFire() == false)
            {
                state = ShipDefinitions.SState.Cooling;
            }
        }
        else if (state == ShipDefinitions.SState.Cooling)
        {
            if (GetComponent<FiringModule>().canFire())
            {
                state = ShipDefinitions.SState.Searching;
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        faction = ShipDefinitions.stringToFaction(gameObject.tag);
        ship = this.GetComponent<ShipIntf>();
        tag = gameObject.tag;
        tagReserve = tag;
        badVector = new Vector3(1e5f, 1e5f, 1e5f);
        if (GetComponent<FiringModule>() == null)
        {
            gameObject.AddComponent<DummyFiringMod>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        ship.setText(ship.getName() +
        ": " + GetComponent<ShipController>().
        getState().ToString().Substring(0, 1));
        if (!ship.getActive()) return;
        handleState();
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

    public void enable()
    {
        gameObject.tag = tagReserve;
        enabled = true;
        ship.start();
    }

    public void disable()
    {
        tagReserve = gameObject.tag;
        gameObject.tag = "Untagged";
        enabled = false;
        ship.stop();
    }

    void OnMouseDown()
    {
        Camera.main.GetComponent<Pause>().requestManualControl(gameObject);
    }

    public string getName()
    {
        return ship.getName();
    }

    public ShipDefinitions.SState getState()
    {
        return state;
    }

    public void pause()
    {
        ship.pause();
    }

    public void unpause()
    {
        ship.unpause();
    }

    public GameObject getTarget()
    {
        return target;
    }
}
