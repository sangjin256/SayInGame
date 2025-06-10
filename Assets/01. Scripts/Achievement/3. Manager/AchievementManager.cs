using System;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

public class AchievementManager : BehaviourSingleton<AchievementManager>
{
    private List<Achievement> _achievements;
    public List<AchievementDTO> Achievements => _achievements.ConvertAll( x => new AchievementDTO(x));

    [SerializeField]
    private List<AchievementSO> _metaDatas;

    public Action OnDataChanged;
    public Action<AchievementDTO> OnNewAchievementRewarded;

    private AchievementRepository _repository;

    private void Awake()
    {
        Init();

        EventManager.AddListener<AchievementEvent>(Increase);
    }

    public void Init()
    {
        _achievements = new List<Achievement>();
        _repository = new AchievementRepository();
        List<AchievementDTO> loadedAchievementList = _repository.Load();

        foreach (var metaData in _metaDatas)
        {
            Achievement duplicatedAchievement = FindByID(metaData.ID);
            if (duplicatedAchievement != null)
            {
                throw new Exception($"업적 ID{metaData.ID}가 중복됩니다.");
            }

            AchievementDTO saveData = loadedAchievementList?.Find(x => x.ID == metaData.ID);
            Achievement achievement = new Achievement(metaData, saveData);
            _achievements.Add(achievement);
        }
    }

    private Achievement FindByID(string id)
    {
        return _achievements.Find(x => x.ID == id);
    }

    public void Increase(AchievementEvent achieveEvent)
    {
        foreach (var achievement in _achievements)
        {
            if (achievement.Condition == achieveEvent.condition)
            {
                bool prevCanClaimReward = achievement.CanClaimReward();
                achievement.Increase(achieveEvent.value);
                bool canClaimReward = achievement.CanClaimReward();

                if(prevCanClaimReward == false && canClaimReward == true)
                {
                    OnNewAchievementRewarded?.Invoke(new AchievementDTO(achievement));
                }
            }
        }

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();
    }

    public bool TryClaimReward(AchievementDTO achieveDTO)
    {
        Achievement achievement = _achievements.Find(x => x.ID == achieveDTO.ID);
        if(achievement == null)
        {
            return false;
        }

        if (achievement.TryClaimReward())
        { 
            CurrencyManager.Instance.Add(achievement.RewardCurrencyType, achievement.RewardAmount);
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }
    private List<AchievementDTO> ToDTOList()
    {
        return _achievements.ConvertAll(x => new AchievementDTO(x));
    }
}
