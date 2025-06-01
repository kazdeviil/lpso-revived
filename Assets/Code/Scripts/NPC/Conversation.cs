using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/Conversation")]
public class Conversation : ScriptableObject
{
    public ConvoPart[] convoParts;
}
