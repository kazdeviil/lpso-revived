using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class OverworldUI : MonoBehaviour
{
    public TMP_Text kibblecount;
    public TMP_Text kibblecountinv;

    public string[] scenes;
    [SerializeField] Button[] mainUIButtons;
    [SerializeField] GameObject[] mainUI;
    int currentUI;
    [Header("UI GameObjects")]
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject PDA;
    [SerializeField] GameObject userInfoMenu;
    [SerializeField] GameObject map;
    [SerializeField] GameObject scrapbook;
    [SerializeField] GameObject nothing;
    [SerializeField] GameObject HouseConfirm;
    [SerializeField] GameObject messagebar;

    [Header("User Pet Menu")]
    [SerializeField] GameObject[] petSlotsOccupied;
    [SerializeField] Button[] petSlotButtons;
    [SerializeField] GameObject[] petSlotsEmpty;
    [SerializeField] Image[] petHeadPreview;
    [SerializeField] GameObject[] petSlotsMember;
    [SerializeField] Image petPreview;
    [SerializeField] GameObject petMemberRibbon;
    int petCount;
    int finalPetScreenCount;
    int petSelected = 0;
    [SerializeField] int maxPageCount = 1;
    int petPageCount = 1;
    [SerializeField] TMPro.TextMeshProUGUI petCountText;
    [SerializeField] TMPro.TextMeshProUGUI petName;

    [Header("Pink Pet Case")]
    [SerializeField] GameObject SideInv;
    [SerializeField] Button SideInvButton;

    [Header("Map")]
    [SerializeField] GameObject travelConfirmPopup;
    int SelectedLocation;
    [SerializeField] string[] mapLocations;
    [SerializeField] Button[] mapIcons;
    [SerializeField] TMPro.TextMeshProUGUI LocationName;

    [Header("Scrapbook")]
    [SerializeField] TMPro.TextMeshProUGUI scrapTitle;
    [SerializeField] TMPro.TextMeshProUGUI scrapSubtitle;
    Color scrapMainColor = new Color32(231,62,101,255);
    [SerializeField] GameObject scrapPageIcon;
    [SerializeField] Image scrapPageIconImage;
    [SerializeField] Sprite[] scrapPageIcons;
    [SerializeField] GameObject scrapTabMask;
    [SerializeField] GameObject scrapTabParent;
    [SerializeField] GameObject[] scrapTabs;
    [SerializeField] Button[] scrapTabButtons;
    [SerializeField] TMPro.TextMeshProUGUI scrapPageNumLText;
    [SerializeField] TMPro.TextMeshProUGUI scrapPageNumRText;
    [SerializeField] string[] scrapTitleText = new string[] {"   My Pet Collection", "   My World", "   My Creations", "   My Skills"};
    [SerializeField] string[] scrapSubtitleText = new string[] {"","Waggington","",""};
    [SerializeField] Color[] scrapTextColor = new Color[] { new Color32(28,136,0,255), new Color32(14,55,190,255), new Color32(190,48,77,255), new Color32(109,65,145,255)};
    int scrapTabNum;
    [SerializeField] GameObject scrapBackButton;
    [SerializeField] Button scrapBack;
    [SerializeField] GameObject scrapMainPage;
    [SerializeField] GameObject[] scrapSubPages;
    [SerializeField] GameObject[] scrapPages;
    [SerializeField] Button scrapXButton;

    public GameObject player;

    // temporary testing list
    public string[] petlist = new string[]
    {
        "Mew",
        "DJ",
        "Princess",
        "Juniper",
        "Holly",
        "Superstar",
        "cheeseburger",
        "Jackie",
        "Willow",
        "Mienfoo",
        "Arlecchino",
        "Miku",
        "Len",
        "Rin",
        "GUMI"
    }; 
    public Sprite[] petSprites;
    public bool[] petMemberStatus;

    public class MapIcon
    { public string AreaName; }

    // Start is called before the first frame update
    void Awake()
    {
        userInfoMenu.SetActive(false);
        inventory.SetActive(false);
        PDA.SetActive(false);
        messagebar.SetActive(true);
        map.SetActive(false);
        scrapbook.SetActive(false);
        nothing.SetActive(false);
        SideInvButton.interactable = false;
    }

    void Update() {
        kibblecount.SetText(GameDataManager.Instance.kibble.ToString());
        kibblecountinv.SetText(GameDataManager.Instance.kibble.ToString());
    }

    public void UIToggles(int buttonID)
    {
        currentUI = buttonID;
        mainUI[buttonID].SetActive(!mainUI[buttonID].activeSelf);
        if (buttonID == 0)
        {
            HouseConfirm.SetActive(false);
            petPageCount = 1;
            // TODO: make default pet whatever pet youre playing as
            petMenuUpdate();
        }
        else if (buttonID == 3)
        {
            travelConfirmPopup.SetActive(false);
        }
        if (buttonID != 5)
        {
            inventory.SetActive(false);
        }
    }
    // TODO: add side inventory logic

    public void petSlotInfoUpdate(int buttonID)
    {
        petSelected = (petPageCount * 12) - buttonID;
        petName.text = petlist[petSelected];
        petPreview.sprite = petSprites[petSelected];
        petMemberRibbon.SetActive(petMemberStatus[(petPageCount * 12) - buttonID]);
    }
    public void petMenuIncrement()
    {
        petPageCount += 1;
        if (petPageCount > maxPageCount)
        {
            petPageCount = 1;
        }
        petMenuUpdate();
    }
    public void petMenuDecrement()
    {
        petPageCount -= 1;
        if (petPageCount <= 0)
        {
            petPageCount = maxPageCount;
        }
        petMenuUpdate();
    }
    void petMenuUpdate()
    {
        petCount = petlist.Length;
        petName.text = petlist[petSelected];
        if (petCount % 12 == 0)
        {
            maxPageCount = petCount / 12;
        }
        else
        {
            maxPageCount = (petCount / 12) + 1;
        }

        // sets all slots to a neutral state
        for (int i = 0; i < petSlotsOccupied.Length; i++)
        {
            petSlotsEmpty[i].SetActive(true);
            petSlotsOccupied[i].SetActive(true);
            petSlotsMember[i].SetActive(true);
        }

        finalPetScreenCount = petCount - ((petCount / 12) * 12);
        if (petPageCount == maxPageCount)
        {
            if (finalPetScreenCount != 0)
            {
                // deactivates empty slot visual when not empty
                for (int i = 0; i < finalPetScreenCount; i++)
                {
                    petSlotsEmpty[i].SetActive(false);
                }
                // deactivates occupied slot visual when not occupied
                for (int i = petSlotsOccupied.Length; i > finalPetScreenCount; i--)
                {
                    petSlotsOccupied[i - 1].SetActive(false);
                }
            }
            else
            {
                Debug.Log("Weird");
            }
        }
        else
        {
            for (int i = 0; i < petSlotsEmpty.Length; i++)
            {
                petSlotsEmpty[i].SetActive(false);
            }
        }

        // sets head and membership icons
        if (petPageCount != maxPageCount)
        {
            for (int i = 0; i < petSlotsOccupied.Length; i++)
            {
                if (!petMemberStatus[((petPageCount * 12) - 12) + i])
                {
                    petSlotsMember[i].SetActive(false);
                }
                petHeadPreview[i].sprite = petSprites[((petPageCount * 12) - 12) + i];
            }
        }
        else
        {
            for (int i = 0; i < finalPetScreenCount; i++)
            {
                if (!petMemberStatus[((petPageCount * 12) - 12) + i])
                {
                    petSlotsMember[i].SetActive(false);
                }
                petHeadPreview[i].sprite = petSprites[((petPageCount * 12) - 12) + i];
            }
        }

        petCountText.text = petCount.ToString();
    }

    public void openScrapbook()
    {
        if (userInfoMenu.activeSelf)
        {
            userInfoMenu.SetActive(false);
        }
        else if (inventory.activeSelf)
        {
            inventory.SetActive(false);
        }
        scrapTitle.text = "My Scrapbook";
        scrapTitle.color = scrapMainColor;
        scrapSubtitle.text = "Table of Contents";
        scrapSubtitle.color = new Color32(68,168,236,255);
        scrapPageNumLText.color = scrapMainColor;
        scrapPageNumRText.color = scrapMainColor;
        scrapbook.SetActive(true);
        scrapMainPage.SetActive(true);
        scrapBackButton.SetActive(false);
        scrapBack.onClick.RemoveAllListeners();
        scrapBack.onClick.AddListener(openScrapbook);
        scrapPageIcon.SetActive(false);
        for (int i = 0; i < scrapPages.Length; i++)
        {
            scrapPages[i].SetActive(false);
            scrapSubPages[i].SetActive(false);
            scrapTabs[i].transform.SetParent(scrapTabMask.transform);
        }
    }
    public void scrapPageSwitch(int buttonID)
    {
        scrapTabNum = buttonID;
        scrapTitle.text = scrapTitleText[buttonID];
        scrapSubtitle.text = scrapSubtitleText[buttonID];
        scrapTitle.color = scrapTextColor[buttonID];
        scrapSubtitle.color = scrapTextColor[buttonID];
        scrapPageNumLText.color = scrapTextColor[buttonID];
        scrapPageNumRText.color = scrapTextColor[buttonID];
        for (int i = 0; i < scrapTabs.Length;i++)
        {
            scrapTabs[i].transform.SetParent(scrapTabMask.transform);
            scrapPages[i].SetActive(false);
            scrapSubPages[i].SetActive(false);
        }
        scrapTabs[buttonID].transform.SetParent(scrapTabParent.transform);
        scrapMainPage.SetActive(false);
        scrapBackButton.SetActive(true);
        scrapPageIcon.SetActive(true);
        scrapPages[buttonID].SetActive(true);
        scrapPageIconImage.sprite = scrapPageIcons[buttonID];
        scrapBack.onClick.RemoveAllListeners();
        scrapBack.onClick.AddListener(openScrapbook);
    }
    void ScrapbookBackSet()
    {
        scrapBack.onClick.RemoveAllListeners();
        scrapBack.onClick.AddListener(() => scrapPageSwitch(scrapTabNum));
    }
    public void scrapbookSubPage()
    {
        scrapPages[scrapTabNum].SetActive(false);
        scrapSubPages[scrapTabNum].SetActive(true);
        ScrapbookBackSet();
    }

    public void TravelTo(int buttonID)
    {
        travelConfirmPopup.SetActive(true);
        LocationName.text = mapLocations[buttonID] + "?";
        SelectedLocation = buttonID;
    }
    public void TravelConfirm()
    {
        GameDataManager.Instance.OldLocation = player.transform.position;
        SceneManager.LoadScene(scenes[SelectedLocation].ToString(), LoadSceneMode.Single);
    }

}
