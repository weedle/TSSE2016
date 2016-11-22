using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    public GameObject target;
    private float normalLength;
	// Use this for initialization
	void Start () {
        Transform t = transform.Find("HealthFullPart");
        normalLength = t.localScale.x;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null)
        {
            //Object.Destroy(transform.parent.gameObject);
            return;
        }
        

        Vector3 newPos = target.transform.position;
        newPos.x -= 0.2f;
        newPos.y += 0.35f;
        transform.position = newPos;

        //Transform t = transform.Find("HealthFullPart");

        //t.localScale -= new Vector3(0.1f, 0, 0);
    }

    public void setHealthPercentage(float perc)
    {
        Transform t = transform.Find("HealthFullPart");

        Vector3 health = t.localScale;
        health.x = normalLength * perc;

        if (perc <= 0) health.x = 0;
        //t.localScale -= new Vector3(0.1f, 0, 0);
        //t.localScale.Set(t.localScale.x * perc, t.localScale.y, t.localScale.z);
        t.localScale = health;
    }
}
