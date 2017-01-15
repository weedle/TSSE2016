using UnityEngine;
using System.Collections;

public class ItemListHandler : MonoBehaviour {
    public void addItem(string text)
    {
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getMerchantItem();
        newItem.transform.SetParent(transform.GetChild(0).transform, false);
    }

    public void removeItem(string text)
    {
        print(transform.GetChild(0).childCount);
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if(transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<UnityEngine.UI.Text>().text.Equals(text))
            {
                GameObject.Destroy(transform.GetChild(0).GetChild(i).gameObject);
                return;
            }
        }
    }
}
