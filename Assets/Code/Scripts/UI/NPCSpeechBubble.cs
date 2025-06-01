using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeechBubble : MonoBehaviour
{
    // TODO:
    // Change the color of the "speaker" text, relative to who is "speaking" (purple for an NPC, orange for a minigame...)

    public TMP_Text NPCName;
    public TMP_Text text;

    public GameObject speechBubble;
    public GameObject buttonHitbox;
    public GameObject arrow;
    public GameObject x;
    public GameObject menu;
    public GameObject[] menuButtons;
    public TMP_Text acceptText;
    public TMP_Text rejectText;

    public int convoIndex = 0;
    public NPCBehavior NPC;

    private void Start()
    {
        speechBubble.SetActive(false);
    }

    public void SetValues()
    {
        NPCName.text = NPC.NPCName;
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

            for (int m = 0; m < menuButtons.Length-1; m++)
            {
                menuButtons[m].SetActive(false);
            }

            int spaceCount = 0;
            for (int i = 0; i < NPC.conversation.convoParts[convoIndex].buttons.Length; i++)
            {
                // shop catalogue
                if (NPC.conversation.convoParts[convoIndex].buttons[i] == 0)
                {
                    menuButtons[0].SetActive(true);
                }
                // sell interface
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 1)
                {
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
                    menuButtons[3].SetActive(true);
                }
                // minigame
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 4)
                {
                    menuButtons[4].SetActive(true);
                }
                // gem diving
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 5)
                {
                    menuButtons[5].SetActive(true);
                }
                // how to play
                else if (NPC.conversation.convoParts[convoIndex].buttons[i] == 6)
                {
                    menuButtons[6].SetActive(true);
                }
                spaceCount++;
            }
            for (int h = 0; h < spaceCount; h++)
            {
                text.text += "\n<alpha=#00>.";
            }
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
        NPC = npc;
        SetValues();
        speechBubble.SetActive(true);
    }

    public void ContinueConvo()
    {
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
