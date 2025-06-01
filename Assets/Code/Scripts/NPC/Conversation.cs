using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation")]
public class Conversation : ScriptableObject
{
    // A holder for the order of each individual speech bubble prompt thingy
    // A conversation should always have at least one ConvoPart in convoParts

    public ConvoPart[] convoParts;
}
