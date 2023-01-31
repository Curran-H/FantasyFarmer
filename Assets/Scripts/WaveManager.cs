using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public static WaveManager Instance;
    public List<GameObject> m_enemy=new List<GameObject>();
    //private List<LaneSpawner> m_laneSpawners= new List<LaneSpawner>();
    [SerializeField]private LaneSpawner[] m_laneSpawners;
    private void Awake() {
        if(Instance == null) 
            Instance = this;
    }
    private void Start()
    {
        m_laneSpawners = new LaneSpawner[transform.childCount];//the lanes are children of wavemanager so create an array that can fit all of them
        InitialiseLaneSpawners();
    }
    private void Update() {
        m_laneSpawners[0].SetActive(true);
        m_laneSpawners[0].SetEnemyToSpawn(m_enemy[0]);
        
    }

    public void SpawnWave() {
    }

    //filling array with laneSpawners
    void InitialiseLaneSpawners()
    {

        m_laneSpawners = GetComponentsInChildren<LaneSpawner>();
        
    }
}
