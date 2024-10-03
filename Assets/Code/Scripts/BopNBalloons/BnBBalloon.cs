using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BnBBalloon : MonoBehaviour, IPointerClickHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public BopNBalloonsLogic logic;
    public BoxCollider2D boxCollider;
    public Animator animator;
    public int balloonType;
    public int balloonID;

    private void Start()
    {
        logic = GameObject.Find("GameHandler").GetComponent<BopNBalloonsLogic>();
        balloonType = Random.Range(0, logic.balloons.Length);
        GetComponent<SpriteRenderer>().sprite = logic.balloons[balloonType];
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
        if (hit.collider == boxCollider)
        {
            Debug.Log("Hit balloon " + balloonID);
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
    //    if (hit.collider == boxCollider)
    //    {
            
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{

    //}
}
