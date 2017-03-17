using UnityEngine;
using System.Collections;

public class prebattleShipHandler : MonoBehaviour
{
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;
    public GameObject ship6;

    public Sprite shipNoneSprite;
    public Sprite ship1Sprite;
    public Sprite ship2Sprite;

    public Color dark = new Color(28f / 255, 29f / 255, 33f / 255);
    public Color highlighted = new Color(251f / 255, 196f / 255, 97f / 255);

    // this is the ship slot we are currently equipping things on
    public int selectedShip = 0;

    public ItemListHandler imgStore;

    // total cost of all equipped items so far
    private int cost;

    // the current loadout
    private ShipDefinitions.ShipEntity[] shipInfo;
    // Use this for initialization
    void Start()
    {
        addButtonListener(ship1);
        addButtonListener(ship2);
        addButtonListener(ship3);
        addButtonListener(ship4);
        addButtonListener(ship5);
        addButtonListener(ship6);
    }

    // Update is called once per frame
    void Update()
    {
        ship1.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
        ship2.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
        ship3.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
        ship4.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
        ship5.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
        ship6.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().
            sprite = shipNoneSprite;
    }


    void addButtonListener(GameObject obj)
    {
        UnityEngine.UI.Button button = obj.GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(delegate
        {
            handleButtonPress(button);
        });
    }

    void handleButtonPress(UnityEngine.UI.Button button)
    {
        GameObject ship = null;
        switch(selectedShip)
        {
            case 0:
                ship = ship1;
                break;
            case 1:
                ship = ship2;
                break;
            case 2:
                ship = ship3;
                break;
            case 3:
                ship = ship4;
                break;
            case 4:
                ship = ship5;
                break;
            case 5:
                ship = ship6;
                break;
        }
        UnityEngine.UI.ColorBlock blk = ship.GetComponent<UnityEngine.UI.Button>().colors;
        blk.normalColor = dark;
        ship.GetComponent<UnityEngine.UI.Button>().colors = blk;

        selectedShip = int.Parse(button.name.Substring(button.name.Length - 1, 1));
        selectedShip--;

        switch (selectedShip)
        {
            case 0:
                ship = ship1;
                break;
            case 1:
                ship = ship2;
                break;
            case 2:
                ship = ship3;
                break;
            case 3:
                ship = ship4;
                break;
            case 4:
                ship = ship5;
                break;
            case 5:
                ship = ship6;
                break;
        }
        UnityEngine.UI.ColorBlock blkActive = ship.GetComponent<UnityEngine.UI.Button>().colors;
        blkActive.normalColor = highlighted;
        ship.GetComponent<UnityEngine.UI.Button>().colors = blkActive;

        setImage(ItemAbstract.newItem(WeaponItem.WeaponType.CrownModRange, 2), 0);
    }

    public void setImage(ItemAbstract item, int index)
    {
        Sprite sprite = imgStore.getImage(item);
        GameObject ship = null;
        switch (selectedShip)
        {
            case 0:
                ship = ship1;
                break;
            case 1:
                ship = ship2;
                break;
            case 2:
                ship = ship3;
                break;
            case 3:
                ship = ship4;
                break;
            case 4:
                ship = ship5;
                break;
            case 5:
                ship = ship6;
                break;
        }
        print(sprite.name);
        ship.transform.GetChild(index + 1).GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        ship.transform.GetChild(index + 1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

    public void unsetImage(int index)
    {
        GameObject ship = null;
        switch (selectedShip)
        {
            case 0:
                ship = ship1;
                break;
            case 1:
                ship = ship2;
                break;
            case 2:
                ship = ship3;
                break;
            case 3:
                ship = ship4;
                break;
            case 4:
                ship = ship5;
                break;
            case 5:
                ship = ship6;
                break;
        }
        ship.transform.GetChild(index + 1).GetComponent<UnityEngine.UI.Image>().sprite = null;
        ship.transform.GetChild(index + 1).GetComponent<UnityEngine.UI.Image>().color = dark;
    }
}
