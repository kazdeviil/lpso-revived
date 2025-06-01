using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]

[Serializable]
public class Quest : ScriptableObject
{
    // Create a Quest for each overarching quest which holds several steps QuestStep

    // Style conventions:
    // Quest filename       Word    where Word is a unique short name of a quest
    // QuestStep filename   Word0   where 0 is the corresponding step number of the quest it's under, starting at 0

    public string title;            // Title or name of the Quest
    public string desc;             // Intended to be either the "finished quest" text in the scrapbook, or "pet description" text for CoAPs
    public QuestStep[] questSteps;  // Ordered list of every step for the specific quest
}
