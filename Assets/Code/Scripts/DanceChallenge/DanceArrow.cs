using UnityEngine;

public class DanceArrow : MonoBehaviour
{
    public float scaleX;
    public float rotationZ;
    public GameObject hitbox;

    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 15f;
        if (scaleX == 1 &&  rotationZ == 0)
        {
            // going left
            speed *= -1;
        }
        else if (scaleX == -1 && rotationZ == 0)
        {
            //
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
    }
}
