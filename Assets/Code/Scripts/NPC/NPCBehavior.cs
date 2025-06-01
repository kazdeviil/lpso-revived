using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // needs to be expanded upon heavily lol

    public string NPCName;
    public Conversation conversation;

    private Dictionary<Quest, int> quests = new Dictionary<Quest, int>();
    public Quest tutorial;
    public Conversation defaultConvo;
    public Conversation[] convos;

    public void StartQuest()
    {
        quests.Add(tutorial, 0);
        conversation = convos[0];
    }

    public void UpdateConvo()
    {
        if (NPCName == "Felina")
        {
            // not really a good metric but its temporary, if the user doesnt have the quest then the fallback default convo is their conversation
            if (quests.ContainsKey(tutorial))
            {
                print("Tutorial was found");
                for (int i = 0; i < tutorial.questSteps.Length; i++)
                {
                    if (quests[tutorial] == i)
                    {
                        print("Player is on step " + i + " of tutorial");
                        if (i == tutorial.questSteps.Length-1)
                        {
                            conversation = defaultConvo;
                            quests.Remove(tutorial);
                            return;
                        }
                        else
                        {
                            conversation = convos[i + 1];
                        }
                        quests[tutorial]++;
                        print("Player went to step " + quests[tutorial]);
                        return;
                    }
                }
            }
            else
            {
                conversation = defaultConvo;
            }
        }
    }
}
