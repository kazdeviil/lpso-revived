using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CatalogItems : MonoBehaviour
{
    public TMP_Text MyKibble;
    public GameObject ItemIcon;
    public TMP_Text ItemCost;
    public GameObject BuyTag;
    public ItemData CurrentItem;
    public GameObject[] ItemDisplay;
    public ItemData[] StoreInventory;
    public GameObject arrowRight;
    public GameObject arrowLeft;
    public int pageCount = 1;
    public int maxPageCount = 1;
    public int finalPageItemCount;
    public TMPro.TextMeshProUGUI pageNumberLeftText;
    public int pageNumberLeft;
    public TMPro.TextMeshProUGUI pageNumberRightText;
    public int pageNumberRight;
    public Sprite itemBgMember;
    public Sprite itemBgNonmember;

    void Start()
    {
        pageCount = 1;
        UpdatePages();
    }

    void UpdatePages()
    {
        // sets max page count
        if (StoreInventory.Length % 10 == 0)
        {
            maxPageCount = StoreInventory.Length / 10;
        }
        else
        {
            maxPageCount = (StoreInventory.Length / 10) + 1;
        }
        UpdateArrows();
        // neutralizes slot state
        for (int i = 0; i < ItemDisplay.Length; i++)
        {
            ItemDisplay[i].SetActive(true);
            ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(false);
        }
        // gets final page item count
        finalPageItemCount = StoreInventory.Length - ((StoreInventory.Length / 10) * 10);
        // deactivates unoccupied slots on final page
        if (pageCount == maxPageCount)
        {
            if (finalPageItemCount != 0)
            {
                for (int i = ItemDisplay.Length; i > finalPageItemCount; i--)
                {
                    ItemDisplay[i - 1].SetActive(false);
                }
            }
        }
        // sets item visuals per displayed button
        if (pageCount != maxPageCount)
        {
            for (int i = 0; i < ItemDisplay.Length; i++)
            {
                if (StoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                {
                    ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                    ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                }
                ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = StoreInventory[((pageCount * 10) - 10) + i].icon;
                ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{StoreInventory[((pageCount * 10) - 10) + i].price:n0}";
            }
        }
        else
        {
            if (finalPageItemCount != 0)
            {
                for (int i = 0; i < finalPageItemCount; i++)
                {
                    if (StoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                    }
                    ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = StoreInventory[((pageCount * 10) - 10) + i].icon;
                    ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{StoreInventory[((pageCount * 10) - 10) + i].price:n0}";
                }
            }
            else
            {
                for (int i = 0; i < ItemDisplay.Length; i++)
                {
                    if (StoreInventory[((pageCount * 10) - 10) + i].membersOnly)
                    {
                        ItemDisplay[i].transform.Find("item hitbox/member tag").gameObject.SetActive(true);
                        ItemDisplay[i].transform.Find("item background").gameObject.GetComponent<Image>().sprite = itemBgMember;
                    }
                    ItemDisplay[i].transform.Find("item hitbox/item image").gameObject.GetComponent<Image>().sprite = StoreInventory[((pageCount * 10) - 10) + i].icon;
                    ItemDisplay[i].transform.Find("item hitbox/price box/price").gameObject.GetComponent<TextMeshProUGUI>().text = $"{StoreInventory[((pageCount * 10) - 10) + i].price:n0}";
                }
            }
        }
    }

    public void SetTag(int buttonID)
    {
        CurrentItem = StoreInventory[((pageCount * 10) - 10) + buttonID];
        MyKibble.SetText($"{GameDataManager.Instance.kibble:n0}");
        ItemIcon.GetComponent<Image>().sprite = CurrentItem.icon;
        ItemCost.SetText($"{CurrentItem.price:n0}");
        BuyTag.SetActive(true);
    }

    public void BuyItem()
    {
        if (GameDataManager.Instance.kibble >= CurrentItem.price)
        {
            Debug.Log(CurrentItem.ID);
            GameDataManager.Instance.inventory.Add((int)CurrentItem.ID);
            GameDataManager.Instance.kibble -= CurrentItem.price;
            MyKibble.SetText($"{GameDataManager.Instance.kibble:n0}");
            BuyTag.SetActive(false);
        }
    }
    public void CloseTag()
    {
        BuyTag.SetActive(false);
    }
    void UpdateArrows()
    {
        // sets page number text
        pageNumberRight = pageCount * 2;
        pageNumberLeft = pageNumberRight - 1;
        pageNumberRightText.text = pageNumberRight.ToString();
        pageNumberLeftText.text = pageNumberLeft.ToString();
        // sets arrow active state
        if (maxPageCount == 1)
        {
            arrowRight.SetActive(false);
            arrowLeft.SetActive(false);
        }
        else
        {
            if (pageCount == maxPageCount)
            {
                arrowRight.SetActive(false);
                arrowLeft.SetActive(true);
            }
            else if (pageCount == 1)
            {
                arrowRight.SetActive(true);
                arrowLeft.SetActive(false);
            }
            else
            {
                arrowRight.SetActive(true);
                arrowLeft.SetActive(true);
            }
        }
    }
    public void PageRight()
    {
        pageCount += 1;
        UpdatePages();
    }
    public void PageLeft()
    {
        pageCount -= 1;
        UpdatePages();
    }
}
