using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipSelection : MonoBehaviour {
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;
    public GameObject ship6;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    public GameObject weapon5;
    public GameObject weapon6;

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

    public Sprite shipNoneSprite;
    public Sprite ship1Sprite;
    public Sprite ship2Sprite;

    public Sprite weaponNoneSprite;
    public Sprite weaponCrownSprite;
    public Sprite weaponLaserSprite;
    public Sprite weaponMissileSprite;
    public Sprite weaponFlameSprite;

    public Sprite engineNoneSprite;
    public Sprite engine1Sprite;
    public Sprite engine2Sprite;

    public UnityEngine.UI.Button buttonContinue;


    public int selectedShip = 0;
    
    
    public struct ShipInfo
    {
        public int shipType;
        public string weaponType;
        public int engineType;
    }

    private int cost;

    private ShipInfo[] shipInfo;
    private GameObject[] ships;
    private GameObject[] weapons;
    private GameObject[] engines;

    // Use this for initialization
    void Start() {
        // dropdown value is 0 for option 1, 1 for option 2, etc

        shipInfo = new ShipInfo[6];
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

        for (int i = 0; i < 6; i++)
        {
            shipInfo[i].shipType = -1;
            shipInfo[i].weaponType = "none";
            shipInfo[i].engineType = -1;
        }

        dropdown.onValueChanged.AddListener(delegate {
            handleDropDownChange(dropdown);
        });

        addButtonListener(shipButton1);
        addButtonListener(shipButton2);
        addButtonListener(crownButton);
        addButtonListener(laserButton);
        addButtonListener(missileButton);
        addButtonListener(flameButton);
        addButtonListener(engineButton1);
        addButtonListener(engineButton2);

        unhighlightButton(shipButton1);
        unhighlightButton(shipButton2);
        unhighlightButton(crownButton);
        unhighlightButton(laserButton);
        unhighlightButton(missileButton);
        unhighlightButton(flameButton);

        buttonContinue.onClick.AddListener(delegate
        {
            tryToContinue(buttonContinue);
        });
    }

    void tryToContinue(Button buttonContinue)
    {
        updateCost();
        int score = PlayerPrefs.GetInt("score");
        if (score >= cost)
        {
            score -= cost;
            PlayerPrefs.SetInt("score", score);

            for (int i = 0; i < 6; i++)
            {
                if (shipInfo[i].shipType == 1)
                    PlayerPrefs.SetInt("shipType" + i, 1);
                else if (shipInfo[i].shipType == 2)
                    PlayerPrefs.SetInt("shipType" + i, 2);
                else
                    PlayerPrefs.SetInt("shipType" + i, 0);

                if (shipInfo[i].engineType == 1)
                    PlayerPrefs.SetInt("engineType" + i, 1);
                else if (shipInfo[i].engineType == 2)
                    PlayerPrefs.SetInt("engineType" + i, 2);
                else
                    PlayerPrefs.SetInt("engineType" + i, 0);

                if (shipInfo[i].weaponType == "flame")
                    PlayerPrefs.SetString("weaponType" + i, "flame");
                else if (shipInfo[i].weaponType == "crown")
                    PlayerPrefs.SetString("weaponType" + i, "crown");
                else if (shipInfo[i].weaponType == "missile")
                    PlayerPrefs.SetString("weaponType" + i, "missile");
                else if (shipInfo[i].weaponType == "laser")
                    PlayerPrefs.SetString("weaponType" + i, "laser");
                else
                    PlayerPrefs.SetString("weaponType" + i, "none");
            }

            //SceneManager.LoadScene("scenes/Testing/mainTesting");
            SceneManager.LoadScene(1);
        }
        else
        {
            textThing.text = "Not enough scrap!";
        }
    }

    void updateTextThing()
    {
        /*
        textThing.text = 
            "Ship " + (selectedShip + 1) + ": " + 
            "Type " + shipInfo[selectedShip].shipType + " " +
            "Weapon " + shipInfo[selectedShip].weaponType;
            */
    }

    void addButtonListener(UnityEngine.UI.Button button)
    {
        button.onClick.AddListener(delegate
        {
            handleButtonPress(button);
        });
    }

    // Update is called once per frame
    void Update () {
        updateTextThing();
    }

    void unhighlightAllWeaponButtons()
    {
        unhighlightButton(crownButton);
        unhighlightButton(laserButton);
        unhighlightButton(missileButton);
        unhighlightButton(flameButton);
}

    void handleButtonPress(UnityEngine.UI.Button button)
    {
        if (button.name == "buttonShipLvl1")
        {
            if (shipInfo[selectedShip].shipType == 1)
            {
                shipInfo[selectedShip].shipType = -1;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = shipNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].shipType = 1;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = ship1Sprite;
                highlightButton(button);
                unhighlightButton(shipButton2);
            }
        }

        if (button.name == "buttonShipLvl2")
        {
            if (shipInfo[selectedShip].shipType == 2)
            {
                shipInfo[selectedShip].shipType = -1;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = shipNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].shipType = 2;
                ships[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = ship2Sprite;
                highlightButton(button);
                unhighlightButton(shipButton1);
            }
        }

        if (button.name == "buttonEngineLvl1")
        {
            if (shipInfo[selectedShip].engineType == 1)
            {
                shipInfo[selectedShip].engineType = -1;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engineNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].engineType = 1;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engine1Sprite;
                highlightButton(button);
                unhighlightButton(engineButton2);
            }
        }

        if (button.name == "buttonEngineLvl2")
        {
            if (shipInfo[selectedShip].engineType == 2)
            {
                shipInfo[selectedShip].engineType = -1;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engineNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].engineType = 2;
                engines[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = engine2Sprite;
                highlightButton(button);
                unhighlightButton(engineButton1);
            }
        }

        if (button.name == "buttonCrown")
        {
            if (shipInfo[selectedShip].weaponType == "crown")
            {
                shipInfo[selectedShip].weaponType = "none";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weaponType = "crown";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponCrownSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonMissile")
        {
            if (shipInfo[selectedShip].weaponType == "missile")
            {
                shipInfo[selectedShip].weaponType = "none";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weaponType = "missile";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponMissileSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonLaser")
        {
            if (shipInfo[selectedShip].weaponType == "laser")
            {
                shipInfo[selectedShip].weaponType = "none";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weaponType = "laser";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponLaserSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        if (button.name == "buttonFlame")
        {
            if (shipInfo[selectedShip].weaponType == "flame")
            {
                shipInfo[selectedShip].weaponType = "none";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponNoneSprite;
                unhighlightButton(button);
            }
            else
            {
                shipInfo[selectedShip].weaponType = "flame";
                weapons[selectedShip]
                    .GetComponent<SpriteRenderer>()
                    .sprite = weaponFlameSprite;
                unhighlightAllWeaponButtons();
                highlightButton(button);
            }
        }

        updateCost();
    }
    
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

    void handleDropDownChange(UnityEngine.UI.Dropdown dropdownTarget)
    {
        selectedShip = dropdownTarget.value;

        ShipInfo selectInfo = shipInfo[dropdownTarget.value]; 
        if(selectInfo.shipType != 2)
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

        if (selectInfo.shipType != 1)
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

        if (selectInfo.engineType != 2)
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

        if (selectInfo.engineType != 1)
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

        if (selectInfo.weaponType != "crown")
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

        if (selectInfo.weaponType != "missile")
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

        if (selectInfo.weaponType != "laser")
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

        if (selectInfo.weaponType != "flame")
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

    void updateCost()
    {
        cost = 0;
        for(int i = 0; i < 6; i++)
        {
            if (shipInfo[i].shipType == 1)
                cost += int.Parse(GameObject.Find("costShip1").GetComponent<Text>().text); // <- HAHAHAHAHAHAHAHAHAHAHAHA
            else if (shipInfo[i].shipType == 2)
                cost += int.Parse(GameObject.Find("costShip2").GetComponent<Text>().text);

            if (shipInfo[i].engineType == 1)
                cost += int.Parse(GameObject.Find("costEngine1").GetComponent<Text>().text);
            else if (shipInfo[i].engineType == 2)
                cost += int.Parse(GameObject.Find("costEngine2").GetComponent<Text>().text);

            if (shipInfo[i].weaponType == "flame")
                cost += int.Parse(GameObject.Find("costFlame").GetComponent<Text>().text);
            else if(shipInfo[i].weaponType == "crown")
                cost += int.Parse(GameObject.Find("costCrown").GetComponent<Text>().text);
            else if (shipInfo[i].weaponType == "missile")
                cost += int.Parse(GameObject.Find("costMissile").GetComponent<Text>().text);
            else if (shipInfo[i].weaponType == "laser")
                cost += int.Parse(GameObject.Find("costLaser").GetComponent<Text>().text);
        }

        int score = PlayerPrefs.GetInt("score");

        textThing.text = "Total Cost is " + cost + " scrap.\nYou have " + score + " scrap.";
    }
}
