using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class RoundEnemies
    {
        public int startRound;
        public GameObject[] enemyPrefabs;
    }

    [Header("Spawn Settings")]
    public List<RoundEnemies> roundEnemies = new List<RoundEnemies>();
    public float spawnInterval = 3f;
    public float spawnArea = 5f;
    [Tooltip("”меньшение интервала спавна с каждым раундом")]
    public float spawnIntervalReduction = 0.1f;
    public float minSpawnInterval = 0.5f;

    private List<GameObject> availableEnemies = new List<GameObject>();
    private int currentRound;

    private void Start()
    {
        currentRound = RoundManager.Instance.currentRound;
        UpdateAvailableEnemies();
        StartSpawning();
    }

    private void OnEnable()
    {
        RoundManager.OnRoundStarted += OnRoundStarted;
    }

    private void OnDisable()
    {
        RoundManager.OnRoundStarted -= OnRoundStarted;
    }

    private void OnRoundStarted(int roundNumber)
    {
        currentRound = roundNumber;
        UpdateAvailableEnemies();

        spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalReduction);

        CancelInvoke("SpawnEnemy");
        StartSpawning();
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void UpdateAvailableEnemies()
    {
        availableEnemies.Clear();

        foreach (var round in roundEnemies)
        {
            if (currentRound >= round.startRound)
            {
                availableEnemies.AddRange(round.enemyPrefabs);
            }
        }
    }

    void SpawnEnemy()
    {
        if (availableEnemies.Count == 0)
        {
            Debug.LogWarning("Ќет доступных противников дл€ спавна!");
            return;
        }

        GameObject enemyToSpawn = GetRandomEnemyPrefab();
        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }

    GameObject GetRandomEnemyPrefab()
    {
        return availableEnemies[Random.Range(0, availableEnemies.Count)];
    }

    Vector2 GetSpawnPosition()
    {
        return new Vector2(
            Random.Range(-spawnArea, spawnArea),
            Random.Range(-spawnArea, spawnArea)
        );
    }
}