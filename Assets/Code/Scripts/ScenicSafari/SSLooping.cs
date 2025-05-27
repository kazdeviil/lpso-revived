using System.Collections.Generic;
using UnityEngine;

public class SSLooping : MonoBehaviour
{
    // float representing horizontal width of sprite
    public float bound;

    // objects to loop, also children of self, set in inspector
    public List<GameObject> gameObjects = new List<GameObject>();

    // position at which to trigger teleporting the gameobject
    public float offscreenPos;

    // speed to move at
    public float speed;


    void Start()
    {
        // gets width of sprite, sets point at which to trigger loop relative to width
        bound = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        offscreenPos = bound * -1;
    }

    void Update()
    {
        Looping();
    }

    void Looping()
    {
        // moves parent (this game object) at speed, can possibly be problematic over time. fix by moving children individually instead
        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);

        // teleports offscreen object to the right of final object in list, sets object as final in list
        if (gameObjects[0].transform.position.x < offscreenPos)
        {
            GameObject obj = gameObjects[0];
            obj.transform.position = new Vector3(gameObjects[gameObjects.Count - 1].transform.position.x + bound, obj.transform.position.y, obj.transform.position.z);
            gameObjects.RemoveAt(0);
            gameObjects.Add(obj);
        }
    }
}
