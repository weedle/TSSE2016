using UnityEngine;
using System.Collections;

public class CameraHelper : MonoBehaviour {
    private Vector3 bottomLeft;
    private Vector3 topRight;
    // Use this for initialization
    void Start ()
    {
    }
	
    void init()
    {
        float vertExtent = Camera.main.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        float xbound = horzExtent * 0.9f;
        float ybound = vertExtent * 0.9f;

        bottomLeft = new Vector3(-xbound, -ybound);
        topRight = new Vector3(xbound, ybound);
        bottomLeft += Camera.main.transform.position;
        topRight += Camera.main.transform.position;
        ShipDefinitions.DrawSquare(bottomLeft, topRight, Color.gray, 0.3f);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public Vector3 getTopRight()
    {
        init();
        return topRight;
    }

    public Vector3 getBottomRight()
    {
        init();
        return new Vector3(topRight.x, bottomLeft.y, 0);
    }

    public Vector3 getTopLeft()
    {
        init();
        return new Vector3(bottomLeft.x, topRight.y, 0);
    }

    public Vector3 getBottomLeft()
    {
        init();
        return bottomLeft;
    }

    public Vector3 getTopMid()
    {
        init();
        init();
        return new Vector3((topRight.x + bottomLeft.x) / 2,
            topRight.y, 0);
    }

    public Vector3 getBottomMid()
    {
        init();
        return new Vector3((topRight.x + bottomLeft.x) / 2,
            bottomLeft.y, 0);
    }

    public Vector3 getRightMid()
    {
        init();
        return new Vector3(topRight.x, (topRight.y + bottomLeft.y) / 2, 0);
    }

    public Vector3 getLeftMid()
    {
        init();
        return new Vector3(bottomLeft.x, (topRight.y + bottomLeft.y) / 2, 0);
    }
}
