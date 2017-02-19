using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Ship selection is a huge ugly mess of code for this particular scene
// and sorely needs some refactoring.
// note to self: remove this comment once refactoring is done
public class ShipSelection : MonoBehaviour {
    // these are the visual ship objects
    // if you select your first ship to be a ruby ship, then ship1
    // will show a ruby ship sprite
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;
    public GameObject ship6;

    // these are the tiny sprites to the right of each ship object, that 
    // reflect what weapon is equipped on that ship
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

    // to the left of each ship is the engine object, that does
    // the same thing but for engines
    public GameObject engine1;
    public GameObject engine2;
    public GameObject engine3;
    public GameObject engine4;
    public GameObject engine5;
    public GameObject engine6;

    public UnityEngine.UI.Button engineButton1;
    public UnityEngine.UI.Button engineButton2;
    public UnityEngine.UI.Button shipButton1;
    public UnityEngine.UI.Button shipButton2;
    public UnityEngine.UI.Button crownButton;
    public UnityEngine.UI.Button laserButton;
    public UnityEngine.UI.Button missileButton;
    public UnityEngine.UI.Button flameButton;
    public UnityEngine.UI.Dropdown dropdown;
    public UnityEngine.UI.Text textThing;

    // sprites to set when ships are selected
    public Sprite shipNoneSprite;
    public Sprite ship1Sprite;
    public Sprite ship2Sprite;

    // sprites to set when weapons are selected
    public Sprite weaponNoneSprite;
    public Sprite weaponCrownSprite;
    public Sprite weaponLaserSprite;
    public Sprite weaponMissileSprite;
    public Sprite weaponFlameSprite;

    // same but for engines
    public Sprite engineNoneSprite;
    public Sprite engine1Sprite;
    public Sprite engine2Sprite;

    public UnityEngine.UI.Button buttonContinue;

    // this is the ship slot we are currently equipping things on
    public int selectedShip = 0;

    // total cost of all equipped items so far
    private int cost;

    // the current loadout
    private ShipDefinitions.ShipEntity[] shipInfo;

    // the sprites we change to reflect the current loadout
    private GameObject[] ships;
    private GameObject[] weapons;
    private GameObject[] engines;

    // Use this for initialization
    void Start() {
        shipInfo = new ShipDefinitions.ShipEntity[6];

        ships = new GameObject[6];
        ships[0] = ship1;
        ships[1] = ship2;
        ships[2] = ship3;
        ships[3] = ship4;
        ships[4] = ship5;
        ships[5] = ship6;

        weapons = new GameObject[6];
        weapons[0] = weapon1;
        weapons[1] = weapon2;
        weapons[2] = weapon3;
        weapons[3] = weapon4;
        weapons[4] = weapon5;
        weapons[5] = weapon6;

        engines = new GameObject[6];
        engines[0] = engine1;
        engines[1] = engine2;
        engines[2] = engine3;
        engines[3] = engine4;
        engines[4] = engine5;
        engines[5] = engine6;

        // initialize info
        for (int i = 0; i < 6; i++)
        {
            ShipDefinitions.ShipEntity entity = new ShipDefinitions.ShipEntity();
            entity.engType = ShipDefinitions.EngineType.None;
            entity.weapType = ShipDefinitions.WeaponType.None;
            entity.shipType = ShipDefinitions.ShipType.None;
            entity.faction = ShipDefinitions.Faction.PlayerAffil;
            entity.uniqueId = "PlayerShip" + i.ToString();
            entity.items = new List<ItemDefinitions.Item>();
            shipInfo[i] = entity;
        }

        // add listener so we change current selected ship
        // when the dropdown changes
        dropdown.onValueChanged.AddListener(delegate {
            handleDropDownChange(dropdown);
        });

        // add moar listeners
        addButtonListener(shipButton1);
        addButtonListener(shipButton2);
        addButtonListener(crownButton);
        addButtonListener(laserButton);
        addButtonListener(missileButton);
        addButtonListener(flameButton);
        addButtonListener(engineButton1);
        addButtonListener(engineButton2);

        // start with all buttons unhighlighted
        // (nothing equipped)
        unhighlightButton(shipButton1);
        unhighlightButton(shipButton2);
        unhighlightButton(crownButton);
        unhighlightButton(laserButton);
        unhighlightButton(missileButton);
        unhighlightButton(flameButton);

        // add listener
        buttonContinue.onClick.AddListener(delegate
        {
            tryToContinue(buttonContinue);
        });
    }

