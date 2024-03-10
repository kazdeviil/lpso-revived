using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInteract : MonoBehaviour
{
    public Button flowerButton;
    public Button bushButton;
    public Button treeButton;
    public Button digButton;

    public TMPro.TextMeshProUGUI luckLevel;
    public TMPro.TextMeshProUGUI luckExp;
    public TMPro.TextMeshProUGUI itemReceived;

    [HideInInspector] public string receivedItem;

    [HideInInspector] public int luckyLevel = 1;
    [HideInInspector] public int luckyExp = 0;
    [HideInInspector] public int levelupThreshold = 4;
    [HideInInspector] public bool luckyMaxxed;

    // i cant figure out arrays of arrays please help streamline the code
    // i was thinking to assign these interactables an integer corresponding to the array index of their loot tables

    [HideInInspector]
    public string[] flowerTable = new string[]
{
        "Pink Daisy",
        "Red Daisy",
        "Yellow Daisy",
        "Purple Daisy",
        "White Daisy",
        "Blue Daisy"
};
    [HideInInspector]
    public string[] bushTable = new string[]
    {
        "Red Tulip",
        "White Tulip",
        "Yellow Tulip",
        "Purple Tulip",
        "Blue Tulip",
        "Pink Tulip"
    };
    [HideInInspector]
    public string[] treeTable = new string[]
    {
        "Sunny Banana",
        "Green Apple",
        "Red Apple",
        "Yellow Apple",
        "White Apple",
        "Golden Apple"
    };
    [HideInInspector]
    public string[] digTable = new string[]
    {
        "Orange Carrot",
        "Purple Carrot",
        "Red Carrot",
        "White Carrot",
        "Yellow Carrot",
        "Golden Carrot"
    };

    public void Start()
    {
        flowerButton.onClick.AddListener(RollFlower);
        bushButton.onClick.AddListener(RollBush);
        treeButton.onClick.AddListener(RollTree);
        digButton.onClick.AddListener(RollDig);
    }

    public void RollFlower()
    {
        receivedItem = "";
        if (luckyLevel == 1)
        {
            int kibbleAmount = UnityEngine.Random.Range(1, 3);
            receivedItem = kibbleAmount.ToString() + " Kibble";
        }
        else
        {
            int randomItemType = UnityEngine.Random.Range(0, 2);
            if (randomItemType == 0)
            {
                if (luckyMaxxed)
                {
                    int random = UnityEngine.Random.Range(0, 6);
                    receivedItem = flowerTable[random];
                }
                else
                {
                    int random = UnityEngine.Random.Range(0, luckyLevel-1);
                    receivedItem = flowerTable[random];
                }
            }
            else
            {
                int kibbleAmount = UnityEngine.Random.Range(1, luckyLevel ^ 2 + 2);
                receivedItem = kibbleAmount.ToString() + " Kibble";
            }
        }
        itemReceived.text = "+" + receivedItem;
        AddLuckExp();
    }

    public void RollBush()
    {
        receivedItem = "";
        if (luckyLevel == 1)
        {
            int kibbleAmount = UnityEngine.Random.Range(1, 3);
            receivedItem = kibbleAmount.ToString() + " Kibble";
        }
        else
        {
            int randomItemType = UnityEngine.Random.Range(0, 2);
            if (randomItemType == 0)
            {
                if (luckyMaxxed)
                {
                    int random = UnityEngine.Random.Range(0, 6);
                    receivedItem = bushTable[random];
                }
                else
                {
                    int random = UnityEngine.Random.Range(0, luckyLevel-1);
                    receivedItem = bushTable[random];
                }
            }
            else
            {
                int kibbleAmount = UnityEngine.Random.Range(1, luckyLevel ^ 2 + 2);
                receivedItem = kibbleAmount.ToString() + " Kibble";
            }
        }
        itemReceived.text = "+" + receivedItem;
        AddLuckExp();
    }

    public void RollTree()
    {
        receivedItem = "";
        if (luckyLevel == 1)
        {
            int kibbleAmount = UnityEngine.Random.Range(1, 3);
            receivedItem = kibbleAmount.ToString() + " Kibble";
        }
        else
        {
            int randomItemType = UnityEngine.Random.Range(0, 2);
            if (randomItemType == 0)
            {
                if (luckyMaxxed)
                {
                    int random = UnityEngine.Random.Range(0, 6);
                    receivedItem = treeTable[random];
                }
                else
                {
                    int random = UnityEngine.Random.Range(0, luckyLevel-1);
                    receivedItem = treeTable[random];
                }
            }
            else
            {
                int kibbleAmount = UnityEngine.Random.Range(1, luckyLevel ^ 2 + 2);
                receivedItem = kibbleAmount.ToString() + " Kibble";
            }
        }
        itemReceived.text = "+" + receivedItem;
        AddLuckExp();
    }

    public void RollDig()
    {
        receivedItem = "";
        if (luckyLevel == 1)
        {
            int kibbleAmount = UnityEngine.Random.Range(1, 3);
            receivedItem = kibbleAmount.ToString() + " Kibble";
        }
        else
        {
            int randomItemType = UnityEngine.Random.Range(0, 2);
            if (randomItemType == 0)
            {
                if (luckyMaxxed)
                {
                    int random = UnityEngine.Random.Range(0, 6);
                    receivedItem = digTable[random];
                }
                else
                {
                    int random = UnityEngine.Random.Range(0, luckyLevel-1);
                    receivedItem = digTable[random];
                }
            }
            else
            {
                int kibbleAmount = UnityEngine.Random.Range(1, luckyLevel ^ 2 + 2);
                receivedItem = kibbleAmount.ToString() + " Kibble";
            }
        }
        itemReceived.text = "+" + receivedItem;
        AddLuckExp();
    }


    public void AddLuckExp()
    {
        if (!luckyMaxxed)
        {
            luckyExp += 1;
            if (luckyExp == levelupThreshold) // level up
            {
                luckyExp = 0;
                luckyLevel += 1;
                levelupThreshold = luckyLevel * 4; // level 1 = 4, level 2 = 8, level 3 = 12, etc
            }
            if (luckyLevel == 7) // if reached max level
            {
                luckExp.text = "Maxxed";
                luckyMaxxed = true;
            }
            else
            {
                luckExp.text = luckyExp.ToString() + "/" + levelupThreshold.ToString();
            }
        }
        else
        {
            luckExp.text = "Maxxed";
        }
        luckLevel.text = luckyLevel.ToString();
    }
}
