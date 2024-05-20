using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;
    [SerializeField] GameObject slot;
    [SerializeField] GameObject slotClone;
    [SerializeField] Vector3 slotPos;
    [SerializeField] int slotIndex;
    [SerializeField] GridLayoutGroup grid;
    [SerializeField] Canvas canvas;

    void Awake()
    {
        slot = gameObject;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        slotPos = transform.position;
        slotIndex = slot.transform.GetSiblingIndex();
        parentAfterDrag = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        slotClone = Instantiate(slot, slotPos, Quaternion.identity, grid.transform);
        image.transform.localScale = new Vector3(0.9f, 0.9f, 0);
        slotClone.transform.SetSiblingIndex(slotIndex);
        Debug.Log(Input.mousePosition.x.ToString() + "  = x \n" + Input.mousePosition.y.ToString() + " = y");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        Destroy(slotClone);
        slotPos = transform.position;
        slot.transform.SetSiblingIndex(slotIndex);
        image.transform.localScale = new Vector3(1.0f, 1.0f, 0);
    }
}
