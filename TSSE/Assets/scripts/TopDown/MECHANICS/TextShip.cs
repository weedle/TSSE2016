using UnityEngine;
using System.Collections;

public class ShipLabel : MonoBehaviour
{
    public GameObject target;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 newPos = target.transform.position;
        //newPos.x += 0.2f;
        newPos.y -= 0.35f;
        transform.position = newPos;
    }

    public void setText(string text)
    {
        GetComponent<TextMesh>().text = text;
    }
}
