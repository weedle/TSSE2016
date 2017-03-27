using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// This is a quest handler based on Jenne's item handler
public class EventListHandler : MonoBehaviour
{
    void Start()
    {
        removeAllItems();
        EventDefinitions.Event newEvent1 = new EventDefinitions.Event("made a new event!", "event1");
        EventDefinitions.Event newEvent2 = new EventDefinitions.Event("oh and look another one!", "event2");
        EventDefinitions.Event newEvent3 = new EventDefinitions.Event("third one for luck", "event3");
        addEvent(newEvent1);
        addEvent(newEvent2);
        addEvent(newEvent3);
        addEmpty();
        addEmpty();
        addEmpty();
        GetComponent<UnityEngine.UI.ScrollRect>().verticalNormalizedPosition = 1;
    }

    // add the item
    public void addEvent(EventDefinitions.Event newEvent)
    {
        // We have an example item in the PrefabHost
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getEventItem();

        // Initially has 1 object
        newItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().
            text = newEvent.desc;

        // Add to listview
        newItem.transform.SetParent(transform.GetChild(0).transform, false);
    }

    // add an empty quest so the quest view looks nice
    public void addEmpty()
    {
        // We have an example item in the PrefabHost
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getEventEmptyItem();

        // Add to listview
        newItem.transform.SetParent(transform.GetChild(0).transform, false);
    }

    // reduce quantity if present, otherwise remove item
    public void removeAllItems()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }
    }
}
