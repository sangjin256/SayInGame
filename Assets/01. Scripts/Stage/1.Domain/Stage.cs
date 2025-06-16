using System.Collections.Generic;
using UnityEngine;
using System;

public class Stage
{
    private readonly int _stageID;                                           // 스테이지 고유 ID
    private int _currentDifficultyLevel;

    private List<Difficulty> _difficultyList;
    public List<Difficulty> DifficultyList => _difficultyList;

    public Stage(int stageID, int currentDifficultyLevel)
    {
        if(stageID <= 0)
        {
            throw new Exception("스테이지 ID는 0보다 커야 합니다.");
        }
        if(currentDifficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }

        _stageID = stageID;
        _difficultyList = new List<Difficulty>();
        _currentDifficultyLevel = currentDifficultyLevel;
    }

     public DifficultyDTO GetDifficulty()
     {
        return _difficultyList[_currentDifficultyLevel - 1].ToDTO();
     }

     public void SetCurrentDifficultyLevel(int difficultyLevel)
     {
        if(difficultyLevel <= 0)
        {
            throw new Exception("난이도는 0보다 커야 합니다.");
        }
     }

     public void IncreaseCurrentDifficultyLevel()
     {
        if(_currentDifficultyLevel >= _difficultyList.Count)
        {
            throw new Exception($"난이도는 난이도 리스트의 범위를 벗어납니다. 현재 난이도: {_currentDifficultyLevel} 난이도 리스트 개수: {_difficultyList.Count}");
        }
        _currentDifficultyLevel++;
     }

     public float GetEnemySpawnFrequency()
     {
        return _difficultyList[_currentDifficultyLevel - 1].EnemySpawnFrequency;
     }

     public float GetEnemySpawnCountMultiplier()
     {
        return _difficultyList[_currentDifficultyLevel - 1].EnemyCountMultiplier;
     }

     public float GetHealthMultiplier()
     {
        return _difficultyList[_currentDifficultyLevel - 1].EnemyHealthMultiplier;
     }
     public float GetDamageMultiplier()
     {
        return _difficultyList[_currentDifficultyLevel - 1].EnemyDamageMultiplier;
     }

     public float GetEliteSpawnRate()
     {
        return _difficultyList[_currentDifficultyLevel - 1].EliteSpawnRate;
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
