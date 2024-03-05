using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class path : MonoBehaviour
{
    public GameObject kibble;
    public Collider2D kibbleCircle;
    public GameObject[] pathPoints;
    public int pathNumber;
    public float speed;
    public bool kibbleCaught;

    private Vector3 actualPosition;
    private int x;

    // Start is called before the first frame update
    void Start()
    {
        x = 1;
        kibbleCaught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!kibbleCaught)
        {
            actualPosition = kibble.transform.position;
            kibble.transform.position = Vector3.MoveTowards(actualPosition, pathPoints[x].transform.position, speed * Time.deltaTime);

            if (actualPosition == pathPoints[x].transform.position && x != pathNumber - 1)
            {
                x++;
            }
        }
    }

}
