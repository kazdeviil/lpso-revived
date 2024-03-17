using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed;
    public GameObject startPos;
    public GameObject endPos;

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPos.transform.position, speed * Time.deltaTime);
        if (transform.position == endPos.transform.position)
        {
            transform.position = startPos.transform.position;
        }
    }
}
