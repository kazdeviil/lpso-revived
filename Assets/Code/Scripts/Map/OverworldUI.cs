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
    [Header("UI GameObjects")]
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject PDA;
    [SerializeField] GameObject userInfoMenu;
    [SerializeField] GameObject map;
    [SerializeField] GameObject scrapbook;
    [SerializeField] GameObject nothing;

    [Header("Pet Menu Buttons")]
    [SerializeField] Button CoAPButtonUserMenu;
    [SerializeField] Button ScrapbookButtonUserMenu;
    [SerializeField] Button ClothesButton;
    [SerializeField] Button HouseButtonUserMenu;
    [SerializeField] GameObject HouseConfirm;
    [SerializeField] Button CrAPButton;

    [Header("Main UI")]
    [SerializeField] Button xButton;
    [SerializeField] Button petButton;
    [SerializeField] Button messagebutton;
    [SerializeField] GameObject messagebar;
    [SerializeField] Button collectapetbutton;

    [Header("PDA")]
    [SerializeField] Button PDAButton;
    [SerializeField] Button PowerButton;

    [Header("Pink Pet Case")]
    [SerializeField] Button invButton;
    [SerializeField] Button closeInvButton;
    [SerializeField] Button InvLeftButton;
    [SerializeField] Button InvRightButton;
    [SerializeField] GameObject SideInv;
    [SerializeField] Button SideInvButton;
    [SerializeField] GameObject SideInvArrow;

    [Header("Map")]
    [SerializeField] Button mapXButton;
    [SerializeField] Button mapOpenButton;
    [SerializeField] GameObject travelConfirmPopup;
    [SerializeField] int SelectedLocation;
    [SerializeField] string[] mapLocations;
    [SerializeField] Button[] mapIcons;
    [SerializeField] TMPro.TextMeshProUGUI LocationName;

    [Header("Scrapbook")]
    [SerializeField] TMPro.TextMeshProUGUI scrapTitle;
    [SerializeField] TMPro.TextMeshProUGUI scrapSubtitle;
    [SerializeField] Color scrapMainColor = new Color32(231,62,101,255);
    [SerializeField] Color scrapCollectionColor = new Color32(0,0,0,255);
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
    [SerializeField] int scrapTabNum;
    [SerializeField] GameObject scrapBackButton;
    [SerializeField] GameObject scrapMainPage;
    [SerializeField] GameObject[] scrapPages;
    [SerializeField] Button scrapXButton;

    public GameObject player;
    [SerializeField] Button nothingOk;

    public class MapIcon
    { public string AreaName; }

    // Start is called before the first frame update
    void Start()
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

    public void toggleInventory() => inventory.SetActive(!inventory.activeSelf);

    // this function isnt what i want but i need to remember to change it later
    public void toggleSideInv() => SideInv.SetActive(!SideInv.activeSelf);

    public void toggleUserMenu()
    { userInfoMenu.SetActive(!userInfoMenu.activeSelf);
        inventory.SetActive(false);
        HouseConfirm.SetActive(false); }

    public void toggleHouseConfirm() => HouseConfirm.SetActive(!HouseConfirm.activeSelf);

    public void toggleMessage() => messagebar.SetActive(!messagebar.activeSelf);

    public void togglePhone()
    { PDA.SetActive(!PDA.activeSelf);
        inventory.SetActive(false); }

    public void toggleMap()
    { map.SetActive(!map.activeSelf);
        travelConfirmPopup.SetActive(false);
        inventory.SetActive(false); }

    public void openScrapbook()
    {
        inventory.SetActive(false);
        scrapTitle.text = "My Scrapbook";
        scrapTitle.color = scrapMainColor;
        scrapSubtitle.text = "Table of Contents";
        scrapSubtitle.color = new Color32(68,168,236,255);
        scrapPageNumLText.color = scrapMainColor;
        scrapPageNumRText.color = scrapMainColor;
        scrapbook.SetActive(true);
        scrapMainPage.SetActive(true);
        scrapBackButton.SetActive(false);
        scrapPageIcon.SetActive(false);
        for (int i = 0; i < scrapPages.Length; i++)
        {
            scrapPages[i].SetActive(false);
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
        }
        scrapTabs[buttonID].transform.SetParent(scrapTabParent.transform);
        scrapMainPage.SetActive(false);
        scrapBackButton.SetActive(true);
        scrapPageIcon.SetActive(true);
        scrapPages[buttonID].SetActive(true);
        scrapPageIconImage.sprite = scrapPageIcons[buttonID];
    }
    public void closeScrapbook() => scrapbook.SetActive(false);

    public void toggleNothing()
    { nothing.SetActive(!nothing.activeSelf);
    inventory.SetActive(false); }

    public void TravelTo(int buttonID)
    {
        Debug.Log("Attemtping to travel to " + mapLocations[buttonID]);
        travelConfirmPopup.SetActive(true);
        LocationName.text = mapLocations[buttonID] + "?";
        SelectedLocation = buttonID;
    }

    public void TravelCancel()
    {
        travelConfirmPopup.SetActive(false);
    }

    public void TravelConfirm()
    {
        GameDataManager.Instance.OldLocation = player.transform.position;
        SceneManager.LoadScene(scenes[SelectedLocation].ToString(), LoadSceneMode.Single);
    }

}
