using UnityEngine;
using System.Collections;

public class TriggerOnApproach : MonoBehaviour {
    public string methodId;
    public GameObject target;
    public float threshold;
    private bool called;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null)
            return;

        if (!called)
        {
            if (Vector3.Distance(transform.position, target.transform.position)
                <= threshold)
            {
                GameObject.Find("GameLogic").GetComponent<GameEventHandler>().
                    callEvent(methodId);
                called = true;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position)
                > threshold)
            {
                called = false;
            }
        }
    }
}
