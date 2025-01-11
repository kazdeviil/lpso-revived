using System.Collections.Generic;
using UnityEngine;

public class SSLooping : MonoBehaviour
{
    public float bound;
    public List<GameObject> gameObjects = new List<GameObject>();
    public float offscreenPos;
    public float speed;

    void Start()
    {
        bound = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        offscreenPos = bound * -1;
    }

    // Update is called once per frame
    void Update()
    {
        Looping();
    }

    void Looping()
    {
        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        if (gameObjects[0].transform.position.x < offscreenPos)
        {
            GameObject obj = gameObjects[0];
            obj.transform.position = new Vector3(gameObjects[gameObjects.Count - 1].transform.position.x + bound, obj.transform.position.y, obj.transform.position.z);
            gameObjects.RemoveAt(0);
            gameObjects.Add(obj);
        }
    }
}
