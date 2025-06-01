using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]

[Serializable]
public class Quest : ScriptableObject
{
    public string title;
    public string desc;
    public QuestStep[] questSteps;
}
