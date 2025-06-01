using UnityEngine;

[CreateAssetMenu(fileName = "ConvoPart", menuName = "ScriptableObjects/ConvoPart")]
public class ConvoPart : ScriptableObject
{
    // Holds the string and icon information for each individual speech bubble popup in a Conversation
    // Each ConvoPart should be part of a Conversation

    public string text;
    public Icon icon;

    // vv dumb use cases for menu options, other icon-type convoparts really would not care about this at all vv
    public string accept = "Ok!";   // default option for a green continue-button
    public string reject = "No";    // default option for a red continue-button (or exiting from a conversation early)

    // 0 = shop catalogue/"Buy"
    public Catalogue catalogue;
    public string shopName;
    // 1 = cash register/"Sell"
    // 2 = continue
    // 3 = cafe menu style catalogue
    // 4 = minigame teleport
    // 5 = gem diving start
    // 6 = how to play (gem diving)
    // 7 = red button (continue)
    public int[] buttons;

    public enum Icon
    {
        Arrow,
        X,
        Menu
    }
}