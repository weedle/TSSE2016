using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
    protected bool paused;
    protected bool pausedOnce;
    protected GameObject manualShip;
	// Use this for initialization
	void Start () {
        paused = false;
        pausedOnce = false;
        manualShip = null;
	}

    // Update is called once per frame
    void Update()
    {
        if (manualShip != null)
        {
            Vector3 bottomLeft = manualShip.transform.position - new Vector3(0.01f, 0.01f);
            Vector3 topRght = manualShip.transform.position + new Vector3(0.01f, 0.01f);
            ShipDefinitions.DrawSquare(bottomLeft, topRght, Color.yellow, 2 * Time.deltaTime);
        }
        if (Input.GetButtonDown("Pause"))
        {
            if (pausedOnce) return;
            pausedOnce = true;
            if (!paused)
            {
                GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<ParticleAbstract>() != null)
                    {
                        ParticleAbstract particle = obj.GetComponent<ParticleAbstract>();
                        particle.pause();
                    }

                    if (obj.GetComponent<IntfShipController>() != null)
                    {
                        IntfShipController particle = obj.GetComponent<IntfShipController>();
                        particle.pause();
                    }
                }
            }
            else
            {
                GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();

                foreach (GameObject obj in objects)
                {
                    if (obj.GetComponent<ParticleAbstract>() != null)
                    {
                        ParticleAbstract particle = obj.GetComponent<ParticleAbstract>();
                        particle.unpause();
                    }

                    if (obj.GetComponent<IntfShipController>() != null)
                    {
                        IntfShipController ctrl = obj.GetComponent<IntfShipController>();
                        ctrl.unpause();
                    }
                }
            }
            paused = !paused;
        }
        if (Input.GetButtonUp("Pause"))
        {
            pausedOnce = false;
        }

        if(paused)
        {
            Color targetLinePlayer = Color.green;
            Color targetLineEnemy = Color.red;
            targetLinePlayer.a = 30;

            GameObject[] objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in objects)
            {
                if (obj.GetComponent<IntfShipController>() != null)
                {
                    IntfShipController ctrl = obj.GetComponent<IntfShipController>();
                    if (ctrl.getTarget() != null)
                    {
                        if (ctrl.getFaction() == ShipDefinitions.Faction.PlayerAffil ||
                            ctrl.getFaction() == ShipDefinitions.Faction.Player)
                        {
                            ShipDefinitions.DrawLine(obj.transform.position,
                                ctrl.getTarget().transform.position, targetLinePlayer,
                                2 * Time.deltaTime, 0.01f);
                        }
                        else if (ctrl.getFaction() == ShipDefinitions.Faction.Enemy)
                        {
                            ShipDefinitions.DrawLine(obj.transform.position,
                                ctrl.getTarget().transform.position, targetLineEnemy,
                                2 * Time.deltaTime, 0.01f);
                        }
                    }
                }
            }
        }
    }

    public void requestManualControl(GameObject ship)
    {
        if(ship.Equals(manualShip))
        {
            return;
        }

        Destroy((Object) ship.GetComponent<IntfShipController>());
        ship.AddComponent<ImplMainShipController>();

        if(manualShip)
        {
            Destroy((Object)manualShip.GetComponent<IntfShipController>());
            manualShip.AddComponent<ImplBasicAIShipController>();
        }
        manualShip = ship;
    }

    public bool getPaused()
    {
        return paused;
    }
}
