using UnityEngine;
using TMPro;

public class QuestDetails : MonoBehaviour
{
    public Quest quest;

    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdateDetails()
    {
        title.text = quest.title;
        desc.text = quest.desc;
    }
}
