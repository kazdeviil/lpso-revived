using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 mousePos;
    public Vector2 WSPlayerPos = new Vector2(0f,0f);
    public float worldXPos;
    Vector2 MoveTo;
    public float moveSpeed;

    private void Start()
    {
        moveSpeed = 0.5f;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        worldXPos = mousePos.x;
        MoveTo = new Vector2 (worldXPos, 1.9f);
        transform.position.Set(worldXPos,1.9f,0);
        WSPlayerPos = Vector2.Lerp(transform.position, MoveTo, moveSpeed);

        if (worldXPos < WSPlayerPos.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
