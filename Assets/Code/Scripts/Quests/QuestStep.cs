using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "ScriptableObjects/QuestStep")]
public class QuestStep : ScriptableObject
{
    // Create a questStep for every part/objective of an overarching quest Quest
    //  Add the questStep to the corresponding Quest.questSteps[] in order

    // Style conventions:
    // Quest filename       Word    where Word is a unique short name of a quest
    // QuestStep filename   Word0   where 0 is the corresponding step number of the quest it's under, starting at 0
    //                              For now it must be a single digit number until QuestsMaster is updated lol

    public string text;         // Description of the step, ie what the player should do to finish the step
    public int progress = 1;    // Amount of things that need to be done for the step to be considered finished

    // For Adventures:
    public NPCBehavior npc;             // The NPC to talk to for this step
    public Conversation conversation;   // What conversation to have with the NPC at this step
}
