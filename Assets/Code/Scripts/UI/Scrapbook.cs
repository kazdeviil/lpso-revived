using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Scrapbook : MonoBehaviour
{
    public OverworldUI ui;

    [SerializeField] TMPro.TextMeshProUGUI titleText;
    [SerializeField] TMPro.TextMeshProUGUI subtitleText;
    Color scrapMainColor = new Color32(231, 62, 101, 255);
    [SerializeField] GameObject pageIcon;
    [SerializeField] Image pageIconImage;
    [SerializeField] Sprite[] pageIcons;
    [SerializeField] GameObject tabMask;
    [SerializeField] GameObject tabParent;
    [SerializeField] GameObject[] tabs;
    [SerializeField] Button[] tabButtons;
    [SerializeField] int pageNumberLeft = 1;
    [SerializeField] TMPro.TextMeshProUGUI pageNumberLeftText;
    [SerializeField] int pageNumberRight = 2;
    [SerializeField] TMPro.TextMeshProUGUI pageNumberRightText;
    [SerializeField] string[] titleStrings = new string[] { "   My Pet Collection", "   My World", "   My Creations", "   My Skills" };
    [SerializeField] string[] subtitleStrings = new string[] { "", "Waggington", "", "" };
    [SerializeField] Color[] textColors = new Color[] { new Color32(28, 136, 0, 255), new Color32(14, 55, 190, 255), new Color32(190, 48, 77, 255), new Color32(109, 65, 145, 255) };
    int tabNumber;
    [SerializeField] GameObject backTab;
    [SerializeField] Button backTabButton;
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject[] subpages;
    [SerializeField] GameObject[] pages;
    [SerializeField] Button xButton;
    [Header("Scrapbook: Pet Collection")]
    [SerializeField] GameObject[] PCButtons;
    [SerializeField] TMPro.TextMeshProUGUI[] PCNames;
    [SerializeField] int currentPet;
    [SerializeField] int pageCount = 1;
    [SerializeField] GameObject pageArrowLeft;
    [SerializeField] GameObject pageArrowRight;


    public void OpenScrapbook()
    {
        // removes clashing ui
        if (ui.userInfoMenu.activeSelf)
        {
            ui.userInfoMenu.SetActive(false);
        }
        else if (ui.inventory.activeSelf)
        {
            ui.inventory.SetActive(false);
        }
        // resets ui values
        if (!ui.scrapbook.activeSelf)
        {
            ui.scrapbook.SetActive(true);
        }
        pageArrowLeft.SetActive(false);
        pageArrowRight.SetActive(false);
        pageNumberLeft = 1;
        pageNumberRight = 2;
        titleText.text = "My Scrapbook";
        titleText.color = scrapMainColor;
        subtitleText.text = "Table of Contents";
        subtitleText.color = new Color32(68, 168, 236, 255);
        pageNumberLeftText.text = pageNumberLeft.ToString();
        pageNumberLeftText.color = scrapMainColor;
        pageNumberRightText.text = pageNumberRight.ToString();
        pageNumberRightText.color = scrapMainColor;
        mainPage.SetActive(true);
        backTab.SetActive(false);
        backTabButton.onClick.RemoveAllListeners();
        backTabButton.onClick.AddListener(OpenScrapbook);
        pageIcon.SetActive(false);
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            subpages[i].SetActive(false);
            tabs[i].transform.SetParent(tabMask.transform);
        }
    }
    public void ScrapPageSwitch(int buttonID)
    {
        tabNumber = buttonID;
        pageCount = 1;
        // sets all the fancy visuals like colors and text
        titleText.text = titleStrings[buttonID];
        subtitleText.text = subtitleStrings[buttonID];
        titleText.color = textColors[buttonID];
        subtitleText.color = textColors[buttonID];
        pageNumberLeftText.color = textColors[buttonID];
        pageNumberRightText.color = textColors[buttonID];

        // resets all tabs, pages, subpages
        for (int i = 0; i < tabs.Length; i++)
        {
            tabs[i].transform.SetParent(tabMask.transform);
            pages[i].SetActive(false);
            subpages[i].SetActive(false);
        }
        // shows ui of current page, hides prior page
        tabs[buttonID].transform.SetParent(tabParent.transform);
        mainPage.SetActive(false);
        backTab.SetActive(true);
        pageIcon.SetActive(true);
        pages[buttonID].SetActive(true);
        pageIconImage.sprite = pageIcons[buttonID];
        // resets back button function
        backTabButton.onClick.RemoveAllListeners();
        backTabButton.onClick.AddListener(OpenScrapbook);
        // calls corresponding function per page
        if (buttonID == 0)
        {
            scrapbookUpdatePetCollection();
        }
    }
    void scrapbookUpdatePetCollection()
    {
        ScrapPageCountUpdate();
        pageNumberLeftText.text = pageNumberLeft.ToString();
        pageNumberRightText.text = pageNumberRight.ToString();
        // updates pet count and maximum page count
        ui.petCount = ui.petlist.Length;
        if (ui.petCount % 12 == 0)
        {
            ui.maxPageCount = ui.petCount / 12;
        }
        else
        {
            ui.maxPageCount = (ui.petCount / 12) + 1;
        }

        // sets name per button
        if (pageCount != ui.maxPageCount)
        {
            for (int i = 0; i < PCButtons.Length; i++)
            {
                PCNames[i].text = ui.petlist[((pageCount * 12) - 12) + i];
            }
        }
        else
        {
            if (ui.finalPetScreenCount != 0)
            {
                for (int i = 0; i < ui.finalPetScreenCount; i++)
                {
                    PCNames[i].text = ui.petlist[((pageCount * 12) - 12) + i];
                }
            }
            else
            {
                for (int i = 0; i < PCButtons.Length; i++)
                {
                    PCNames[i].text = ui.petlist[i];
                }
            }
        }
        // sets all buttons to uniform state
        for (int i = 0; i < PCButtons.Length; i++)
        {
            PCButtons[i].SetActive(true);
        }
        // sets final page button amount
        ui.finalPetScreenCount = ui.petCount - ((ui.petCount / 12) * 12);
        if (pageCount == ui.maxPageCount)
        {
            if (ui.finalPetScreenCount != 0)
            {
                // deactivates pet button when no pet exists
                for (int i = PCButtons.Length; i > ui.finalPetScreenCount; i--)
                {
                    PCButtons[i - 1].SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < PCButtons.Length; i++)
                {
                    PCButtons[i].SetActive(true);
                }
            }
        }
    }

    public void scrapbookSubPage()
    {
        // sets correct subpage
        pages[tabNumber].SetActive(false);
        subpages[tabNumber].SetActive(true);
        // sets back button function to corresponding page
        backTabButton.onClick.RemoveAllListeners();
        backTabButton.onClick.AddListener(() => ScrapPageSwitch(tabNumber));
    }
    public void scrapbookPetPageLeft()
    {
        pageCount -= 1;
        scrapbookUpdatePetCollection();
    }
    public void scrapbookPetPageRight()
    {
        pageCount += 1;
        scrapbookUpdatePetCollection();
    }
    void ScrapPageCountUpdate()
    {
        pageNumberRight = pageCount * 2;
        pageNumberLeft = pageNumberRight - 1;
        // updates arrow visibility
        if (pageCount <= 1)
        {
            pageArrowLeft.SetActive(false);
            pageArrowRight.SetActive(true);
        }
        else if (pageCount == ui.maxPageCount)
        {
            pageArrowRight.SetActive(false);
            pageArrowLeft.SetActive(true);
        }
        else
        {
            pageArrowLeft.SetActive(true);
            pageArrowRight.SetActive(true);
        }
    }
}
