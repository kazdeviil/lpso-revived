using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PageDisplay : MonoBehaviour
{

    public Page[] page;
    public GameObject[] PetClickables;
    public int selectedClickable;
    CoAPQuests coapQuests;
    [SerializeField] GameObject QuestHandler;

    [SerializeField] TMP_Text titleText;
    [SerializeField] Image backgroundImage;

    public int pagenumber = 0;

    public GameObject canvas;
    public GameObject smallbutton;
    public int buttonwidth;
    public Sprite bluebutton;
    public GameObject[] buttons;
    [SerializeField] GameObject leftbutton;
    [SerializeField] GameObject rightbutton;
    [SerializeField] GameObject leftarrow;
    [SerializeField] GameObject rightarrow;
    public GameObject tempbutton;
    public GameObject[] petScreen;

    [SerializeField] GameObject petQuestOverview;
    [SerializeField] TMPro.TextMeshProUGUI petTitle;
    [SerializeField] TMPro.TextMeshProUGUI petDesc;
    [SerializeField] TMPro.TextMeshProUGUI petQuest1;
    [SerializeField] TMPro.TextMeshProUGUI petQuest2;
    [SerializeField] TMPro.TextMeshProUGUI petQuest3;
    [SerializeField] TMPro.TextMeshProUGUI petQuest4;
    [SerializeField] GameObject quest3Display;
    [SerializeField] GameObject quest4Display;
    [SerializeField] GameObject quest1Check;
    [SerializeField] GameObject quest2Check;
    [SerializeField] GameObject quest3Check;
    [SerializeField] GameObject quest4Check;
    [SerializeField] Image petImage;

    private void Awake()
    {
        coapQuests = QuestHandler.GetComponent<CoAPQuests>();
    }
    void Start()
    {
        
            int pageamount = page.GetLength(0);
        buttons = new GameObject[pageamount];
        for (var i = 0; i < pageamount; i++)
        {
            GameObject tempbutton = Instantiate(smallbutton, new Vector3(Screen.width/2 + i * buttonwidth - (pageamount/2) * buttonwidth, 45, 0), Quaternion.identity);
            tempbutton.transform.SetParent(canvas.transform);
            buttons[i] = tempbutton;
        }

        updatepage();
    }
    void updatepage()
    {
        // changes the text and background image to correspond with each collect a pet page
        
        titleText.text = page[pagenumber].pagename;
        backgroundImage.sprite = page[pagenumber].background;
        for (int i = 0; i < page.GetLength(0); i++)
        {
            if (i == pagenumber)
            {
                petScreen[pagenumber].SetActive(true);
            }
            else
            {
                petScreen[i].SetActive(false);
            }
        }

        // places page buttons at the bottom of the screen and automatically adds buttons for new pages

        for (var i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = smallbutton.GetComponent<Image>().sprite;
            buttons[i].transform.localScale = new Vector3(3.5f, 3.5f, 0);
        }

        //  changes the current corresponding screen button to a blue icon

        buttons[pagenumber].GetComponent <Image>().sprite = bluebutton;
        buttons[pagenumber].transform.localScale += new Vector3(0.5f, 0.5f, 0);

        // disables left and right buttons when there are no more pages to that direction

        if (pagenumber == 0)
        {
            leftbutton.SetActive(false);
            leftarrow.SetActive(false);
        }
        else
        {
            leftbutton.SetActive(true);
            leftarrow.SetActive(true);
        }

        if (pagenumber == page.GetLength(0) - 1)
        {
            rightbutton.SetActive(false);
            rightarrow.SetActive(false);
        }
        else
        {
            rightbutton.SetActive(true);
            rightarrow.SetActive(true);
        }
    }

    // takes user to next page in order if possible
    public void nextpage()
    {
        pagenumber += 1;
        updatepage();
    }

    //takes user to previous page in order if possible
    public void prevpage() 
    {
        pagenumber -= 1;
        updatepage();
    }

    public void SelectClickable(int buttonID)
    {
        selectedClickable = buttonID;
        Debug.Log("Attempting menu open for " + coapQuests.ActiveQuestPet[selectedClickable]);
        petTitle.text = coapQuests.ActiveQuestPet[selectedClickable];
        petDesc.text = coapQuests.ActiveQuestPetDesc[selectedClickable];
        petQuest1.text = coapQuests.Quest1[selectedClickable];
        petQuest2.text = coapQuests.Quest2[selectedClickable];
        petQuest3.text = coapQuests.Quest3[selectedClickable];
        petQuest4.text = coapQuests.Quest4[selectedClickable];
        if (coapQuests.QuestCount[selectedClickable] < 4)
        {
            quest4Display.SetActive(false);
            if (coapQuests.QuestCount[selectedClickable] < 3)
            {
                quest3Display.SetActive(false);
            }
            else
            {
                quest3Display.SetActive(true);
            }
        }
        else
        {
            quest4Display.SetActive(true);
            quest3Display.SetActive(true);
        }
        petQuestOverview.SetActive(true);
        // petImage.sprite = PetClickables[selectedClickable].GetComponent<Sprite>(); i forgor how to change ui sprites

        if (coapQuests.Quest1Done[selectedClickable])
        {
            quest1Check.SetActive(true);
        }
        else
        {
            quest1Check.SetActive(false);
        }
        if (coapQuests.Quest2Done[selectedClickable])
        {
            quest2Check.SetActive(true);
        }
        else
        {
            quest2Check.SetActive(false);
        }
        if (coapQuests.Quest3Done[selectedClickable])
        {
            quest3Check.SetActive(true);
        }
        else
        {
            quest3Check.SetActive(false);
        }
        if (coapQuests.Quest4Done[selectedClickable])
        {
            quest4Check.SetActive(true);
        }
        else
        {
            quest4Check.SetActive(false);
        }
    }
    public void ToggleQuestMenu()
    {
        petQuestOverview.SetActive(false);
    }

}
