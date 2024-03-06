using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryHandler : MonoBehaviour
{
    public GameObject[] Buttons;
    public GameObject[] ItemSlotBg;
    public Sprite inventorySlotOccupied;
    public TMP_Text inventoryItemStackAmount;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <Buttons.Length; i++){
            addButton(i);
        }
    }

    void addButton(int itemID){
        GameDataManager Data = GameDataManager.Instance;
        if(itemID < GameDataManager.Instance.inventory.Count){
            ItemData ItemData = Data.itemList[Data.inventory[Data.inventory.Count - itemID -1]];
            Buttons[itemID].GetComponent<Image>().sprite = ItemData.icon;
            Debug.Log("Button " + itemID + " complete.");
        }
        else
        {
            ItemSlotBg[itemID].gameObject.SetActive(false);
            Debug.Log("Button " + itemID + " empty.");
        }
    }
}

