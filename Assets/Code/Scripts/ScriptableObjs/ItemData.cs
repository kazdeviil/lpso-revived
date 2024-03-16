using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 1)]
public class ItemData : ScriptableObject
{
    public GameObject[] objects;

    public int ID;
    public string itemName;
    public string itemDesc;
    public Sprite icon;
    public int price;
    public string category;
    public GameObject rotations;
    public bool membersOnly;
    public itemCategory ItemCategory;
    public itemCategoryText ItemCategoryText;
    public bool restricted;

    public enum itemCategory
    {
        Furniture,
        Clothing,
        Food,
        Toy,
        Grooming,
        Miscellaneous
    }
    public enum itemCategoryText
    {
        Furniture,
        Accessories,
        Wallpaper,
        Flooring,
        Hat,
        Glasses,
        Collar,
        WristItems,
        Gloves,
        Tops,
        Bottoms,
        Shoes,
        Food,
        Drink,
        Toy,
        Grooming,
        Miscellaneous,
        Bell
    }
}
