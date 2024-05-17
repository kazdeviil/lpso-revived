using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FashionationLogic : MonoBehaviour
{
    // UI references
    [SerializeField] private TextMeshProUGUI Timer;
    [SerializeField] private int MinutesCount;
    [SerializeField] private int SecondsCount;

    [SerializeField] private TextMeshProUGUI ScoreTxt;
    [SerializeField] private int Score;

    [SerializeField] private TextMeshProUGUI HeartTxt;
    [SerializeField] private Image HeartIcon;
    [SerializeField] private Sprite PinkHeart;
    [SerializeField] private Sprite GoldHeart;
    [SerializeField] private int HeartScore;

    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private int Level;


    // logic
    [SerializeField] private Sprite[] AllItemSprites;
    public List<int> CurrentItems;
    [SerializeField] private int MaxCurrentItems;
    [SerializeField] private GameObject FashionItem;
    [SerializeField] private Transform FashionItemParent;
    [SerializeField] private Vector3 SpawnPosTop;


    void Start()
    {
        MaxCurrentItems = 3;
        ChooseSprites();
        for (int i = 0; i < 5; i++)
        {
            SpawnItem();
        }
    }

    // chooses which items will be on that level
    void ChooseSprites()
    {
        int random = Random.Range(0, AllItemSprites.Length);
        if (CurrentItems.Count < MaxCurrentItems)
        {
            if (CurrentItems.Count == 0)
            {
                CurrentItems.Add(random);
                Debug.Log("Added item no. " + random + " on start.");
                ChooseSprites();
            }
            else
            {
                if (CurrentItems.Contains(random))
                {
                    Debug.Log("Did not add no. " + random + " because it was already in list. Retrying.");
                }
                else
                {
                    CurrentItems.Add(random);
                    Debug.Log("Added no. " + random);
                    
                }
                ChooseSprites();
            }
        }
    }

    void SpawnItem()
    {
        GameObject fashionItem = Instantiate(FashionItem, new Vector3(FashionItemParent.transform.position.x, FashionItemParent.transform.position.y), Quaternion.identity, FashionItemParent); ;
        int random = Random.Range(0, CurrentItems.Count);
        Debug.Log("Instantiated item of no. " + CurrentItems[random]);
        fashionItem.GetComponent<FashionationItem>().ItemImage = AllItemSprites[CurrentItems[random]];
        fashionItem.GetComponent<FashionationItem>().SetStats(CurrentItems[random]);
        Debug.Log("Item's stat: " + fashionItem.GetComponent<FashionationItem>().ItemNumber);
    }
}
