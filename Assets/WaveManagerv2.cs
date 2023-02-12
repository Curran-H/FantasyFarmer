using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManagerv2 : MonoBehaviour
{
    // A struct to store the wave data, including the number of each type of enemy and the wave's duration.
    [System.Serializable]
    public struct WaveData
    {
        public int numSlimes;   // Number of slimes in this wave
        public int numOrcs;     // Number of orcs in this wave
        public int numSkeletons; // Number of skeletons in this wave
        public float waveDuration;  // Total duration of this wave
    }

    [SerializeField] List<WaveData> waves = new List<WaveData>();  // List of wave data

    [SerializeField] Transform[] spawnPoints;  // Array of spawn points for enemies

    [SerializeField] GameObject slimePrefab;  // Prefab for the slime enemy
    [SerializeField] GameObject orcPrefab;    // Prefab for the orc enemy
    [SerializeField] GameObject skeletonPrefab;  // Prefab for the skeleton enemy

    private int currentWave = 0;  // Current wave number

    [SerializeField] UnityEngine.UI.Text waveCounterText;  // UI element for displaying the wave number
    [SerializeField] UnityEngine.UI.Text CenterCountdown;  // UI element for displaying the wave number

    [SerializeField] float timeBetweenWaves = 30f;  // Time between each wave

    void Start()
    {
        // Start the first wave
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        // Get the data for the current wave
        WaveData wave = waves[currentWave];

        if (currentWave == 0)
        {
            for (int i = 10; i > 0; i--)
            {
                waveCounterText.text = "Starting in: " + i;
                yield return new WaitForSeconds(1f);
                UpdateUI();
            }
            // Spawn the enemies for the current wave
            SpawnEnemies();

            // Check if the wave has been cleared
            StartCoroutine(CheckWaveCleared());

            // Wait for the duration of the wave
            yield return new WaitForSeconds(wave.waveDuration);
        }

        else
        {
            // Enable the center text before starting the countdown
            CenterCountdown.text = "";
            for (float i = 5; i > 0; i--) // Start count down from 5 in the middle of the screen
            {
                CenterCountdown.text = "" + i;
                // Wait for 1 second before decreasing the countdown value
                yield return new WaitForSeconds(1);
            }

            // Disable the center text after the countdown
            CenterCountdown.text = "";

            // Spawn the enemies for the current wave
            SpawnEnemies();

            // Check if the wave has been cleared
            StartCoroutine(CheckWaveCleared());

            // Wait for the duration of the wave
            yield return new WaitForSeconds(wave.waveDuration);
        }
    }

    void SpawnEnemies()
    {
        // Get the data for the current wave
        WaveData wave = waves[currentWave];

        // Calculate the time between each enemy spawn
        float timeBetweenSpawns = wave.waveDuration / (wave.numSlimes + wave.numOrcs + wave.numSkeletons);

        // Spawn the slimes, orcs, and skeletons for the current wave
        StartCoroutine(SpawnEnemies(slimePrefab, wave.numSlimes, timeBetweenSpawns));
        StartCoroutine(SpawnEnemies(orcPrefab, wave.numOrcs, timeBetweenSpawns));
        StartCoroutine(SpawnEnemies(skeletonPrefab, wave.numSkeletons, timeBetweenSpawns));
    }

    IEnumerator SpawnEnemies(GameObject enemyPrefab, int count, float timeBetweenSpawns)
    {
        // Spawn the specified number of enemies
        for (int i = 0; i < count; i++)
        {
            // Spawn a single enemy
            SpawnEnemy(enemyPrefab);

            // Wait before spawning the next enemy
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        // Select a random spawn point from the available spawn points
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        // Instantiate an enemy at the selected spawn point
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    IEnumerator CheckWaveCleared()
    {
        // Keep checking if there are any enemies remaining
        while (EnemiesRemaining() > 0)
        {
            yield return null;
        }

        // Increment the wave counter
        currentWave++;
        // Update the UI
        UpdateUI();

        // If all waves have been cleared, log a message
        if (currentWave >= waves.Count)
        {
            Debug.Log("All waves cleared!");
            waveCounterText.text = "All waves cleared!";
        }
        // If there are still waves left, start the next wave
        else
        {
            StartCoroutine(StartWave());
        }
    }

    int EnemiesRemaining()
    {
        int enemies = 0;

        // Check the number of enemies with the "Slime", "Orc", and "Skeleton" tags
        enemies += GameObject.FindGameObjectsWithTag("Slime").Length;
        enemies += GameObject.FindGameObjectsWithTag("Orc").Length;
        enemies += GameObject.FindGameObjectsWithTag("Skeleton").Length;

        return enemies;
    }

    void UpdateUI()
    {
        // Update the text of the waveCounterText UI element
        waveCounterText.text = "Wave: " + (currentWave + 1);
    }
}