    // if we have enough scrap for the current loadout, then
    // save that loadout into player preferences so we can
    // access it in the next scene
    // then, load said scene
    void tryToContinue(Button buttonContinue)
    {
        updateCost();
        int score = PlayerPrefs.GetInt("score");
        if (score >= cost)
        {
            score -= cost;
            PlayerPrefs.SetInt("score", score);

            List<ShipDefinitions.ShipEntity> list = new List<ShipDefinitions.ShipEntity>();
            list.AddRange(shipInfo);

            LevelDefinitions.Level level = new LevelDefinitions.Level();

            level.uniqueId = "awyesfirstlevel";
            level.ships = list;
            level.type = LevelDefinitions.LevelType.Wave;

            /*
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModAmmoCap, 3));
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModDamage, 3));
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModFireRate, 3));
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModRange, 3));
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModRechargeRate, 3));
            level.ships[0].items.Add(new ItemDefinitions.Item(ItemDefinitions.ItemType.MissileModSpeed, 3));
            */

            PlayerPrefs.SetString("TSSE[Level][Current]", "awyesfirstlevel");

            LevelDefinitions.saveLevel(level);

            // load the combat scene
            SceneManager.LoadScene(1);
        }
        else
        {
            textThing.text = "Not enough scrap!";
        }
    }

    void addButtonListener(UnityEngine.UI.Button button)
    {
        button.onClick.AddListener(delegate
        {
            handleButtonPress(button);
        });
    }

    // Update is called once per frame
    void Update ()
    {
    }

    // set all weapons to be unselected
    // if you select a weapon, call this first to 
    // unselect the other weapons
    void unhighlightAllWeaponButtons()
    {
        unhighlightButton(crownButton);
        unhighlightButton(laserButton);
        unhighlightButton(missileButton);
        unhighlightButton(flameButton);
}

    // if you select any ship, weapon, or engine, this button
    // updates the sprites of those buttons to show you what's selected
    void handleButtonPress(UnityEngine.UI.Button button)
    {
        if (button.name == "buttonShipLvl1")
        {
            if (shipInfo[selectedShip].shipType == ShipDefinitions.ShipType.Ruby)
            {
                shipInfo[selectedShip].shipType = ShipDefinitions.ShipType.None;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = shipNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].shipType = ShipDefinitions.ShipType.Ruby;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = ship1Sprite;
                highlightButton(button);
                unhighlightButton(shipButton2);
            }
        }

