using UnityEngine;

public class SSSpawner : MonoBehaviour
{
    public GameObject parent;
    public GameObject structure;
    public Sprite[] sprites;
    public int order;
    public float speed;
    public float spawnLifespan;

    public int spawnChance;
    public float spawnCooldown;
    public float spawnIntervalDefault;
    public float spawnIntervalCurrent;

    public float spawnTimerDefault;
    public float spawnTimerCurrent;


    private void Start()
    {
        spawnTimerCurrent = spawnTimerDefault;
        spawnIntervalCurrent = spawnIntervalDefault;
    }

    private void Update()
    {
        SpawnTimer();
    }

    void SpawnTimer()
    {
        spawnTimerCurrent -= Time.deltaTime;
        if (spawnTimerCurrent <= 0)
        {
            Debug.Log("Timeout");
            Spawn();
        }
        else
        {
            if (spawnTimerCurrent <= spawnCooldown)
            {
                SpawnChance();
            }
        }
    }

    void SpawnChance()
    {
        spawnIntervalCurrent -= Time.deltaTime;
        if (spawnIntervalCurrent <= 0)
        {
            int ran = Random.Range(0, spawnChance);
            if (ran == 0)
            {
                Debug.Log("Spawn chance");
                Spawn();

            }
            spawnIntervalCurrent = spawnIntervalDefault;
        }
    }

    void Spawn()
    {
        int sprite = Random.Range(0, sprites.Length);
        GameObject newStructure = Instantiate(structure, parent.transform.position, Quaternion.identity, parent.transform);
        newStructure.GetComponent<SSStructure>().order = order;
        newStructure.GetComponent<SSStructure>().sprite = sprites[sprite];
        newStructure.GetComponent<SSStructure>().speed = speed;
        newStructure.GetComponent<SSStructure>().lifespan = spawnLifespan;

        Debug.Log($"Spawned structure {sprite} with parent {gameObject.name} / {parent.name}");

        spawnTimerCurrent = spawnTimerDefault;
    }
}
