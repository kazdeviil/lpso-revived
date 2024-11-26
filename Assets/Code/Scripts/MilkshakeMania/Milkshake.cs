using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milkshake : MonoBehaviour
{
    [SerializeField] private MilkshakeManiaLogic mmlogic;
    public int cup;
    public int flavor;
    public int topping;
    public int straw;

    [SerializeField] private Sprite Cup;
    [SerializeField] private Sprite Topping;
    [SerializeField] private Sprite Straw;

    private void Start()
    {
        mmlogic = GameObject.Find("MinigameHandler").GetComponent<MilkshakeManiaLogic>();
        int randomCup = Random.Range(0, mmlogic.Cup.Length);
        cup = randomCup;
        int randomFlavor = Random.Range(0, mmlogic.Flavor.Length);
        flavor = randomFlavor;
        int randomTop = Random.Range(0, mmlogic.Topping.Length);
        topping = randomTop;
        int randomStraw = Random.Range(0, mmlogic.Straw.Length);
        straw = randomStraw;

        //for (int i = 0; i < mmlogic.Cup.Length; i++)
        //{
        //    if (cup == i)
        //    {

        //    }
        //}
        if (cup == 0)
        {
            Cup = mmlogic.classicSprites[flavor];
        }
        else if (cup == 1)
        {
            Cup = mmlogic.curvySprites[flavor];
        }
        else
        {
            Cup = mmlogic.tallSprites[flavor];
        }

        Topping = mmlogic.toppingSprites[topping];
        Straw = mmlogic.strawSprites[straw];

        GetComponent<SpriteRenderer>().sprite = Cup;
        transform.Find("topping").GetComponent<SpriteRenderer>().sprite = Topping;
        transform.Find("straw").GetComponent<SpriteRenderer>().sprite = Straw;
        Debug.Log($"Milkshake: {cup} {flavor} {topping} {straw}");
    }
}
