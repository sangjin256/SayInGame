using System.Threading;
using UnityEngine;
using System;
using Unity.VisualScripting;

public enum EAchievementCondition
{
    GoldCollect,
    DronKillCount,
    BossKillCount,
    PlayTime,
    Trigger,
    AttendanceCount,
}

public class Achievement
{
    public readonly string ID;
    public readonly string Name;
    public readonly string Description;
    public readonly EAchievementCondition Condition;
    public int GoalValue;
    public ECurrencyType RewardCurrencyType;
    public int RewardAmount;

    private int _currentValue;
    public int CurrentValue => _currentValue;

    private bool _rewardClaimed;
    public bool RewardClaimed => _rewardClaimed;

    public Achievement(AchievementSO metaData, AchievementDTO saveData)
    {
        if (string.IsNullOrEmpty(metaData.ID))
        {
            throw new Exception("업적 ID는 비어있을 수 없습니다.");
        }
        if (string.IsNullOrEmpty(metaData.Name))
        {
            throw new Exception("업적 이름은 비어있을 수 없습니다.");
        }

        if (string.IsNullOrEmpty(metaData.Description))
        {
            throw new Exception("업적 설명은 비어있을 수 없습니다.");
        }

        if(metaData.GoalValue <= 0)
        {
            throw new Exception("업적 목표 값은 0보다 커야 합니다.");
        }

        if(metaData.RewardAmount <= 0)
        {
            throw new Exception("업적 보상 값은 0보다 커야 합니다.");
        }

        if(saveData != null && saveData.CurrentValue < 0)
        {
            throw new Exception("업적 진행 값은 0보다 작을 수 없습니다.");
        }

        ID = metaData.ID;
        Name = metaData. Name;
        Description = metaData.Description;
        Condition = metaData.Condition;
        GoalValue = metaData.GoalValue;
        RewardCurrencyType = metaData.RewardCurrencyType;
        RewardAmount = metaData.RewardAmount;

        if (saveData != null)
        {
            _currentValue = saveData.CurrentValue;
            _rewardClaimed = saveData.RewardClaimed;
        }
    }

    public void Increase(int value)
    {
        if(value <= 0)
        {
            throw new Exception("증가 값은 0보다 커야 합니다.");
        }

        _currentValue += value;
    }

    public bool CanClaimReward()
    {
        return _rewardClaimed == false && _currentValue >= GoalValue;
    }

    public bool TryClaimReward()
    {
        if (!CanClaimReward())
        {
            return false;
        }

        _rewardClaimed = true;
        return true;
    }
}
