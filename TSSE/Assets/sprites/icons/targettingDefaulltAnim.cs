using UnityEngine;
using System.Collections;

public class targettingDefaulltAnim : MonoBehaviour {
    private float maxDisplacement = 0.08f;
    private Vector2 dir;
    private float dirChangeTimer;
	// Use this for initialization
	void Start () {
        dir = Random.insideUnitCircle;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(
            new Vector3(0, 0, 100 * dir.SqrMagnitude() * Time.deltaTime));

        Vector3 pos = transform.position - transform.parent.position;
        //{
            if((pos.x > maxDisplacement && dir.x > 0) ||
            (pos.x < -maxDisplacement && dir.x < 0))
            {
                dir.x *= -1;
            }
            if ((pos.y >= maxDisplacement && dir.y > 0) ||
            (pos.y <= -maxDisplacement && dir.y < 0))
            {
                dir.y *= -1;
            }
            transform.position =
                    transform.parent.position + 
                    new Vector3(pos.x + 4 * maxDisplacement * Time.deltaTime * dir.x,
                                pos.y + 4 * maxDisplacement * Time.deltaTime * dir.y, 0);

        dirChangeTimer += Time.deltaTime;

        if(dirChangeTimer > 3)
        {
            dirChangeTimer = 0;
            dir = Random.insideUnitCircle;
        }
    }
}
