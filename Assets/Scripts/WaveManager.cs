using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    /// <summary>
    /// wave number
    /// </summary>
    public int m_waveNumber;


    public int m_waveOneEnemyLimit;//edit these in inspector
    public int m_waveTwoEnemyLimit;//edit these in inspector
    public int m_waveThreeEnemyLimit;//edit these in inspector

    /// <summary>
    /// Time between waves
    /// </summary>
    public int m_timeBetweenWaves;

    /// <summary>
    /// counts how many enemies have been spawned in this wave so far
    /// </summary>
    [SerializeField] public int m_enemyCount;


    public static WaveManager Instance;

    /// <summary>
    /// stores indexes of which lanes are to be active
    /// </summary>
    [SerializeField] private List<int> m_activeLaneIndexes = new List<int>();

    ///<summary>
    ///how many lanes can spawn enemies at once
    ///</summary>
    [SerializeField] private int m_maxPossibleActiveLanes;

    ///<summary>
    ///minumum time to wait between enemy spawns in a lane
    ///</summary>
    [SerializeField] private float m_minSpawnInterval;

    /// <summary>
    /// maximum time to wait between enemy spawns in a lane
    /// </summary>
    [SerializeField] private float m_maxSpawnInterval;

    ///<summary>
    ///stores integer that states how long to wait before changing which lanes are active
    ///</summary>
    [SerializeField] private int laneSwapInterval;

    [SerializeField]private float m_elapsedTime;

    [SerializeField]private float m_nextWaveElapsedTime;

    /// <summary>
    /// how frequently should spawn time in a lane be changed, should be larger than maxSpawnTime
    /// but smaller than laneswap interval
    /// </summary>
    [SerializeField] private float m_changingSpawnTime;

    [SerializeField]private float elapsedChangingSpawnTime;

    public List<GameObject> m_enemies = new List<GameObject>();//fill the list with different enemies and use their index to set what lanes spawn what specific enemy

    [SerializeField] private LaneSpawner[] m_laneSpawners;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }
    private void Start()
    {
        m_nextWaveElapsedTime = 0;
        m_waveNumber = 1;
        m_elapsedTime = 0;
        m_laneSpawners = new LaneSpawner[transform.childCount];//the lanes are children of wavemanager so create an array that can fit all of them
        InitialiseLaneSpawners();
    }
    private void Update()
    {
        waveOne();
        


    }

   void waveOne()
    {

        elapsedChangingSpawnTime += Time.deltaTime;
        m_elapsedTime += Time.deltaTime;

        if (elapsedChangingSpawnTime > m_changingSpawnTime)
        {
            SetSpawnIntervals();
            elapsedChangingSpawnTime = 0;

        }

        
        
        if (m_elapsedTime > laneSwapInterval)
        {

            SelectRandomActiveLanes();
            ActivateLanes();
            

            m_elapsedTime = 0;
        }
    }
    void waveTwo()
    {
        //change settings for wave 2 here, e.g more active lanes at once or a shorter interval
        m_waveNumber = 2;
        m_maxSpawnInterval = 10;
        m_elapsedTime += Time.deltaTime;
        if (m_elapsedTime > laneSwapInterval)
        {

            SelectRandomActiveLanes();
            ActivateLanes();

            m_elapsedTime = 0;
        }
    }
    void waveThree()
    {
        //change settings for wave 3 here, e.g more active lanes at once or a shorter interval
        
        m_waveNumber++;
        m_elapsedTime += Time.deltaTime;
        if (m_elapsedTime > laneSwapInterval)
        {

            SelectRandomActiveLanes();
            ActivateLanes();

            m_elapsedTime = 0;
        }
    }









    /// <summary>
    /// Generates indexes of lanes to be activated
    /// </summary>
    void SelectRandomActiveLanes()
    {
        m_activeLaneIndexes.Clear();
        for (int i = 0; i < m_maxPossibleActiveLanes; i++)
        {
            
            m_activeLaneIndexes.Add((int)UnityEngine.Random.Range(0, m_laneSpawners.Length));

        }
    }

    /// <summary>
    /// Sets which lanes are to be activated and deactivated previously activated lanes
    /// </summary>
    void ActivateLanes()
    {
        //deactivate all lanes first
        foreach (LaneSpawner ls in m_laneSpawners)
        {
            ls.SetActive(false);
        }
        //now activate the new active lanes
        for (int i = 0; i < m_activeLaneIndexes.Count; i++)
        {
            m_laneSpawners[m_activeLaneIndexes[i]].SetActive(true);
        }
    }

    /// <summary>
    /// fills array with laneSpawners and sets the enemy to spawn,will replace the enemy part with a separate function when we add more enemies  
    /// </summary>
    void InitialiseLaneSpawners()
    {

       m_laneSpawners = GetComponentsInChildren<LaneSpawner>();
       foreach(LaneSpawner ls in m_laneSpawners)
        {
            ls.SetEnemyToSpawn(m_enemies[0]);
        }

    }
    void SetSpawnIntervals()
    {
        for(int i = 0; i < m_activeLaneIndexes.Count; i++)
        {
            m_laneSpawners[m_activeLaneIndexes[i]].m_spawnTimeInterval = UnityEngine.Random.Range(m_minSpawnInterval, m_maxSpawnInterval);
        }
    }
   
    
}
