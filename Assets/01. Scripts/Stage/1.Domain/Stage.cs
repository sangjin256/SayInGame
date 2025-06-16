using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage
{
    private readonly int _stageID;                                           // 스테이지 고유 ID
    private float _baseEnemySpawnFrequency;                        // 기본 적 소환 주기
    public float BaseEnemySpawnFrequency => _baseEnemySpawnFrequency;
    private float _baseEnemySpawnDensity;
    public float BaseEnemySpawnDensity => _baseEnemySpawnDensity;
    private List<Difficulty> _difficultyList;
    public List<Difficulty> DifficultyList => _difficultyList;

    public Stage(int stageID, float baseEnemySpawnFrequency, float baseEnemySpawnDensity)
    {
    if(stageID <= 0)
    {
        throw new Exception("스테이지 ID는 0보다 커야 합니다.");
    }
    if(baseEnemySpawnFrequency <= 0)
    {
        throw new Exception("적 소환 주기는 0보다 커야 합니다.");
    }
    if(baseEnemySpawnDensity <= 0)
    {
        throw new Exception("적 소환 밀도는 0보다 커야 합니다.");
    }

    _stageID = stageID;
    _baseEnemySpawnFrequency = baseEnemySpawnFrequency;
    _baseEnemySpawnDensity = baseEnemySpawnDensity;
    _difficultyList = new List<Difficulty>();
    }

     public DifficultyDTO GetDifficulty(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }

        return _difficultyList[difficultyLevel - 1].ToDTO();
     }

     public float GetEnemySpawnFrequency(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }
        return _baseEnemySpawnFrequency * _difficultyList[difficultyLevel - 1].EnemySpawnFrequency;
     }

     public float GetEnemySpawnCountMultiplier(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }
        return _baseEnemySpawnDensity * _difficultyList[difficultyLevel - 1].EnemyCountMultiplier;
     }

     public float GetHealthMultiplier(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }
        return _difficultyList[difficultyLevel - 1].EnemyHealthMultiplier;
     }
     public float GetDamageMultiplier(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }
        return _difficultyList[difficultyLevel - 1].EnemyDamageMultiplier;
     }

     public float GetEliteSpawnRate(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
        if(difficultyLevel > _difficultyList.Count)
        {
            throw new Exception("난이도는 난이도 리스트의 범위를 벗어납니다.");
        }
        return _difficultyList[difficultyLevel - 1].EliteSpawnRate;
     }

    // 난이도 리스트에 추가
     public void AddDifficultyList(Difficulty difficulty)
     {
        if (difficulty == null)
        {
            throw new Exception("난이도는 null이 될 수 없습니다.");
        }
        _difficultyList.Add(difficulty);
     }
     
}
