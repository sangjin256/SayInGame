using System.Collections.Generic;
using UnityEngine;

public class StageManager : BehaviourSingleton<StageManager>
{
    private Stage _stage;
    public bool IsDataLoaded = false;

    private void Awake()
    {
        Global.Instance.OnDataLoaded += OnLoadDataFunc;
    }

    private void Start()
    {
        TimeManager.Instance.OnDifficultyChanged += IncreaseStage;
    }

    private void Init()
    {
        _stage = new Stage(1, 1);
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

        _stage.SetCurrentDifficultyLevel(GetDifficultyLevel());

        IsDataLoaded = true;
        
        Debug.Log("난이도 리스트 초기화 완료");
    }

    public void IncreaseStage()
    {
        _stage.IncreaseCurrentDifficultyLevel();
    }

    public float GetEnemySpawnFrequency()
    {
        return _stage.GetEnemySpawnFrequency();
    }

    public float GetEnemySpawnCountMultiplier()
    {
        return _stage.GetEnemySpawnCountMultiplier();
    }

    public float GetHealthMultiplier()
    {
        return _stage.GetHealthMultiplier();
    }

    public float GetDamageMultiplier()
    {
        return _stage.GetDamageMultiplier();
    }

    public float GetEliteSpawnRate()
    {
        return _stage.GetEliteSpawnRate();
    }

    public float GetNextDifficultyTime()
    {
        return _stage.GetNextDifficultyTime();
    }

    public int GetDifficultyLevel()
    {
        float currentTime = TimeManager.Instance.TimeDTO.CurrentTime;

        int currentLevel = 1;

        foreach(Difficulty difficulty in _stage.DifficultyList)
        {
            if(currentTime > difficulty.NextDifficultyStartTime)
            {
                currentLevel = difficulty.DifficultyLevel+1;
            }
        }

        return currentLevel;
    }
}
