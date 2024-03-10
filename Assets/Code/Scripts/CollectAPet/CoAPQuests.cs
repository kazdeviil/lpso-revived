using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAPQuests : MonoBehaviour
{
    [HideInInspector] public bool CoAPActive;
    public bool CoAPDalmatianGot;
    [HideInInspector] public bool CoAPDalmatianActive;
    [HideInInspector] public int CoAPDalmatianCarrotCount = 0;

    [HideInInspector] public bool CoAPLovebugGot;
    [HideInInspector] public bool CoAPLovebugActive;
    [HideInInspector] public bool CoAPLovebugMuffin;
    [HideInInspector] public int CoAPLovebugSplash = 0;
    [HideInInspector] public bool CoAPLovebugFlower;

    [HideInInspector] public bool CoAPDalmatianBowl;
    [HideInInspector] public int CoAPDalmatianBones = 0;
    [HideInInspector] public int CoAPDalmatianMnM = 0;
    [HideInInspector] public int CoAPDalmatianPies = 0;

    [HideInInspector] public bool CoAPOwlGot;
    [HideInInspector] public bool CoAPOwlActive;
    [HideInInspector] public bool CoAPOwlClock;
    [HideInInspector] public int CoAPOwlSlides = 0;

    [HideInInspector] public bool CoAPKangarooGot;
    [HideInInspector] public bool CoAPKangarooActive;
    [HideInInspector]public bool CoAPKangarooWallpaper;
    [HideInInspector] public int CoAPKangarooFruitista = 0;
    [HideInInspector] public int CoAPKangarooBanana = 0;
    [HideInInspector] public bool CoAPKangarooOrange;

    public int[] QuestCount = new int[]
    {
        2, 3
    };
    public string[] ActiveQuestPet = new string[]
    {
        "Pup to the Rescue!",
        "Fruit Kangaroo"
    };
    public bool[] ActiveQuestPetBool = new bool[]
    {
        false,
        false
    };
    public string[] Quest1 = new string[]
    {
        "Get a Sunny Banana from a tree",
        "Pick a red tulip from a bush"
    };
    public bool[] Quest1Done = new bool[]
    {
        false,
        false
    };
    public string[] Quest2 = new string[]
    {
        "Dig up 3 White Carrots",
        "Shake a tree 5 times"
    };
    public bool[] Quest2Done = new bool[]
    {
        false,
        false
    };
    public string[] Quest3 = new string[]
    {
        "None",
        "Dig up a Purple Carrot"
    };
    public bool[] Quest3Done = new bool[]
    {
        false,
        false
    };
    public string[] Quest4 = new string[]
    {
        "None",
        "None"
    };
    public bool[] Quest4Done = new bool[]
    {
        false,
        false
    };

    // Start is called before the first frame update
    void Awake()
    {
        // i think any quests that need to be finished during the play session need to be reset here
        CoAPOwlSlides = 0;
    }
}
