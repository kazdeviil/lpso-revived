using UnityEngine;

public class SSStructure : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public int order;
    public float speed;
    public float lifespan;


    private void Start()
    {
        spriteRenderer.sprite = sprite;
        spriteRenderer.sortingOrder = order;
    }

    private void Update()
    {
        Movement();
        Lifespan();
    }

    private void Movement()
    {
        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void Lifespan()
    {
        lifespan -= Time.deltaTime;
        if (lifespan < 0 )
        {
            Destroy(gameObject);
        }
    }
}
