using UnityEngine;
using System.Collections;

public class AmmoBar : MonoBehaviour
{
    private GameObject target;
    private float normalLength;
    // Use this for initialization
    void Start()
    {
        Transform t = transform.Find("AmmoFullPart");
        normalLength = t.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            //Object.Destroy(transform.parent.gameObject);
            return;
        }


        Vector3 newPos = target.transform.position;
        //newPos.x -= 0.1f;
        newPos.y += 0.30f;
        transform.position = newPos;

        //Transform t = transform.Find("HealthFullPart");

        //t.localScale -= new Vector3(0.1f, 0, 0);
    }

    public void setAmmoPercentage(float perc)
    {
        Transform t = transform.Find("AmmoFullPart");

        Vector3 ammo = t.localScale;
        ammo.x = normalLength * perc;

        if (perc <= 0) ammo.x = 0;
        if (ammo.x > 1) ammo.x = 1;
        //t.localScale -= new Vector3(0.1f, 0, 0);
        //t.localScale.Set(t.localScale.x * perc, t.localScale.y, t.localScale.z);
        t.localScale = ammo;
    }

    public void setTarget(GameObject obj)
    {
        this.target = obj;
    }
}
