using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectAPetPlatform : MonoBehaviour
{
    public Sprite TopOriginal;
    public Sprite BottomOriginal;
    public Sprite TopSelected;
    public Sprite BottomSelected;

    public void GetSprites()
    {
        TopOriginal = gameObject.transform.parent.Find("top").GetComponent<Image>().sprite;
        BottomOriginal = gameObject.transform.parent.Find("bottom").GetComponent<Image>().sprite;
    }

    public void OnMouseEnter()
    {
        gameObject.transform.parent.Find("top").GetComponent<Image>().sprite = TopSelected;
        gameObject.transform.parent.Find("bottom").GetComponent<Image>().sprite = BottomSelected;
    }

    public void OnMouseExit()
    {
        gameObject.transform.parent.Find("top").GetComponent<Image>().sprite = TopOriginal;
        gameObject.transform.parent.Find("bottom").GetComponent<Image>().sprite = BottomOriginal;
    }
}
