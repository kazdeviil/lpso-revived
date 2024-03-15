using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempFavFoodGeneration : MonoBehaviour
{
    public int upperLimit = 20;

    public int Food1;
    public int Food2;
    public int Food3;

    public void GenerateFood()
    {
        Food1 = Random.Range(1, upperLimit);
        Food2 = Random.Range(1, upperLimit);
        Food3 = Random.Range(1, upperLimit);
        ValidateFood();
        Debug.Log("Favorite food 1: " + GameDataManager.Instance.itemList[Food1].itemName);
        Debug.Log("Favorite food 2: " + GameDataManager.Instance.itemList[Food2].itemName);
        Debug.Log("Favorite food 3: " + GameDataManager.Instance.itemList[Food3].itemName);
    }

    void ValidateFood()
    {
        if (Food2 == Food1)
        {
            Food2 = Random.Range(1, upperLimit);
            ValidateFood();
        }
        if (Food3 == Food1 || Food3 == Food2)
        {
            Food3 = Random.Range(1, upperLimit);
            ValidateFood();
        }
    }
}
