using UnityEngine;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.AI;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> SpawnPointList;
    public GameObject EnemyPrefab;

    public float Radius = 1f;
    public int MaxSpawnCount = 10;
    private float _elapsedTime = 0f;
    [SerializeField] private int _SpawnCount = 0;
    [SerializeField] private GameObject _eliteEnemyPrefab;

    private void Update()
    {
        if(!StageManager.Instance.IsDataLoaded) return;

        if (_SpawnCount >= MaxSpawnCount) return;
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= StageManager.Instance.GetEnemySpawnFrequency())
        {
            //
            Debug.Log($"스폰 카운트: {StageManager.Instance.GetDifficultyLevel()} {StageManager.Instance.GetEnemySpawnFrequency()} {StageManager.Instance.GetEnemySpawnCountMultiplier()} {StageManager.Instance.GetHealthMultiplier()} {StageManager.Instance.GetEliteSpawnRate()}");

            Vector3 spawnPoint = SpawnPointList[Random.Range(0, SpawnPointList.Count)].position;

            // 엘리트 스폰 확률
            Health enemyHealth = null;
            if(Random.value < StageManager.Instance.GetEliteSpawnRate())
            {
               enemyHealth = Instantiate(_eliteEnemyPrefab, RandomClusteredPosition(spawnPoint), Quaternion.identity).GetComponent<Health>(); 
            }
            else
            {
                enemyHealth = Instantiate(EnemyPrefab, RandomClusteredPosition(spawnPoint), Quaternion.identity).GetComponent<Health>(); 
            }
            
            // 스테이지에 따라서 조정
            MaxSpawnCount = (int)(MaxSpawnCount * StageManager.Instance.GetEnemySpawnCountMultiplier());
            enemyHealth.MaxHealth = enemyHealth.MaxHealth * StageManager.Instance.GetHealthMultiplier();
            //데미지
            // 프로젝타일 마다 데미지가 있음

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
