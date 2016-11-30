using UnityEngine;
using System.Collections;

public class FiringSprite : MonoBehaviour {
    public Sprite pirateICBg;
    public Sprite pirateFMBg;
    public Sprite regularICBg;
    public Sprite regularFMBg;
    public Sprite crownFg;
    public Sprite laserFg;
    public Sprite missileFg;
    public Sprite flameFg;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setSprite(ShipDefinitions.Faction faction, string weaponType)
    {
        if (faction == ShipDefinitions.Faction.Enemy)
        {
            if(weaponType == "crown" || 
                weaponType == "laser")
            GetComponent<SpriteRenderer>().sprite
                = pirateICBg;
            else if (weaponType == "missile" ||
                weaponType == "flame")
                GetComponent<SpriteRenderer>().sprite
                    = pirateFMBg;
        }
        else
        {
            if (weaponType == "crown" ||
                weaponType == "laser")
                GetComponent<SpriteRenderer>().sprite
                    = regularICBg;
            else if (weaponType == "missile" ||
                weaponType == "flame")
                GetComponent<SpriteRenderer>().sprite
                    = regularFMBg;
        }
        switch (weaponType)
        {
            case "crown":
                transform.Find("ModuleForeground")
                    .GetComponent<SpriteRenderer>()
                    .sprite = crownFg;
                break;
            case "laser":
                transform.Find("ModuleForeground")
                    .GetComponent<SpriteRenderer>()
                    .sprite = laserFg;
                break;
            case "missile":
                transform.Find("ModuleForeground")
                    .GetComponent<SpriteRenderer>()
                    .sprite = missileFg;
                break;
            case "flame":
                transform.Find("ModuleForeground")
                    .GetComponent<SpriteRenderer>()
                    .sprite = flameFg;
                break;
        }
        return;
    }
}
