using System.Collections.Generic;
using UnityEngine;

public class AdventuresUI : MonoBehaviour
{
    [SerializeField] private GameObject uiSlot;

    public GameObject questDetails;

    [SerializeField] private List<Quest> adventuresList;

    [SerializeField] private GameObject toggled;
    public GameObject parent;

    public QuestsMaster questsMaster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questDetails.SetActive(false);
        for (int i = 0; i < adventuresList.Count; i++)
        {
            GameDataManager.Instance.adventures.TryAdd(adventuresList[i], false);
            GameDataManager.Instance.advProgress.TryAdd(adventuresList[i], 0);
            print("Added or found " + adventuresList[i].title + " with progress " + GameDataManager.Instance.advProgress[adventuresList[i]]);
        }
        foreach (KeyValuePair<Quest, int> entry in GameDataManager.Instance.advProgress)
        {
            print(entry.Key.title + entry.Value);
        }
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (KeyValuePair<Quest, bool> entry in GameDataManager.Instance.adventures)
        {
            GameObject slot = Instantiate(uiSlot, parent.gameObject.transform.position, Quaternion.identity, parent.gameObject.transform);
            slot.GetComponent<QuestDisplay>().quest = entry.Key;
            slot.GetComponent<QuestDisplay>().ui = this;
            print("Added " + entry.Key.title);
        }
    }

    public void ViewAll()
    {
        parent.SetActive(true);
        questDetails.SetActive(false);
    }

    public void ToggleView()
    {
        toggled.SetActive(!toggled.activeSelf);
        if (toggled.activeSelf)
        {
            questDetails.SetActive(false);
            parent.SetActive(true);
        }
    }

    public void GiveQuest(Quest quest)
    {
        GameDataManager Data = GameDataManager.Instance;
        Data.adventures.TryAdd(quest, false);
        Data.advProgress.TryAdd(quest, 0);
        Data.saveGame();
        Data.loadGame();
    }
}
