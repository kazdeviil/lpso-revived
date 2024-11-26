using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkshakeManiaLogic : MonoBehaviour
{
    public string[] Cup = new string[] { "Classic", "Curvy", "Tall" };
    public string[] Flavor = new string[] { "Strawberry", "Grape", "Orange", "Banana" };
    public string[] Topping = new string[] { "Sprinkles", "Chocolate", "Cherry", "Wafer" };
    public string[] Straw = new string[] { "Pink", "Stripe" };

    public Sprite[] classicSprites;
    public Sprite[] curvySprites;
    public Sprite[] tallSprites;
    public Sprite[] toppingSprites;
    public Sprite[] strawSprites;

    [SerializeField] private GameObject milkshake;
    [SerializeField] private GameObject shakeparent;

    private void Start()
    {
        Instantiate(milkshake, new Vector3(0,0,0), Quaternion.identity, shakeparent.transform);
    }
}
