using UnityEngine;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.AI;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> SpawnPointList;
    public GameObject EnemyPrefab;

    public float Radius = 1f;
    public float Timer = 3f;
    public int MaxSpawnCount = 10;
    private float _elapsedTime = 0f;
    [SerializeField] private int _SpawnCount = 0;

    private void Update()
    {
        if (_SpawnCount >= MaxSpawnCount) return;
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= Timer)
        {
            Vector3 spawnPoint = SpawnPointList[Random.Range(0, SpawnPointList.Count)].position;
            Health enemyHealth = Instantiate(EnemyPrefab, RandomClusteredPosition(spawnPoint), Quaternion.identity).GetComponent<Health>();
            enemyHealth.OnDie += OnDie;
            _elapsedTime = 0f;
            _SpawnCount++;
        }
    }

    private void OnDie()
    {
        _SpawnCount--;
    }

    public Vector3 RandomClusteredPosition(Vector3 spawnPoint)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float innterRadius = Mathf.Sqrt(Random.Range(0f, 1f));

        float x = Mathf.Cos(angle) * innterRadius * Radius;
        float y = Mathf.Sin(angle) * innterRadius * Radius;

        return spawnPoint + new Vector3(x, 0, y);
    }
}
