using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoAPQuests : MonoBehaviour
{
    [HideInInspector]
    public bool CoAPLovebugGot = false;
    [HideInInspector]
    public bool CoAPLovebugActive = false;
    [HideInInspector]
    public bool CoAPLovebugMuffin = false;
    [HideInInspector]
    public int CoAPLovebugSplash = 0;
    [HideInInspector]
    public bool CoAPLovebugFlower = false;

    [HideInInspector]
    public bool CoAPDalmatianGot = false;
    [HideInInspector]
    public bool CoAPDalmatianActive = false;
    [HideInInspector]
    public bool CoAPDalmatianBowl = false;
    [HideInInspector]
    public int CoAPDalmatianBones = 0;
    [HideInInspector]
    public int CoAPDalmatianMnM = 0;
    [HideInInspector]
    public int CoAPDalmatianPies = 0;

    [HideInInspector]
    public bool CoAPOwlGot = false;
    [HideInInspector]
    public bool CoAPOwlActive = false;
    [HideInInspector]
    public bool CoAPOwlClock = false;
    [HideInInspector]
    public int CoAPOwlSlides = 0;

    [HideInInspector]
    public bool CoAPKangarooGot = false;
    [HideInInspector]
    public bool CoAPKangarooActive = false;
    [HideInInspector]
    public bool CoAPKangarooWallpaper = false;
    [HideInInspector]
    public int CoAPKangarooFruitista = 0;
    [HideInInspector]
    public int CoAPKangarooBanana = 0;
    [HideInInspector]
    public bool CoAPKangarooOrange = false;

    // Start is called before the first frame update
    void Start()
    {
        // i think any quests that need to be finished during the play session need to be reset here
        CoAPOwlSlides = 0;
    }

    public void CheckLovebug()
    {
        if (CoAPLovebugMuffin && CoAPLovebugFlower && CoAPLovebugSplash >= 4)
        {
            CoAPLovebugGot = true;
            CoAPLovebugActive = false;
            // run some function that shows you achieved the pet and then add pet to list
        }
    }
    public void CheckDalmatian()
    {
        if (CoAPDalmatianBowl && CoAPDalmatianBones >= 5 && CoAPDalmatianMnM >= 3 && CoAPDalmatianPies >= 2)
        {
            CoAPDalmatianGot = true;
            CoAPDalmatianActive = false;
            // the code
        }
    }
    public void CheckOwl()
    {
        if (CoAPOwlClock && CoAPOwlSlides >= 2)//NUMBER OF SLIDES
        {
            CoAPOwlGot = true;
            CoAPOwlActive = false;
            // the code again
        }
    }


    // strings

    public string DalmatianName = "Pup to the Rescue";
    public string DalmatianQuest1 = "Get a Sunny Banana from a tree";
    public string DalmatianQuest2 = "Dig up 3 White Carrots";
}
