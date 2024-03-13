using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PetMenu : MonoBehaviour
{
    public OverworldUI ui;

    [SerializeField] GameObject[] slotsOccupied;
    [SerializeField] Button[] slotButtons;
    [SerializeField] GameObject[] slotsEmpty;
    [SerializeField] Image[] headPreview;
    [SerializeField] GameObject[] slotsMemberTag;
    [SerializeField] Image petPreview;
    [SerializeField] GameObject previewRibbon;
    int petSelected = 0;
    [HideInInspector] public int pageCount = 1;
    [SerializeField] TMPro.TextMeshProUGUI petCountText;
    [SerializeField] TMPro.TextMeshProUGUI petName;
    [SerializeField] GameObject[] pageButtons;
    [SerializeField] GameObject pageButtonLeft;
    [SerializeField] GameObject pageButtonRight;
    [SerializeField] Sprite pageButtonSelected;

    public void petSlotInfoUpdate(int buttonID)
    {
        petSelected = (pageCount * 12) - buttonID;
        petName.text = ui.petlist[petSelected];
        petPreview.sprite = ui.petSprites[petSelected];
        ui.petBarIcon.sprite = ui.petSprites[petSelected];
        previewRibbon.SetActive(ui.petMemberStatus[(pageCount * 12) - buttonID]);
    }
    public void petMenuIncrement()
    {
        pageCount += 1;
        if (pageCount > ui.maxPageCount)
        {
            pageCount = 1;
        }
        petMenuUpdate();
    }
    public void petMenuDecrement()
    {
        pageCount -= 1;
        if (pageCount <= 0)
        {
            pageCount = ui.maxPageCount;
        }
        petMenuUpdate();
    }
    public void petMenuJump(int buttonID)
    {
        pageCount = buttonID + 1;
        petMenuUpdate();
    }
    public void petMenuUpdate()
    {
        ui.petCount = ui.petlist.Length;
        petName.text = ui.petlist[petSelected];
        // sets max page count dynamically
        if (ui.petCount % 12 == 0)
        {
            ui.maxPageCount = ui.petCount / 12;
        }
        else
        {
            ui.maxPageCount = (ui.petCount / 12) + 1;
        }

        // vvv buttons for jumping to pages vvv
        // changes currently selected page button sprite
        if (ui.maxPageCount > 1)
        {
            for (int i = 0; i < pageButtons.Length; i++)
            {
                pageButtons[i].GetComponent<Image>().sprite = pageButtonLeft.GetComponent<Image>().sprite;
            }
        }
        pageButtons[pageCount - 1].GetComponent<Image>().sprite = pageButtonSelected;
        // sets page button amount
        if (ui.maxPageCount != 1)
        {
            pageButtonLeft.SetActive(true);
            pageButtonRight.SetActive(true);
            for (int i = 0; i < pageButtons.Length; i++)
            {
                pageButtons[i].SetActive(false);
            }
            for (int i = 1; i <= ui.maxPageCount; i++)
            {
                pageButtons[i - 1].SetActive(true);
            }
        }
        else
        {
            pageButtonLeft.SetActive(false);
            pageButtonRight.SetActive(false);
            for (int i = 1; i < pageButtons.Length; i++)
            {
                pageButtons[i].SetActive(false);
            }
        }

        // sets all slots to a neutral state
        for (int i = 0; i < slotsOccupied.Length; i++)
        {
            slotsEmpty[i].SetActive(true);
            slotsOccupied[i].SetActive(true);
            slotsMemberTag[i].SetActive(true);
        }
        // gets amount of pets on final page
        ui.finalPetScreenCount = ui.petCount - ((ui.petCount / 12) * 12);
        if (pageCount == ui.maxPageCount)
        {
            if (ui.finalPetScreenCount != 0)
            {
                // deactivates empty slot visual when not empty
                for (int i = 0; i < ui.finalPetScreenCount; i++)
                {
                    slotsEmpty[i].SetActive(false);
                }
                // deactivates occupied slot visual when not occupied
                for (int i = slotsOccupied.Length; i > ui.finalPetScreenCount; i--)
                {
                    slotsOccupied[i - 1].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < slotsEmpty.Length; i++)
            {
                slotsEmpty[i].SetActive(false);
            }
        }

        // sets head and membership icons
        if (pageCount != ui.maxPageCount)
        {
            for (int i = 0; i < slotsOccupied.Length; i++)
            {
                if (!ui.petMemberStatus[((pageCount * 12) - 12) + i])
                {
                    slotsMemberTag[i].SetActive(false);
                }
                headPreview[i].sprite = ui.petSprites[((pageCount * 12) - 12) + i];
            }
        }
        else
        {
            if (ui.finalPetScreenCount != 0)
            {
                for (int i = 0; i < ui.finalPetScreenCount; i++)
                {
                    if (!ui.petMemberStatus[((pageCount * 12) - 12) + i])
                    {
                        slotsMemberTag[i].SetActive(false);
                    }
                    headPreview[i].sprite = ui.petSprites[((pageCount * 12) - 12) + i];
                }
            }
            else
            {
                for (int i = 0; i < slotsEmpty.Length; i++)
                {
                    if (!ui.petMemberStatus[i])
                    {
                        slotsMemberTag[i].SetActive(false);
                    }
                    headPreview[i].sprite = ui.petSprites[i];
                }
            }
        }

        petCountText.text = ui.petCount.ToString();
    }
}
