using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public static WaveManager Instance;

    private void Awake() {
        if(Instance == null) 
            Instance = this;
    }

    private void Update() {
    }

    public void SpawnWave() {
    }
}
