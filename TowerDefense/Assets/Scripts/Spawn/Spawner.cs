using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads level information from the json level file
/// Uses coroutines to spawn each group of enemies and each wave. If needed the coroutines can be replaced with manual timers
/// </summary>
public class Spawner : MonoBehaviour
{
    //this should be read in by some serialization
    public LevelWaves m_waves;
    
    //Where the enemies should spawn
    public GameObject m_spawnLocation;
    //The destination for the enemies
    public GameObject m_destLoc;

    //Current level. This will need to be set when loading into the level
    protected int m_curLevel = 1;

    //The current wave number
    private int m_curWave = 0;
    //save the number of groups to be spawned this wave
    private int m_totalGroupsNeededToSpawn = 0;

    //dictionary to hold the enemy prefabs.
    private Dictionary<string, GameObject> m_enemyPrefabs = new Dictionary<string, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        //reset the current wave
        m_curWave = 0;

        //Read the level data from the level json file
        TextAsset textAsset = Resources.Load<TextAsset>("Levels/Level" + m_curLevel);
        m_waves = JsonUtility.FromJson<LevelWaves>(textAsset.text);

        //Start the first wave. This will probably moved to a start button event
        StartWave();
    }
    
    //Starts the current wave
    protected void StartWave()
    {
        //reset the groups needed to spawn and the 
        m_totalGroupsNeededToSpawn = 0;

        //Loop through the enemy groups and start the spawn groups coroutine.
        WaveSpawn waveSpawn = m_waves.m_waves[m_curWave];
        foreach (EnemyGroup group in waveSpawn.m_enemies)
        {
            //Track the number of groups that spawned
            m_totalGroupsNeededToSpawn++;
            StartCoroutine(SpawnGroup(group));
        }

        //If there are still more waves then queue up the next wave
        if( m_curWave + 1 < m_waves.m_waves.Count )
        {
            StartCoroutine(NextWave());
        }
    }

    //Waits for the current wave to completely spawn then increments the wave number and starts the next wave
    protected IEnumerator NextWave()
    {
        //wait until all the groups have spawned
        yield return new WaitUntil(() => m_totalGroupsNeededToSpawn <= 0);

        //increment the wave number
        m_curWave++;

        //wait for the delay for the new wave
        yield return new WaitForSeconds(m_waves.m_waves[m_curWave].m_timeBetweenWaves);

        //start the wave
        StartWave();
    }

    //Spawn a given enemy group
    protected IEnumerator SpawnGroup(EnemyGroup group)
    {
        //delay by the amount of start
        yield return new WaitForSeconds(group.m_startDelay);

        //loop through the group and spawn each enemy after the spawn delay
        while(group.m_count > 0)
        {
            Spawn(group.m_enemy);
            group.m_count--;
            yield return new WaitForSeconds(group.m_spawnDelay);
        }

        //this group is done spawning so decrement the total group
        m_totalGroupsNeededToSpawn--;
    }

    //Spawns the given enemy
    private void Spawn(string enemyName)
    {
        //Get the enemy prefab based on the name
        GameObject enemy = GetEnemyPrefab(enemyName);

        //Spawn the given enemy
        GameObject spawnedObject = GameObject.Instantiate<GameObject>(enemy, m_spawnLocation.transform.position, Quaternion.identity);
        if(spawnedObject)
        {
            //Set where the enemy should go
            spawnedObject.GetComponent<BaseEnemy>().GoToLocation(m_destLoc.transform.position);
        }
    }

    private GameObject GetEnemyPrefab(string enemyName)
    {
        //If the enemy prefab isn't in the prefab dictionary then load it and add it to the dictionary
        //This part can probably be done at the start by going through the list of needed enemies for the level
        if (!m_enemyPrefabs.ContainsKey(enemyName))
        {
            m_enemyPrefabs.Add(enemyName, Resources.Load<GameObject>("Enemy/" + enemyName));
        }
        return m_enemyPrefabs[enemyName];
    }
}
