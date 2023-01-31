using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{


    /// <summary>
    /// allows you to disable a lane from spawning anything
    /// </summary>
    bool m_isActive;
    /// <summary>
    /// defaults to 5 seconds
    /// </summary>
    public int m_spawnTimeInterval;
    private float m_elapsedTime;
    private GameObject m_enemyToSpawn;
    void Start()
    {
        m_isActive = false;
        m_spawnTimeInterval = 5;
    }

    // Update is called once per frame
    void Update()
    {

        if (m_isActive)//if lane is not active
        {
            m_elapsedTime += Time.deltaTime;
            if (m_elapsedTime > m_spawnTimeInterval)
            {
                SpawnEnemy();
                m_elapsedTime = 0;
            }
        }

    }
    public void SetEnemyToSpawn(GameObject enemy)
    {
        m_enemyToSpawn = enemy;
    }
    public void SpawnEnemy()
    {
        GameObject enemy = Instantiate(m_enemyToSpawn, transform.position, Quaternion.identity);
    }
    public void SetActive(bool active)
    {
        m_isActive = active;

    }
}
