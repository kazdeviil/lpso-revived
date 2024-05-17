using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FashionationItem : MonoBehaviour
{
    public int ItemNumber;
    public Sprite ItemImage;
    public FashionationLogic fashionation;

    private void Start()
    {
        fashionation  = GameObject.Find("MinigameHandler").GetComponent<FashionationLogic>();
    }

    public void SetStats(int itemNum)
    {
        ItemNumber = itemNum;
        GetComponent<Button>().image.sprite = ItemImage;
    }
}
