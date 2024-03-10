using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInteract : MonoBehaviour
{
    // object interaction
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
    [HideInInspector] public bool luckyMaxxed = false;
    [HideInInspector] public int random;
    [HideInInspector] public int randomRollMax;

    // CoAP tracking
    [HideInInspector] public bool CoAPActive = false;
    [HideInInspector] public bool CoAPDalmatianActive = false;
    public bool CoAPDalmatianGot = false;
    [HideInInspector] public bool CoAPDalmatianBanana = false;
    [HideInInspector] public bool CoAPDalmatianCarrot = false;
    [HideInInspector] public int CoAPDalmatianCarrotCount = 0;

    public Button CoAPToggle;
    public TMPro.TextMeshProUGUI PetName;
    public TMPro.TextMeshProUGUI Quest1;
    public TMPro.TextMeshProUGUI Quest2;
    public TMPro.TextMeshProUGUI infoText;
    public string Quest1Text;
    public bool Quest1Done;
    public string Quest2Text;
    public bool Quest2Done;

    // i cant figure out arrays of arrays please help streamline the code
    // i was thinking to assign these interactables an integer corresponding to the array index of their loot tables
    // anyway, holds full loot table information

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

    public void Start() // assigns methods to buttons, necessary to share luck exp info
    {
        flowerButton.onClick.AddListener(RollFlower);
        bushButton.onClick.AddListener(RollBush);
        treeButton.onClick.AddListener(RollTree);
        digButton.onClick.AddListener(RollDig);

        CoAPToggle.onClick.AddListener(ActivateQuest);
    }

    // calls from each loot table and gives random item from loot table or random kibble within specified ranges
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
                    randomRollMax = 6;
                }
                else
                {
                    randomRollMax = luckyLevel - 1;
                }
                random = UnityEngine.Random.Range(0, randomRollMax);
                receivedItem = flowerTable[random];
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
                    randomRollMax = 6;
                }
                else
                {
                    randomRollMax = luckyLevel - 1;
                }
                random = UnityEngine.Random.Range(0, randomRollMax);
                receivedItem = bushTable[random];
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
                    randomRollMax = 6;
                }
                else
                {
                    randomRollMax = luckyLevel-1;
                }
                random = UnityEngine.Random.Range(0, randomRollMax);
                receivedItem = treeTable[random];
                if (CoAPActive)
                {
                    if(CoAPDalmatianActive)
                    {
                        if (!CoAPDalmatianBanana)
                        {
                            if (random == 0)
                            {
                                CoAPDalmatianBanana = true;
                                Quest1Done = true;
                                UpdateQuest();
                            }
                        }
                    }
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
                    randomRollMax = 6;
                }
                else
                {
                    randomRollMax = luckyLevel - 1;
                }
                random = UnityEngine.Random.Range(0, randomRollMax);
                receivedItem = digTable[random];
                if (CoAPActive)
                {
                    if (CoAPDalmatianActive)
                    {
                        if (!CoAPDalmatianCarrot)
                        {
                            if (random == 3)
                            {
                                CoAPDalmatianCarrotCount += 1;
                                if (CoAPDalmatianCarrotCount >= 3)
                                {
                                    CoAPDalmatianCarrot = true;
                                    Quest2Done = true;
                                }
                                UpdateQuest();
                            }
                        }
                    }
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
    // called after every interaction, calculates self, changes (placeholder) ui accordingly
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


    public void ActivateQuest()
    {
        if (!CoAPActive)
        {
            UpdateQuest();
        }
        else
        {
            infoText.text = "You already have a quest in progress!";
        }
    }

    public void UpdateQuest()
    {
        CoAPDalmatianActive = true;
        CoAPActive = true;
        PetName.text = "Current:\n" + GetComponent<CoAPQuests>().DalmatianName;

        // sets quest 1 text
        // needs additional context
        Quest1Text = GetComponent<CoAPQuests>().DalmatianQuest1;
        if (Quest1Done)
        {
            Quest1Text += "\n(Completed)";
        }
        Quest1.text = Quest1Text;

        // sets quest 2 text
        // needs additional context
        Quest2Text = GetComponent<CoAPQuests>().DalmatianQuest2;
        if (Quest2Done)
        {
            Quest2Text += "\n(Completed)";
        }
        else
        {
            Quest2Text += "\n(" + (3 - CoAPDalmatianCarrotCount).ToString() + " left)";
        }
        Quest2.text = Quest2Text;
    }
}
