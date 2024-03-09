using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemInteract : MonoBehaviour
{
    public GameObject lootTableProvider;

    public Button flowerButton;
    public Button treeButton;

    public int luckyLevel = 1;
    public int luckyExp = 0;
    public int levelupThreshold = 10;

    public void RollFlower()
    {
        int normalLoot = lootTableProvider.GetComponent<LootTables>().flowerTableNormal.Length;
        int rareLoot = lootTableProvider.GetComponent<LootTables>().flowerTableRare.Length;

        // decides what loot table to choose from
        int random = UnityEngine.Random.Range(0, 10);
        if (random <= 4)
        {
            int kibbleAmount = UnityEngine.Random.Range(1, 3);
            Debug.Log("+ " + kibbleAmount.ToString() + " Kibble!");
        }
        else
        {
            if (random == 9)
            {
                // decides which rare flower to give
                int rareFlowerLoot = UnityEngine.Random.Range(0, rareLoot);
                Debug.Log("Super Lucky! + " + lootTableProvider.GetComponent<LootTables>().flowerTableRare[rareFlowerLoot]);
            }
            else
            {
                // decides which normal flower to give
                int normalFlowerLoot = UnityEngine.Random.Range(0,normalLoot);
                Debug.Log("Nice! + " + lootTableProvider.GetComponent<LootTables>().flowerTableNormal[normalFlowerLoot]);
            }
        }
        AddLuckExp();
    }


    public void AddLuckExp()
    {
        if (luckyLevel != 7)
        {
            // this value needs to be somewhere else not in a function
            
            luckyExp += 1;
            if (luckyExp == levelupThreshold)
            {
                luckyExp = 0;
                luckyLevel += 1;
                Debug.Log("Lucky level up!");
            }
            levelupThreshold = luckyLevel * 4; // level 1 = 4, level 2 = 8, level 3 = 12, etc
            Debug.Log("Lucky level: " + luckyLevel.ToString() + "\nLucky experience: " + luckyExp.ToString() + "/" + levelupThreshold.ToString());
        }
        else
        {
            Debug.Log("You're as lucky as you can be!");
        }
    }
}
