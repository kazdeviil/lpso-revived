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
    [SerializeField] BoxCollider2D player;
    [SerializeField] InventoryHandler inventoryHandler;

    void Awake()
    {
        slot = gameObject;
        inventoryHandler = canvas.gameObject.GetComponentInChildren<InventoryHandler>();
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
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);
        if (hit.collider == player)
        {
            GameDataManager gdm = GameDataManager.Instance;
            if (gdm.itemList[gdm.inventory[(((inventoryHandler.pageCount * 12) - 12) + slotIndex)]].ItemCategory == ItemData.itemCategory.Food)
            {
                if (GameDataManager.Instance.CurrentPet.foodLevel == 100)
                {
                    Debug.Log("Your pet is too full to eat any more!");
                }
                else
                {
                    Debug.Log("Yummy food! Your pet ate the " + gdm.itemList[gdm.inventory[(((inventoryHandler.pageCount * 12) - 12) + slotIndex)]].itemName);
                    gdm.RemoveInventory(gdm.itemList[gdm.inventory[(((inventoryHandler.pageCount * 12) - 12) + slotIndex)]].ID);
                    int food = 5;
                    if (!(gdm.CurrentPet.foodLevel <= 100 - food))
                    {
                        int overflow = food + gdm.CurrentPet.foodLevel;
                        int reverse = overflow - 100;
                        food -= reverse;
                    }
                    gdm.AddFood(5);
                    inventoryHandler.inventoryUpdate();
                    string str = "+" + food.ToString();
                    inventoryHandler.owUI.Popup(str);
                }
            }
            else if (gdm.itemList[gdm.inventory[(((inventoryHandler.pageCount * 12) - 12) + slotIndex)]].ItemCategory == ItemData.itemCategory.Furniture)
            {
                Debug.Log("Furniture!");
            }
            else
            {
                Debug.Log("Something else!");
            }
        }

        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        Destroy(slotClone);
        slotPos = transform.position;
        slot.transform.SetSiblingIndex(slotIndex);
        image.transform.localScale = new Vector3(1.0f, 1.0f, 0);
    }
}
