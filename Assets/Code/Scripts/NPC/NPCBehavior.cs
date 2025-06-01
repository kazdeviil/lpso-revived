using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // needs to be expanded upon heavily lol

    public string NPCName;              // The name that the text appearing at the top of the speech bubble will be
    public Conversation conversation;   // The next conversation to be had by clicking on the NPC
    public TextColor textColor;         // The color of the NPC Name in the speech bubble

    private Dictionary<Quest, int> quests = new Dictionary<Quest, int>();   // placeholder, holds what step of the quest the player is at
    public Quest tutorial;              // kind of placeholder, need a reference to the tutorial somewhere in the player probably
    public Conversation defaultConvo;   // Conversation when the NPC doesn't have any special quest text
    public Conversation[] convos;       // placeholder i think, for each tutorial conversation
                                        // maybe could expand QuestStep to hold a NPCBehavior and Conversation?

    public enum TextColor
    {
        Purple,
        Orange
    }

    public void StartQuest()
    {
        // placeholder, activates the tutorial 
        quests.Add(tutorial, 0);
        conversation = convos[0];
    }

    public void UpdateConvo()
    {
        // oh boy
        // giant if-statement holding every NPC and stuff, to change each corresponding conversation step to the right one
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
