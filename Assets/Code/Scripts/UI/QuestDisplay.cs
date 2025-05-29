using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public Quest quest;
    public AdventuresUI ui;

    [SerializeField] private TMP_Text title;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        title.text = quest.title;
    }

    public void Expand()
    {
        ui.parent.SetActive(false);
        ui.questDetails.GetComponent<QuestDetails>().quest = quest;
        ui.questDetails.GetComponent<QuestDetails>().UpdateDetails();
        ui.questDetails.SetActive(true);
    }
}
