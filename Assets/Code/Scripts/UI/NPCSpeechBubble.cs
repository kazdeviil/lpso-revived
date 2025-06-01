using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeechBubble : MonoBehaviour
{
    // The text information (who, what)
    public TMP_Text NPCName;
    public TMP_Text text;

    // The visual aspects
    public GameObject speechBubble;
    public GameObject buttonHitbox;
    public GameObject arrow;
    public GameObject x;
    public GameObject menu;
    public GameObject[] menuButtons;
    public TMP_Text acceptText;
    public TMP_Text rejectText;

    // The NPC being spoken to, and where in the conversation you are in
    public int convoIndex = 0;
    public NPCBehavior NPC;

    // The colors the NPC name can be
    public Color32 purple = new(132, 67, 160, 255);
    public Color32 orange = new(208, 115, 13, 255);

    private void Start()
    {
        // No speech bubble by default
        speechBubble.SetActive(false);
    }

    public void SetValues()
    {
        // Sets all visuals of the speech bubble

        // Sets correct NPC name and color
        NPCName.text = NPC.NPCName;
        if (NPC.textColor == NPCBehavior.TextColor.Purple)
        {
            NPCName.color = purple;
        }
        else
        {
            NPCName.color = orange;
        }
        
        // Sets the contents of the text and corresponding icon visuals
        text.text = NPC.conversation.convoParts[convoIndex].text;
        if (NPC.conversation.convoParts[convoIndex].icon == ConvoPart.Icon.X)
        {
            arrow.SetActive(false);
            x.SetActive(true);
            menu.SetActive(false);
            buttonHitbox.GetComponent<Button>().interactable = true;
        }
        else if (NPC.conversation.convoParts[convoIndex].icon == ConvoPart.Icon.Menu)
        {
            arrow.SetActive(false);
            x.SetActive(false);
            menu.SetActive(true);
            buttonHitbox.GetComponent<Button>().interactable = false;

            // Each menu button gets deactivated before showing any buttons
            for (int m = 0; m < menuButtons.Length-1; m++)
            {
                menuButtons[m].SetActive(false);
            }

            // For every menu button that's added, an extra space count is needed to make room for the menu options in the text box
            int spaceCount = 0;
            for (int i = 0; i < NPC.conversation.convoParts[convoIndex].buttons.Length; i++)
            {
                // shop catalogue
                if (NPC.conversation.convoParts[convoIndex].buttons[i] == 0)
                {
                    // need to make NPC hold a reference to a shop catalogue
                    menuButtons[0].SetActive(true);
                }
                // sell interface
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 1)
                {
                    // need to make selling ui and reference it here
                    menuButtons[1].SetActive(true);
                }
                // continue
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 2)
                {
                    acceptText.text = NPC.conversation.convoParts[convoIndex].accept;
                    menuButtons[2].SetActive(true);
                }
                // cafe menu style catalogue
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 3)
                {
                    // need to make cafe menu ui and stick in the shop catalogue here
                    menuButtons[3].SetActive(true);
                }
                // minigame
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 4)
                {
                    // gotoscene minigame probably
                    menuButtons[4].SetActive(true);
                }
                // gem diving
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 5)
                {
                    // activate gem diving
                    menuButtons[5].SetActive(true);
                }
                // how to play
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 6)
                {
                    // need to make how to play ui and show it here, this is only for gem diving tho
                    menuButtons[6].SetActive(true);
                }
                spaceCount++;
            }
            for (int h = 0; h < spaceCount; h++)
            {
                // a very hacky way of adding blank buffer space but hey man it works
                text.text += "\n<alpha=#00>.";
            }
            // The red menu button always exists and the text on it just changes
            rejectText.text = NPC.conversation.convoParts[convoIndex].reject;
        }
        else
        {
            arrow.SetActive(true);
            x.SetActive(false);
            menu.SetActive(false);
            buttonHitbox.GetComponent<Button>().interactable = true;
        }
    }

    public void StartConvo(NPCBehavior npc)
    {
        // When the NPC is clicked, it gets the NPC's information and uses it to get the conversation
        NPC = npc;
        NPC.CheckQuests();
        SetValues();
        speechBubble.SetActive(true);
    }

    public void ContinueConvo()
    {
        // Advances the conversation to the next ConvoPart, if there is one
        convoIndex++;
        if (convoIndex > NPC.conversation.convoParts.Length - 1)
        {
            convoIndex = 0;
            NPC.UpdateConvo();
            speechBubble.SetActive(false);
        }
        else
        {
            SetValues();
        }
    }
}
