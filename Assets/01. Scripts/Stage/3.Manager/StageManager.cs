using System.Collections.Generic;
using UnityEngine;

public class StageManager : BehaviourSingleton<StageManager>
{
    [SerializeField]
    private List<DifficultySO> _difficultySOList;
    private List<Difficulty> _difficultyList;

    private Stage _stage;
    private bool isDataLoaded = false;

    private void Awake()
    {
        Global.Instance.OnDataLoaded += OnLoadDataFunc;
        Init();
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        // TODO: 로드된 데이터를 기반으로 만들어야 할듯?
        _stage = new Stage(1, 1, 1, 1);
    }

    private void OnLoadDataFunc()
    {
        var timeDataList = DataTable.Instance.GetTimeDataList();

        foreach(TimeData timeData in timeDataList)
        {
            _stage.AddDifficultyList(new Difficulty(timeData.TID, timeData.Time,
             timeData.DifficultyNum, timeData.DifficultyText,
                timeData.EnemyCountMultiplier, timeData.EnemyHealthMultiplier,
                 timeData.EnemyDamageMultiplier, timeData.EliteSpawnRate, timeData.EnemySpawnFrequency));
        }

        isDataLoaded = true;
        
        Debug.Log("난이도 리스트 초기화 완료");
    }

    private void UpdateStage()
    {
        _stage.IncreaseDifficulty();
    }

    public int GetStageLevel()
    {
        return _stage.CurrentDifficultyLevel;
    }

    public float GetBaseEnemySpawnFrequency()
    {
        return _stage.MultiplyBaseEnemySpawnFrequency();
    }

    public float GetBaseEnemySpawnDensity()
    {
        return _stage.MultiplyBaseEnemySpawnDensity();
    }

    public void GetAvailableMonsterTypes()
    {
    }
}
