using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LaneSpawner : MonoBehaviour {
    /// <summary>
    /// if true, prevents a lane from spawning anything
    /// </summary>
    bool m_isActive;
    /// <summary>
    /// Interval between enemy spawns
    /// </summary>

    ///<summary>
    ///how long to wait before spawning another enemy
    ///</summary>
    public float m_spawnTimeInterval;


    private float m_elapsedTime;//time counter
    /// <summary>
    /// what enemy this lane is spawning
    /// </summary>
    private GameObject m_enemyToSpawn;
    
    void Start()
    {
        m_elapsedTime = 0;
       
        m_isActive = false;//all lanes start out deactivated and are randomly activated from WaveManager
        m_spawnTimeInterval = 0.5f;//ill change this default value once i add random intervals in WaveManager
    }

    // Update is called once per frame
    void Update() {
        if (m_isActive)//if lane is not active
        {
            m_elapsedTime += Time.deltaTime;
            if (m_elapsedTime >= m_spawnTimeInterval)
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
        WaveManager.Instance.m_enemyCount++;//this is not great but it works and ill change it later
        GameObject enemy = Instantiate(m_enemyToSpawn, transform.position, UnityEngine.Quaternion.identity);
    }
    public void SetActive(bool active)
    {
        m_isActive = active;
    }
}
