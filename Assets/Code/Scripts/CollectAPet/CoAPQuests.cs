using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAPQuests : MonoBehaviour
{
    [HideInInspector] public bool CoAPActive;

    [HideInInspector]
    public int[] QuestCount = new int[] // how many quests each pet has
    {
        2, 3
    };
    [HideInInspector]
    public string[] ActiveQuestPet = new string[] // the current active quest selected by player
    {
        "Pup to the Rescue!",
        "Fruit Kangaroo"
    };
    [HideInInspector]
    public bool[] ActiveQuestPetBool = new bool[] // checks if the player already has a quest active
    {
        false,
        false
    };
    [HideInInspector]
    public bool[] GotPet = new bool[]
    {
        false,
        false
    };

    [HideInInspector]
    public string[] Quest1 = new string[] // quest 1 string
    {
        "Get a Sunny Banana from a tree",
        "Pick a red tulip from a bush"
    };
    [HideInInspector]
    public bool[] Quest1Done = new bool[] // if quest 1 of a particular pet is finished
    {
        false,
        false
    };
    [HideInInspector]
    public bool[] Quest1Counter = new bool[] // if quest 1 has an associated count (mutliple items to complete quest)
    {
        false,
        false
    };
    [HideInInspector]
    public int[] Quest1Count = new int[] // number of count items required (doesnt matter if Quest1Counter[SelectedPet] is false
    {
        0,
        0
    };
    [HideInInspector]
    public int[] Quest1CountPlayer = new int[] // number of count items player received during quest active
    {
        0,
        0
    };

    [HideInInspector]
    public string[] Quest2 = new string[]
    {
        "Dig up 3 White Carrots",
        "Shake a tree 5 times"
    };
    [HideInInspector]
    public bool[] Quest2Done = new bool[]
    {
        false,
        false
    };
    [HideInInspector]
    public bool[] Quest2Counter = new bool[]
    {
        true,
        true
    };
    [HideInInspector]
    public int[] Quest2Count = new int[]
    {
        3, 5 
    };
    [HideInInspector]
    public int[] Quest2CountPlayer = new int[]
    {
        0,
        0
    };

    [HideInInspector]
    public string[] Quest3 = new string[]
    {
        "None",
        "Dig up a Purple Carrot"
    };
    [HideInInspector]
    public bool[] Quest3Done = new bool[] // set to true if pet has no quest
    {
        true,
        false
    };
    [HideInInspector]
    public bool[] Quest3Counter = new bool[]
    {
        false,
        false
    };
    [HideInInspector]
    public int[] Quest3Count = new int[]
    {
        0,
        0
    };
    [HideInInspector]
    public int[] Quest3CountPlayer = new int[]
    {
        0,
        0
    };

    [HideInInspector]
    public string[] Quest4 = new string[]
    {
        "None",
        "None"
    };
    [HideInInspector]
    public bool[] Quest4Done = new bool[]
    {
        true,
        true
    };
    [HideInInspector]
    public bool[] Quest4Counter = new bool[]
    {
        false,
        false
    };
    [HideInInspector]
    public int[] Quest4Count = new int[]
    {
        0,
        0
    };
    [HideInInspector]
    public int[] Quest4CountPlayer = new int[]
    {
        0,
        0
    };

    // Start is called before the first frame update
    void Awake()
    {
        // i think any quests that need to be finished during the play session need to be reset here
    }
}
