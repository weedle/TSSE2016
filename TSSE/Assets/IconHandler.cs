using UnityEngine;
using System.Collections;

public class IconHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setNum(int num)
    {
        if (num == 0)
            Destroy(gameObject);
        transform.GetChild(0).GetChild(0).GetChild(0).
            GetComponent<UnityEngine.UI.Text>().text = num.ToString();
    }

    public void decOne()
    {
        Transform textChild = transform.GetChild(0).GetChild(0).GetChild(0);
        int num = int.Parse(textChild.GetComponent<UnityEngine.UI.Text>().text);
        num--;
        print(num);
        if (num == 0)
        {
            Destroy(gameObject);
        }
        textChild.GetComponent<UnityEngine.UI.Text>().text = num.ToString();
    }
}
