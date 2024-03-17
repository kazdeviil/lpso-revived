using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectAPetUI : MonoBehaviour
{
    public OverworldUI GameData;

    public TMPro.TextMeshProUGUI petsInCollection;
    public TMPro.TextMeshProUGUI currentPetName;
    public TMPro.TextMeshProUGUI currentPageTxt;
    public TMPro.TextMeshProUGUI nextPageTxt;
    public TMPro.TextMeshProUGUI nextNextPageTxt;
    public TMPro.TextMeshProUGUI prevPageTxt;
    public TMPro.TextMeshProUGUI prevPrevPageTxt;
    public Image petPreview;
    public GameObject petPreviewStand;
    public int pageCount = 1;
    public int nextPageCount = 2;
    public int nextNextPageCount = 3;
    public int prevPageCount;
    public int prevPrevPageCount;
    public int maxPageCount = 8;
    public int maxPetPages;
    public int finalPetPageCount;
    public int selectedPet;
    public int totalPets;
    public int slotCount = 6;
    public int selectedSlot = 0;

    public Sprite TopNonmember;
    public Sprite BottomNonmember;
    public Sprite BallNonmember;
    public Sprite TopMember;
    public Sprite BottomMember;
    public Sprite BallMember;

    public Sprite noPet;
    public GameObject[] petPlatforms;

    public SelectAPetPlatform platform1;
    public SelectAPetPlatform platform2;
    public SelectAPetPlatform platform3;
    public SelectAPetPlatform platform4;
    public SelectAPetPlatform platform5;
    public SelectAPetPlatform platform6;
    public GameObject[] pageButtons;

    void Awake()
    {
        totalPets = GameData.petlist.Length;
        petsInCollection.text = $"{totalPets} Pets in my\nPet Collection";
        currentPetName.text = GameData.petlist[0];
        selectedPet = 0;
        petPreview.sprite = GameData.petSprites[selectedPet];
        if (GameData.petMemberStatus[selectedPet])
        {
            petPreviewStand.transform.Find("top").gameObject.GetComponent<Image>().sprite = TopMember;
            petPreviewStand.transform.Find("top/star").gameObject.SetActive(true);
            petPreviewStand.transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomMember;
            petPreviewStand.transform.Find("balls").gameObject.GetComponent<Image>().sprite = BallMember;
        }
        else
        {
            petPreviewStand.transform.Find("top").gameObject.GetComponent<Image>().sprite = TopNonmember;
            petPreviewStand.transform.Find("top/star").gameObject.SetActive(false);
            petPreviewStand.transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomNonmember;
            petPreviewStand.transform.Find("balls").gameObject.GetComponent<Image>().sprite = BallNonmember;
        }
        pageCount = 1;
        nextPageCount = pageCount + 1;
        nextNextPageCount = nextPageCount + 1;
        
        // neutralizes clicked stand, sets pet sprite per stand
        for (int i = 1; i < petPlatforms.Length; i++)
        {
            petPlatforms[i].transform.Find("gold").gameObject.SetActive(false);
            petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = GameData.petSprites[i];
        }
        petPlatforms[0].transform.Find("gold").gameObject.SetActive(true);
        // sets max pet pages, max page count, and final pet page count
        if (totalPets % slotCount == 0)
        {
            maxPetPages = totalPets / slotCount;
        }
        else
        {
            maxPetPages = (totalPets / slotCount) + 1;
        }
        if (maxPetPages > maxPageCount)
        {
            maxPageCount = maxPetPages;
        }
        prevPageCount = maxPageCount;
        prevPrevPageCount = prevPageCount - 1;
        finalPetPageCount = totalPets - ((totalPets / slotCount) * slotCount);
        UpdatePets();
    }
    public void SelectPet(int buttonID)
    {
        selectedSlot = buttonID;
        // activates gold ring on selected stand IF selected stand is a player pet, deactivates rest
        if ((pageCount * slotCount) - (slotCount - buttonID) < GameData.petlist.Length)
        {
            selectedPet = (pageCount * slotCount) - (slotCount - buttonID);
            currentPetName.text = GameData.petlist[selectedPet];
            petPreview.sprite = GameData.petSprites[selectedPet];
            if (GameData.petMemberStatus[selectedPet])
            {
                petPreviewStand.transform.Find("top").gameObject.GetComponent<Image>().sprite = TopMember;
                petPreviewStand.transform.Find("top/star").gameObject.SetActive(true);
                petPreviewStand.transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomMember;
                petPreviewStand.transform.Find("balls").gameObject.GetComponent<Image>().sprite = BallMember;
            }
            else
            {
                petPreviewStand.transform.Find("top").gameObject.GetComponent<Image>().sprite = TopNonmember;
                petPreviewStand.transform.Find("top/star").gameObject.SetActive(false);
                petPreviewStand.transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomNonmember;
                petPreviewStand.transform.Find("balls").gameObject.GetComponent<Image>().sprite = BallNonmember;
            }
            for (int i = 0; i < petPlatforms.Length; i++)
            {
                petPlatforms[i].transform.Find("gold").gameObject.SetActive(false);
            }
            petPlatforms[buttonID].transform.Find("gold").gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("Pet not in player pet list");
        }
    }
    void UpdatePets()
    {
        // sets pets if final screen with pets on it
        if (pageCount >= maxPetPages)
        {
            // if final pet page
            if (pageCount == maxPetPages)
            {
                if (finalPetPageCount != 0)
                {
                    // puts other sprite
                    for (int i = 0; i < petPlatforms.Length; i++)
                    {
                        petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = noPet;
                        SetNonmember(i);
                    }
                    // puts pets that are player's
                    for (int i = 0; i < finalPetPageCount; i++)
                    {
                        petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = GameData.petSprites[((pageCount * slotCount) - slotCount) + i];
                        if (GameData.petMemberStatus[((pageCount * slotCount) - slotCount) + i])
                        {
                            SetMember(i);
                        }
                        else
                        {
                            SetNonmember(i);
                        }
                    }
                }
                else
                {
                    // puts other sprite
                    for (int i = 0; i < petPlatforms.Length; i++)
                    {
                        petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = noPet;
                        SetNonmember(i);
                    }
                }
            }
            // if past pet page
            else
            {
                // puts other sprite
                for (int i = 0; i < petPlatforms.Length; i++)
                {
                    petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = noPet;
                    SetNonmember(i);
                }
            }
        }
        // sets all pet stands on screen
        else
        {
            // puts pets that are player's
            for (int i = 0; i < petPlatforms.Length; i++)
            {
                petPlatforms[i].transform.Find("pet").gameObject.GetComponent<Image>().sprite = GameData.petSprites[((pageCount * slotCount) - slotCount) + i];
                if (GameData.petMemberStatus[((pageCount * slotCount) - slotCount) + i])
                {
                    SetMember(i);
                }
                else
                {
                    SetNonmember(i);
                }
            }
        }
        platform1.GetSprites(); platform2.GetSprites(); platform3.GetSprites(); platform4.GetSprites(); platform5.GetSprites(); platform6.GetSprites();
    }

    public void ArrowLeft()
    {
        pageCount -= 1;
        if (pageCount <= 0)
        {
            pageCount = maxPageCount;
        }
        UpdateNumbers();
        UpdatePets();
        CheckSelected();
    }
    public void ArrowRight()
    {
        pageCount += 1;
        if (pageCount > maxPageCount)
        {
            pageCount = 1;
        }
        UpdateNumbers();
        UpdatePets();
        CheckSelected();
    }
    void UpdateNumbers()
    {
        currentPageTxt.text = pageCount.ToString();
        if (pageCount == maxPageCount)
        {
            nextPageCount = 1;
            nextNextPageCount = 2;
        }
        else if (pageCount == maxPageCount - 1)
        {
            nextPageCount = maxPageCount;
            nextNextPageCount = 1;
        }
        else
        {
            nextPageCount = pageCount + 1;
            nextNextPageCount = pageCount + 2;
        }
        if (pageCount == 1)
        {
            prevPageCount = maxPageCount;
            prevPrevPageCount = prevPageCount - 1;
        }
        else if (pageCount == 2)
        {
            prevPageCount = 1;
            prevPrevPageCount = maxPageCount;
        }
        else
        {
            prevPageCount = pageCount - 1;
            prevPrevPageCount = pageCount - 2;
        }
        nextPageTxt.text = nextPageCount.ToString();
        nextNextPageTxt.text = nextNextPageCount.ToString();
        prevPageTxt.text = prevPageCount.ToString();
        prevPrevPageTxt.text = prevPrevPageCount.ToString();
    }
    void CheckSelected()
    {
        // if traveled to page with selected pet, retain selection visual
        for (int i = 0; i < petPlatforms.Length; i++)
        {
            if ((pageCount * slotCount) - (slotCount - i) == selectedPet)
            {
                petPlatforms[i].transform.Find("gold").gameObject.SetActive(true);
            }
            else
            {
                petPlatforms[i].transform.Find("gold").gameObject.SetActive(false);
            }
        }
    }
    void SetNonmember(int i)
    {
        petPlatforms[i].transform.Find("top").gameObject.GetComponent<Image>().sprite = TopNonmember;
        petPlatforms[i].transform.Find("top/star").gameObject.SetActive(false);
        petPlatforms[i].transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomNonmember;
    }
    void SetMember(int i)
    {
        petPlatforms[i].transform.Find("top").gameObject.GetComponent<Image>().sprite = TopMember;
        petPlatforms[i].transform.Find("top/star").gameObject.SetActive(true);
        petPlatforms[i].transform.Find("bottom").gameObject.GetComponent<Image>().sprite = BottomMember;
    }
    public void JumpPage(int i)
    {
        if (i == 0)
        {
            pageCount = prevPrevPageCount;
        }
        else if (i == 1)
        {
            pageCount = prevPageCount;
        }
        else if (i == 2)
        {
            pageCount = nextPageCount;
        }
        else if (i == 3)
        {
            pageCount = nextNextPageCount;
        }
        else if (i == 4)
        {
            pageCount = 1;
        }
        else
        {
            pageCount = maxPageCount;
        }
        if ((pageCount * slotCount) - (slotCount - selectedSlot) == selectedPet)
        {
            petPlatforms[selectedSlot].transform.Find("gold").gameObject.SetActive(true);
        } 
        else
        {
            petPlatforms[selectedSlot].transform.Find("gold").gameObject.SetActive(false);
        }
        UpdatePets();
        UpdateNumbers();
    }
}
