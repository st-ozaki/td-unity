using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Each wave can contain multiple enemy groups
[System.Serializable]
public class EnemyGroup
{
    //The type of enemy to spawn
    public string m_enemy;
    //The number of enemies to spawn
    public int m_count;
    //The time between spawning each enemy
    public float m_spawnDelay = 0.05f;
    //The time before this group spawns
    public float m_startDelay = 0.0f;
}

//Not sure if this is needed but I'm using for it now :)
[System.Serializable]
public class WaveSpawn
{
    //The time after the last enemy of the wave has been spawned
    public float m_timeBetweenWaves = 2.0f;
    //A list of the enemy groups for this wave
    public List<EnemyGroup> m_enemies;
}

[System.Serializable]
public class LevelWaves
{
    //A list of the waves for the level
    public List<WaveSpawn> m_waves;
}