        if (button.name == "buttonShipLvl2")
        {
            if (shipInfo[selectedShip].shipType == ShipDefinitions.ShipType.Peacock)
            {
                shipInfo[selectedShip].shipType = ShipDefinitions.ShipType.None;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = shipNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].shipType = ShipDefinitions.ShipType.Peacock;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = ship2Sprite;
                highlightButton(button);
                unhighlightButton(shipButton1);
            }
        }

        if (button.name == "buttonEngineLvl1")
        {
            if (shipInfo[selectedShip].engType == ShipDefinitions.EngineType.Engine1)
            {
                shipInfo[selectedShip].engType = ShipDefinitions.EngineType.None;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engineNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].engType = ShipDefinitions.EngineType.Engine1;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engine1Sprite;
                highlightButton(button);
                unhighlightButton(engineButton2);
            }
        }

        if (button.name == "buttonEngineLvl2")
        {
            if (shipInfo[selectedShip].engType == ShipDefinitions.EngineType.Engine2)
            {
                shipInfo[selectedShip].engType = ShipDefinitions.EngineType.None;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engineNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].engType = ShipDefinitions.EngineType.Engine2;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engine2Sprite;
                highlightButton(button);
                unhighlightButton(engineButton1);
            }
        }

        if (button.name == "buttonCrown")
        {
            if (shipInfo[selectedShip].weapType == ShipDefinitions.WeaponType.Crown)
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.None;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.Crown;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponCrownSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonMissile")
        {
            if (shipInfo[selectedShip].weapType == ShipDefinitions.WeaponType.Missile)
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.None;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.Missile;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponMissileSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonLaser")
        {
            if (shipInfo[selectedShip].weapType == ShipDefinitions.WeaponType.Laser)
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.None;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.Laser;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponLaserSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonFlame")
        {
            if (shipInfo[selectedShip].weapType == ShipDefinitions.WeaponType.Flame)
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.None;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weapType = ShipDefinitions.WeaponType.Flame;
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponFlameSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        updateCost();
    }
    
    // for now we're just changing the color tint when a weapon
    // is selected
    void highlightButton(UnityEngine.UI.Button button)
    {
        button.GetComponent<UnityEngine.UI.Image>()
                .color = new Color(255, 0, 0, 255);
    }

    void unhighlightButton(UnityEngine.UI.Button button)
    {
        button.GetComponent<UnityEngine.UI.Image>()
                .color = new Color(256, 256, 256, 256);
    }

    // react on dropdown change
    // to show current equipped things on that ship
    void handleDropDownChange(UnityEngine.UI.Dropdown dropdownTarget)
    {
        selectedShip = dropdownTarget.value;

        ShipDefinitions.ShipEntity selectInfo = shipInfo[dropdownTarget.value]; 
        if(selectInfo.shipType != ShipDefinitions.ShipType.Peacock)
        {
            unhighlightButton(
                GameObject.Find("buttonShipLvl2")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonShipLvl2")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.shipType != ShipDefinitions.ShipType.Ruby)
        {
            unhighlightButton(
                GameObject.Find("buttonShipLvl1")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonShipLvl1")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.engType != ShipDefinitions.EngineType.Engine2)
        {
            unhighlightButton(
                GameObject.Find("buttonEngineLvl2")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonEngineLvl2")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.engType != ShipDefinitions.EngineType.Engine1)
        {
            unhighlightButton(
                GameObject.Find("buttonEngineLvl1")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonEngineLvl1")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.weapType != ShipDefinitions.WeaponType.Crown)
        {
            unhighlightButton(
                GameObject.Find("buttonCrown")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonCrown")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.weapType != ShipDefinitions.WeaponType.Missile)
        {
            unhighlightButton(
                GameObject.Find("buttonMissile")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonMissile")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.weapType != ShipDefinitions.WeaponType.Laser)
        {
            unhighlightButton(
                GameObject.Find("buttonLaser")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonLaser")
                .GetComponent<UnityEngine.UI.Button>());
        }

        if (selectInfo.weapType != ShipDefinitions.WeaponType.Flame)
        {
            unhighlightButton(
                GameObject.Find("buttonFlame")
                .GetComponent<UnityEngine.UI.Button>());
        }
        else
        {
            highlightButton(
                GameObject.Find("buttonFlame")
                .GetComponent<UnityEngine.UI.Button>());
        }
    }

    // find the cost of the current loadout
    // this really really needs to change, but it currently gets the cost
    // by... looking at what the labels say the text is
    // we should make this centralized later, especially if we end up
    // implementing that automatic balancing thing
    void updateCost()
    {
        cost = 0;
        for(int i = 0; i < 6; i++)
        {
            if (shipInfo[i].shipType == ShipDefinitions.ShipType.Ruby)
                cost += int.Parse(GameObject.Find("costShip1").GetComponent<Text>().text); // <- HAHAHAHAHAHAHAHAHAHAHAHA
            else if (shipInfo[i].shipType == ShipDefinitions.ShipType.Peacock)
                cost += int.Parse(GameObject.Find("costShip2").GetComponent<Text>().text);

            if (shipInfo[i].engType == ShipDefinitions.EngineType.Engine1)
                cost += int.Parse(GameObject.Find("costEngine1").GetComponent<Text>().text);
            else if (shipInfo[i].engType == ShipDefinitions.EngineType.Engine2)
                cost += int.Parse(GameObject.Find("costEngine2").GetComponent<Text>().text);

            if (shipInfo[i].weapType == ShipDefinitions.WeaponType.Flame)
                cost += int.Parse(GameObject.Find("costFlame").GetComponent<Text>().text);
            else if(shipInfo[i].weapType == ShipDefinitions.WeaponType.Crown)
                cost += int.Parse(GameObject.Find("costCrown").GetComponent<Text>().text);
            else if (shipInfo[i].weapType == ShipDefinitions.WeaponType.Missile)
                cost += int.Parse(GameObject.Find("costMissile").GetComponent<Text>().text);
            else if (shipInfo[i].weapType == ShipDefinitions.WeaponType.Laser)
                cost += int.Parse(GameObject.Find("costLaser").GetComponent<Text>().text);
        }

        int score = PlayerPrefs.GetInt("score");

        textThing.text = "Total Cost is " + cost + " scrap.\nYou have " + score + " scrap.";
    }
}
