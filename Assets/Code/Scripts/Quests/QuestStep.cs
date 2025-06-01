using UnityEngine;

[CreateAssetMenu(fileName = "QuestStep", menuName = "ScriptableObjects/QuestStep")]
public class QuestStep : ScriptableObject
{
    public string text;
    public int progress = 1;
}
