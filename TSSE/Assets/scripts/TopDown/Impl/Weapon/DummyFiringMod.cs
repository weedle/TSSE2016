using UnityEngine;
using System.Collections;
using System;

// A Dummy weapon mod for if you want a ship that doesn't shoot
public class DummyFiringMod : MonoBehaviour, FiringModule {
    bool FiringModule.canFire()
    {
        return false;
    }

    void FiringModule.fire()
    {
        return;
    }

    float FiringModule.getEffectiveAngle()
    {
        return 0;
    }

    float FiringModule.getEffectiveDistance()
    {
        return 0;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public float getAmmoPerc()
    {
        return 1;
    }

    public void applyBuff(Inventory.Item item)
    {

    }
}
