using UnityEngine;

public class SSSpawner : MonoBehaviour
{
    public enum SpawnType { Sprite, Object };
    public SpawnType Type;

    // parent of spawned, also just. this game object
    private GameObject parent;

    // prefab of spawned
    [SerializeField] private GameObject structure;

    // possible sprites to set to spawned
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject[] objects;

    // order in layer for spawned
    [SerializeField] private int order;

    // speed for spawned
    [SerializeField] private float speed;

    // time in seconds that spawned stays in scene
    [SerializeField] private float spawnLifespan;



    // how likely it is to spawn a spawned
    [SerializeField] private int spawnChance;

    // upper limit for allowing a spawn (slightly misleading name, the actual "cooldown" is spawnTimerDefault - spawnCooldown)
    [SerializeField] private float spawnCooldown;

    // how often, in seconds, a spawn attempt is made
    [SerializeField] private float spawnIntervalDefault;

    // the countdown for each interval
    private float spawnIntervalCurrent;

    // max amount of time allowed between spawns
    [SerializeField] private float spawnTimerDefault;

    // the countdown for timer
    private float spawnTimerCurrent;

    // range of allowed vertical variance for spawn position
    [SerializeField] private float verticalOffsetMax;
    private float range;


    private void Start()
    {
        parent = gameObject;
        range = parent.transform.position.y + verticalOffsetMax;
        spawnTimerCurrent = spawnTimerDefault;
        spawnIntervalCurrent = spawnIntervalDefault;
    }

    private void Update()
    {
        SpawnTimer();
    }

    void SpawnTimer()
    {
        // counts down
        spawnTimerCurrent -= Time.deltaTime;

        // forces spawn if time is up
        if (spawnTimerCurrent <= 0)
        {
            Spawn();
        }
        else
        {
            // starts spawn chance if cooldown is chillin w it
            if (spawnTimerCurrent <= spawnCooldown)
            {
                SpawnChance();
            }
        }
    }

    void SpawnChance()
    {
        // counts down
        spawnIntervalCurrent -= Time.deltaTime;

        // checks once every interval
        if (spawnIntervalCurrent <= 0)
        {
            // random chance to spawn every interval
            int ran = Random.Range(0, spawnChance);
            if (ran == 0)
            {
                Spawn();
            }
            // resets count every cycle
            spawnIntervalCurrent = spawnIntervalDefault;
        }
    }

    void Spawn()
    {
        // creates spawned with relevant properties
        float spawnY = Random.Range(parent.transform.position.y, range);
        Vector3 spawnPos = new Vector3(parent.transform.position.x, spawnY, parent.transform.position.z);
        GameObject newStructure = Instantiate(structure, spawnPos, Quaternion.identity, parent.transform);
        if (Type == SpawnType.Sprite)
        {
            int sprite = Random.Range(0, sprites.Length);
            newStructure.GetComponent<SSStructure>().sprite = sprites[sprite];
            newStructure.GetComponent<SSStructure>().order = order;
            newStructure.GetComponent<SSStructure>().speed = speed;
            newStructure.GetComponent<SSStructure>().lifespan = spawnLifespan;
        }
        else
        {
            // idk yet
        }

        // resets spawn timer
        spawnTimerCurrent = spawnTimerDefault;
    }
}
