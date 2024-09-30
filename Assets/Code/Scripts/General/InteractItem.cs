using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class InteractItem : MonoBehaviour, IPointerClickHandler
{
    public enum Stat
    {
        Food, Fun, Comfort
    }

    public Stat stat;
    public LootTable lootTable;
    public PolygonCollider2D hitbox;
    public Vector3Int tile;
    public GameObject user;

    private void Start()
    {
        hitbox = GetComponent<PolygonCollider2D>();
        lootTable = GetComponent<LootTable>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector3.forward);
        if (hit.collider == hitbox)
        {
            Debug.Log("Hit interactable");
        }
    }
}
