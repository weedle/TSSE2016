using UnityEngine;
using System.Collections;

public class Bounds : MonoBehaviour
{
    static float vertExtent;
    static float horzExtent;

    static float xbound;
    static float ybound;

    // Use this for initialization
    void Start ()
    {
        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        xbound = horzExtent * 0.98f;
        ybound = vertExtent * 0.98f;

        Vector3 bottomLeft = new Vector3(-xbound, -ybound);
        Vector3 topRight = new Vector3(xbound, ybound);
        ShipDefinitions.DrawSquare(bottomLeft, topRight, Color.gray, 0.3f);
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    public static Vector2 getRandPosInBounds()
    {
        Vector2 randPt = Vector2.zero;

        randPt.x = Random.Range(-xbound, xbound);
        randPt.y = Random.Range(-ybound, ybound);

        return randPt;
    }

    public static Vector3 getPosInBounds(Vector3 position)
    {
        vertExtent = Camera.main.orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        xbound = horzExtent * 0.90f;
        ybound = vertExtent * 0.90f;

        return getPosInBounds(position, xbound, ybound);
    }

    public static Vector3 getPosInBounds(Vector3 position, float givenXBound, float givenYBound)
    {
        Vector3 temp = position;

        if (position.x < -givenXBound)
        {
            temp.Set(givenXBound, position.y, position.z);
        }
        if (position.x > givenXBound)
        {
            temp.Set(-givenXBound, position.y, position.z);
        }
        if (position.y > givenYBound)
        {
            temp.Set(position.x, -givenYBound, position.z);
        }
        if (position.y < -givenYBound)
        {
            temp.Set(position.x, givenYBound, position.z);
        }
        position = temp;

        return position;
    }

    public static Vector3 getPosInBounds(Vector3 position, Vector3 bgPos, Vector3 bgSize)
    {
        Vector3 temp = position;

        if (position.x < bgPos.x - bgSize.x / 2)
            temp.Set(bgPos.x + bgSize.x / 2, bgPos.y, position.z);
        if (position.x > bgPos.x + bgSize.x / 2)
            temp.Set(bgPos.x - bgSize.x / 2, bgPos.y, position.z);
        if (position.y < bgPos.y - bgSize.y / 2)
            temp.Set(bgPos.x, bgPos.y + bgSize.y / 2, position.z);
        if (position.y > bgPos.y + bgSize.y / 2)
            temp.Set(bgPos.x, bgPos.y - bgSize.y / 2, position.z);

        position = temp;

        return position;
    }
}
