using UnityEngine;
using System.Collections;

public class QuestDefinitions : MonoBehaviour {
    public struct Quest
    {
        public int questId;
        public bool completed;
        public string questInfo;
        public Quest(string info)
        {
            questInfo = info;
            questId = info.GetHashCode();
            completed = false;
        }
    }

    public Quest getQuest(int id)
    {
        Quest quest = new Quest();
        quest.questId = id;
        quest.questInfo = PlayerPrefs.GetString("Quest-" + id + "-info");
        quest.completed = (PlayerPrefs.GetInt("Quest-" + id + "-status") == 1);
        return quest;
    }

    public Quest setQuest(string info)
    {
        Quest quest = new Quest();
        quest.questInfo = info;
        quest.questId = info.GetHashCode();
        quest.completed = false;
        PlayerPrefs.SetString("Quest-" + quest.questId + "-info", info);
        PlayerPrefs.SetInt("Quest-" + quest.questId + "-status", 0);
        return quest;
    }

    public Quest setQuest(Quest quest)
    {
        return setQuest(quest.questInfo);
    }
}
