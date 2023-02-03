using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBush : TurretController {
    
    [SerializeField] private GameObject m_toGenerate;
    [SerializeField] private float m_timeBetweenGeneration = 2; 
    [SerializeField] private int m_amountToGenMin = 1;
    [SerializeField] private int m_amountToGetMax = 2;

    private float m_nextGenerationTime;

    private void Start() {
        m_nextGenerationTime = Time.time + m_timeBetweenGeneration;
    }

    private void Update() {
        if(Time.time >= m_nextGenerationTime) {
            Generate();
        }
    }

    private void Generate() {
        int amountToGen = Random.Range(m_amountToGenMin, m_amountToGetMax+1);
        
        for(int i=0; i<amountToGen; i++) {
            Vector3 genPos = transform.position + new Vector3(Random.Range(-1.25f, 2.25f), Random.Range(-1.25f, 2.25f), transform.position.z);
            GameObject go = Instantiate(m_toGenerate, genPos, Quaternion.identity);
        }

        m_nextGenerationTime = Time.time + m_timeBetweenGeneration;
    }
}
