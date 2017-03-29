using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// This is a quest handler based on Jenne's item handler
public class QuestListHandler : MonoBehaviour
{

    void Start()
    {
        removeAllItems();
        QuestDefinitions.Quest quest = new QuestDefinitions.Quest("TESTING QUEST");
        addQuest(quest);
        addEmpty();
        addEmpty();
        addEmpty();
        /*
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);
        addQuest(quest);*/
        GetComponent<UnityEngine.UI.ScrollRect>().verticalNormalizedPosition = 1;
        //GetComponent<UnityEngine.UI.ScrollRect>().
    }

    // add the item
    public void addQuest(QuestDefinitions.Quest quest)
    {
        // We have an example item in the PrefabHost
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getQuestItem();
        
        // Initially has 1 object
        newItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().
            text = quest.questInfo;

        // Add to listview
        newItem.transform.SetParent(transform.GetChild(0).transform, false);
    }

    // add an empty quest so the quest view looks nice
    public void addEmpty()
    {
        // We have an example item in the PrefabHost
        GameObject newItem = GameObject.Find("GameLogic").
            GetComponent<PrefabHost>().getQuestEmptyItem();

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
