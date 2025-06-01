using System.Collections.Generic;
using UnityEngine;

public class QuestsMaster : MonoBehaviour
{
    public Quest[] quests;      // Every quest
    public QuestStep[] steps;   // Every step in every quest (oof)
    public Dictionary<Quest, List<QuestStep>> questDict = new();

    // This basically keeps track of every quest and step within each quest
    // All you need to do is add every quest and step into the list (in numerical order) and it'll figure out the rest
    // As long as the naming conventions in Quest and QuestStep were followed then there should be no issue
    private void Start()
    {
        foreach (Quest quest in quests)
        {
            // print("Quest called " + quest.name + " found");
            questDict.Add(quest, new List<QuestStep>());
            foreach (QuestStep step in steps)
            {
                string stepname = step.name.Remove(step.name.Length-1);
                if (stepname == quest.name)
                {
                    // print("Step called " + step.name + " found, shortened to " + stepname);
                    questDict[quest].Add(step);
                }
            }
            //for (int i = 0; i < questDict[quest].Count; i++)
            {
                //print(quest.name + " and " + questDict[quest][i].name);
            }
        }
    }
}
