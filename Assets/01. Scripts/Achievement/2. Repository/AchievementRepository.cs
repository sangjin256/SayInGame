using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

public class AchievementRepository
{
    private const string SAVE_KEY = nameof(AchievementRepository);

    public void Save(List<AchievementDTO> dataList)
    {
        SaveDatas<AchievementSaveData> datas = new SaveDatas<AchievementSaveData>();
        datas.DataList = dataList.ConvertAll(data => new AchievementSaveData
        {
            ID = data.ID,
            CurrentValue = data.CurrentValue,
            RewardClaimed = data.RewardClaimed
        });

        string json = JsonUtility.ToJson(datas);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public List<AchievementDTO> Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }
        return null;
        string json = PlayerPrefs.GetString(SAVE_KEY);
        SaveDatas<AchievementSaveData> datas = JsonUtility.FromJson<SaveDatas<AchievementSaveData>>(json);

        return datas.DataList.ConvertAll(x => new AchievementDTO(x.ID, x.CurrentValue, x.RewardClaimed));
    }
}

[Serializable]
public struct AchievementSaveData
{
    public string ID;
    public int CurrentValue;
    public bool RewardClaimed;
}