using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    public string desc;
    public QuestStep[] questSteps;
}
