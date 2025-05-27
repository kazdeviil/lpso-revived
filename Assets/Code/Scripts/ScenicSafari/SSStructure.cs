using UnityEngine;

public class SSStructure : MonoBehaviour
{
    // set in inspector, is prefab's own sprite renderer
    [SerializeField] private SpriteRenderer spriteRenderer;

    // properties set by spawner
    [HideInInspector] public Sprite sprite;
    [HideInInspector] public int order;
    [HideInInspector] public float speed;
    [HideInInspector] public float lifespan;


    private void Start()
    {
        // sets properties
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
        // moves left at set speed
        transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
    }

    private void Lifespan()
    {
        // counts down from set lifespan, destroys self once it hits 0
        lifespan -= Time.deltaTime;
        if (lifespan <= 0 )
        {
            Destroy(gameObject);
        }
    }
}
