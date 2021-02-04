using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float _nextSpawnTime;
    [SerializeField] float _delayRangeStart = 0;
    [SerializeField] float _delayRangeEnd = 5;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] _enemies;

    void Start()
    {
        _nextSpawnTime = Time.time + Random.Range(_delayRangeStart, _delayRangeEnd);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
            Spawn();
    }

    void Spawn()
    {
        GameObject enemyToSpawn = _enemies[Random.Range(0, _enemies.Length - 1)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length - 1)];
        Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        _nextSpawnTime = Time.time + Random.Range(_delayRangeStart, _delayRangeEnd);
    }

    bool ShouldSpawn()
    {
        return Time.time >= _nextSpawnTime;
    }
}
