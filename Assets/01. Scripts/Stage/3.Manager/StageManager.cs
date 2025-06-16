using System.Collections.Generic;
using UnityEngine;

public class StageManager : BehaviourSingleton<StageManager>
{
    [SerializeField]
    private List<DifficultySO> _difficultySOList;
    private List<Difficulty> _difficultyList;

    private Stage _stage;

    private void Awake()
    {

        Init();
    }

    private void Init()
    {
        _difficultyList = new List<Difficulty>(_difficultySOList.Count);

        _stage = new Stage(1, 1, 1, 1);

        // 난이도 리스트 초기화
        foreach (DifficultySO difficultySO in _difficultySOList)
        {
            // TODO: 예외 처리?

            // TODO: 난이도 리스트에 추가
            _stage.AddDifficulty(new Difficulty());
        }
    }

    public int GetStageLevel()
    {
        return _stage.CurrentDifficultyLevel;
    }

    public void GetBaseEnemySpawnFrequency(int stageLevel)
    {
    }

    public void GetBaseEnemySpawnDensity(int stageLevel)
    {
    }

    public void GetAvailableMonsterTypes(int stageLevel)
    {

    }
}
