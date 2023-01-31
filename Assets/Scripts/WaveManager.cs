using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public int m_waveNumber;

    public static WaveManager Instance;
    /// <summary>stores indexes of which lanes are to be active</summary>
    [SerializeField] private List<int> m_activeLaneIndexes = new List<int>();

    ///<summary>how many lanes can spawn enemies at once</summary>
    [SerializeField] private int m_maxPossibleActiveLanes;

    ///<summary>minumum time to wait between enemy spawns in a lane</summary>
    [SerializeField] private int m_minSpawnInterval;

    ///<summary>maximum time to wait between enemy spawns in a lane</summary>
    [SerializeField] private int m_maxSpawnInterval;

    ///<summary>stores integer that states how long to wait before changing which lanes are active</summary>
    [SerializeField] private int laneSwapInterval;

    private float m_elapsedTime;

    public List<GameObject> m_enemies=new List<GameObject>();//fill the list with different enemies and use their index to set what lanes spawn what specific enemy
    
    [SerializeField]private LaneSpawner[] m_laneSpawners;
    private void Awake() {
        if(Instance == null) 
            Instance = this;
        
    }
    private void Start()
    {
        m_waveNumber = 0;
        m_elapsedTime = 0;
        m_laneSpawners = new LaneSpawner[transform.childCount];//the lanes are children of wavemanager so create an array that can fit all of them
        InitialiseLaneSpawners();
    }
    private void Update() {
        m_elapsedTime += Time.deltaTime;
        if (m_elapsedTime > laneSwapInterval)
        {

        }
        
    }

    //filling array with laneSpawners

    void SelectRandomActiveLanes()
    {
        
        for(int i = 0; i < m_maxPossibleActiveLanes; i++)
        {
            m_activeLaneIndexes.Add((int)Random.Range(0, m_laneSpawners.Length));
            
        }
    }

    void ActivateLanes()
    {
        //deactivate all lanes first
        foreach(LaneSpawner ls in m_laneSpawners)
        {
            ls.SetActive(false);
        }
        //now activate the new active lanes
        for(int i = 0; i < m_activeLaneIndexes.Count; i++)
        {
            m_laneSpawners[m_activeLaneIndexes[i]].SetActive(true);
        }
    }
    void InitialiseLaneSpawners()
    {

        m_laneSpawners = GetComponentsInChildren<LaneSpawner>();
        
    }
}
