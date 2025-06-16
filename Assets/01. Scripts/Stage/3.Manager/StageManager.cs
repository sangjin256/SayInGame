using System.Collections.Generic;
using UnityEngine;

public class StageManager : BehaviourSingleton<StageManager>
{
    private Stage _stage;
    private bool isDataLoaded = false;

    private void Awake()
    {
        Global.Instance.OnDataLoaded += OnLoadDataFunc;
    }

    private void Init()
    {
        // 기본값 설정: 스테이지 1, 난이도 1, 기본 소환 주기 1초, 기본 소환 밀도 1
        _stage = new Stage(1, 1f, 1f);
    }



    private void OnLoadDataFunc()
    {
        var timeDataList = DataTable.Instance.GetTimeDataList();
        
        Init();


        foreach(TimeData timeData in timeDataList)
        {
            _stage.AddDifficultyList(new Difficulty(timeData.TID, timeData.NextTime,
             timeData.DifficultyNum, timeData.DifficultyText,
                timeData.EnemyCountMultiplier, timeData.EnemyHealthMultiplier,
                 timeData.EnemyDamageMultiplier, timeData.EliteSpawnRate, timeData.EnemySpawnFrequency));
        }

        isDataLoaded = true;
        
        Debug.Log("난이도 리스트 초기화 완료");
    }

    public float GetEnemySpawnFrequency()
    {
        int difficultyLevel = GetStageLevel();   

        return _stage.GetEnemySpawnFrequency(difficultyLevel);
    }

    public float GetEnemySpawnCountMultiplier()
    {
        int difficultyLevel = GetStageLevel(); 

        return _stage.GetEnemySpawnCountMultiplier(difficultyLevel);
    }

    public float GetHealthMultiplier()
    {
        int difficultyLevel = GetStageLevel(); 
        
        return _stage.GetHealthMultiplier(difficultyLevel);
    }

    public float GetDamageMultiplier()
    {
        int difficultyLevel = GetStageLevel(); 

        return _stage.GetDamageMultiplier(difficultyLevel);
    }

    public float GetEliteSpawnRate()
    {
        int difficultyLevel = GetStageLevel(); 

        return _stage.GetEliteSpawnRate(difficultyLevel);
    }

    public int GetStageLevel()
    {
        float currentTime = TimeManager.Instance.TimeDTO.CurrentTime;

        int currentLevel = 1;

        foreach(Difficulty difficulty in _stage.DifficultyList)
        {
            if(currentTime > difficulty.DifficultyStartTime)
            {
                currentLevel = difficulty.DifficultyLevel+1;
            }
        }

        return currentLevel;
    }
}
