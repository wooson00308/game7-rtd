using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum SpawnModes
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public static Action OnWaveCompleted;
    
    [Header("Settings")]
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private float delayBtwWaves = 1f;

    [Header("Fixed Delay")]
    [SerializeField] private float delayBtwSpawns;
    
    [Header("Random Delay")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandomDelay;

    [Header("Poolers")] 
    [SerializeField] private ObjectPooler enemyWave1Pooler;
    [SerializeField] private ObjectPooler enemyWave2Pooler;
    [SerializeField] private ObjectPooler enemyWave3Pooler;

    int currentEnemyId = 0;

    private float _spawnTimer;
    private int _enemiesSpawned;
    private int _enemiesRamaining;
    
    private Waypoint _waypoint;

    List<Enemy> _enemies = new List<Enemy>();

    bool _canStartGame = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        _waypoint = GetComponent<Waypoint>();
        _enemiesRamaining = enemyCount;
    }

    public void GameStartEvent()
    {
        _canStartGame = true;
    }

    private void FixedUpdate()
    {
        if (!_canStartGame) return;

        _spawnTimer -= Time.fixedDeltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = delayBtwSpawns;
            //_spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    public Enemy GetEnemy(int id)
    {
        return _enemies.Find(x => x.Id.Equals(id));
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = GetPooler().GetInstanceFromPool();
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.Id = currentEnemyId++;
        enemy.Waypoint = _waypoint;
        enemy.ResetEnemy();

        enemy.transform.localPosition = transform.position;
        newInstance.SetActive(true);

        _enemies.Add(enemy);
    }

    private float GetSpawnDelay()
    {
        float delay = delayBtwSpawns;
        if (spawnMode == SpawnModes.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else
        {
            delay = GetRandomDelay();
        }

        return delayBtwSpawns;
    }
    
    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandomDelay);
        return randomTimer;
    }

    private ObjectPooler GetPooler()
    {
        int currentWave = LevelManager.Instance.CurrentWave;
        if (currentWave <= 1) // 1- 10
        {
            return enemyWave1Pooler;
        }

        if (currentWave > 1 && currentWave <= 2) // 11- 20
        {
            return enemyWave2Pooler;
        }
        
        if (currentWave > 2 && currentWave <= 3) // 21- 30
        {
            return enemyWave3Pooler;
        }
        


        return null;
    }
    
    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves);
        _enemiesRamaining = enemyCount;
        _spawnTimer = 0f;
        _enemiesSpawned = 0;
    }
    
    private void RecordEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _enemiesRamaining--;
        if (_enemiesRamaining <= 0)
        {
            OnWaveCompleted?.Invoke();
            StartCoroutine(NextWave());
        }
    }
    
    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemy;
        EnemyHealth.OnEnemyKilled += RecordEnemy;

        LevelManager.GameStartEvent += GameStartEvent;
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemy;
        EnemyHealth.OnEnemyKilled -= RecordEnemy;

        LevelManager.GameStartEvent -= GameStartEvent;
    }
}
