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
    [SerializeField] GameObject[] petSlotsMember;
    int petCount;
    int petSelected;
    [SerializeField] int maxPageCount;
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
        petSelected = buttonID;
        petName.text = GameDataManager.Instance.pets[buttonID].name;
    }

    public void petMenuIncrement()
    {
        petPageCount += 1;
        if (GameDataManager.Instance.pets.Count < (petPageCount * 12) - 1)
        {
            petPageCount = 1;
        }
    }

    public void petMenuDecrement()
    {
        petPageCount -= 1;
        if (petPageCount == 0)
        {
            petPageCount = maxPageCount;
        }
    }

    public void petMenuUpdate()
    {
        petCount = GameDataManager.Instance.pets.Count;
        petName.text = GameDataManager.Instance.CurrentPet.name;
        maxPageCount = (GameDataManager.Instance.pets.Count / 12) + 1;
        // GameDataManager.Instance.pets[(petPageCount * 12) - 1];
        // GameDataManager.Instance.pets[(petPageCount * 12) - 12];













        for (int i = 0; i < petSlotsOccupied.Length; i++)
        {
            petSlotsEmpty[i].SetActive(true);
            petSlotsOccupied[i].SetActive(true);
        }
        for (int i = 0; i < petCount; i++)
        {
            petSlotsEmpty[i].SetActive(false);
        }
        for (int i = petSlotsOccupied.Length; i > petCount; i--)
        {
            petSlotsOccupied[i - 1].SetActive(false);
        }
        petCountText.text = petCount.ToString();
        Debug.Log(petCount);
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
