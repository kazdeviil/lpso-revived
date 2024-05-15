using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunchItem : MonoBehaviour
{
    public int ItemNumber;

    public Sprite boxblue;
    public Sprite boxgreen;
    public Sprite boxred;
    public Sprite boxdefault;
    public bool selected = false;

    public MatchnmunchLogic logic;

    SpriteRenderer boxsprite;

    private void Start()
    {
        boxsprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnMouseEnter()
    {
        if (!selected)
        {
            if (logic.winningFood == 0)
            {
                boxsprite.sprite = boxblue;
            }
            else
            {
                if (ItemNumber == logic.winningFood)
                {
                    boxsprite.sprite = boxblue;
                }
                else
                {
                    boxsprite.sprite = boxred;
                }
            }
        }
    }

    public void OnMouseExit()
    {
        if (!selected)
        {
            boxsprite.sprite = boxdefault;
        }
    }

    public void OnMouseDown()
    {
        if (ItemNumber == logic.winningFood)
        {
            selected = true;
            boxsprite.sprite = boxgreen;
        }
    }
}
