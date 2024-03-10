using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInteract : MonoBehaviour
{
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

    // object interaction
    // probably make this a private array instead
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
    [HideInInspector] public int random;
    [HideInInspector] public int randomRollMax;



    CoAPQuests coapQuests;
    [SerializeField] GameObject QuestManager;
    public Button[] CoAPButtons;
    public int SelectedPet;
    public Button CoAPDalmatian;
    public Button CoAPKangaroo;
    public TMPro.TextMeshProUGUI PetName;
    public TMPro.TextMeshProUGUI Quest1;
    public TMPro.TextMeshProUGUI Quest2;
    public TMPro.TextMeshProUGUI Quest3;
    public TMPro.TextMeshProUGUI Quest4;
    public TMPro.TextMeshProUGUI infoText;
    [HideInInspector] public string Quest1Text;
    [HideInInspector] public bool Quest1Done;
    [HideInInspector] public string Quest2Text;
    [HideInInspector] public bool Quest2Done;
    [HideInInspector] public string Quest3Text;
    [HideInInspector] public bool Quest3Done;
    [HideInInspector] public string Quest4Text;
    [HideInInspector] public bool Quest4Done;




    // i cant figure out arrays of arrays please help streamline the code
    // i was thinking to assign these interactables an integer corresponding to the array index of their loot tables
    // anyway, holds full loot table information
    // TODO: decouple later

    public void Awake() // assigns methods to buttons, necessary to share luck exp info
    {
        flowerButton.onClick.AddListener(RollFlower);
        bushButton.onClick.AddListener(RollBush);
        treeButton.onClick.AddListener(RollTree);
        digButton.onClick.AddListener(RollDig);

        coapQuests = QuestManager.GetComponent<CoAPQuests>();
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
                if (coapQuests.CoAPActive)
                {
                    if(SelectedPet == 0)
                    {
                        if (!coapQuests.Quest1Done[0])
                        {
                            if (random == 0)
                            {
                                coapQuests.Quest1Done[0] = true;
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
                if (coapQuests.CoAPActive)
                {
                    if (SelectedPet == 0)
                    {
                        if (!coapQuests.Quest2Done[0])
                        {
                            if (random == 3)
                            {
                                coapQuests.CoAPDalmatianCarrotCount += 1;
                                if (coapQuests.CoAPDalmatianCarrotCount >= 3)
                                {
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

    public void SelectPet(int buttonID)
    {
        for (int i = 0; i < coapQuests.ActiveQuestPetBool.Length; i++)
        {
            coapQuests.ActiveQuestPetBool[i] = false;
        }
        Debug.Log("Attempting to activate " + coapQuests.ActiveQuestPet[buttonID]);
        SelectedPet = buttonID;
        coapQuests.ActiveQuestPetBool[buttonID] = true;
        Debug.Log(coapQuests.ActiveQuestPetBool[0].ToString() + " " + coapQuests.ActiveQuestPetBool[1]);
        UpdateQuest();
    }

    public void ActivateQuest()
    {
        if (!coapQuests.CoAPActive)
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
        coapQuests.CoAPActive = true;
        coapQuests.ActiveQuestPetBool[SelectedPet] = true;
        
        PetName.text = "Current:\n" + coapQuests.ActiveQuestPet[SelectedPet];
        infoText.text = "";

        // sets quest 1 text
        Quest1Text = coapQuests.Quest1[SelectedPet];
        if (coapQuests.Quest1Done[SelectedPet])
        {
            Quest1Text += "\n(Completed)";
        }
        Quest1.text = Quest1Text;

        // sets quest 2 text
        Quest2Text = coapQuests.Quest2[SelectedPet];
        if (Quest2Done)
        {
            Quest2Text += "\n(Completed)";
        }
        else
        {
            Quest2Text += "\n(" + (3 - coapQuests.CoAPDalmatianCarrotCount).ToString() + " left)";
        }
        Quest2.text = Quest2Text;

        // sets quest 3 text
        Quest3Text = coapQuests.Quest3[SelectedPet];
        if (Quest3Done)
        {
            Quest3Text += "\n(Completed)";
        }
        else
        {
            Quest3Text += "\n(" + (3 - coapQuests.CoAPDalmatianCarrotCount).ToString() + " left)";
        }
        Quest3.text = Quest3Text;
    }
}
