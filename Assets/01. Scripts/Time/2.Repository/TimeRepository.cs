using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeRepository
{
    private const string SAVE_KEY = nameof(TimeRepository);

    public void Save(TimeDomainDTO data)
    {
        TimeSaveData saveData = new TimeSaveData();
        saveData = new TimeSaveData
        {
            CurrentTime = data.CurrentTime,
            NextDifficultyChangeTime = data.NextDifficultyChangeTime
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public TimeDomainDTO Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        TimeSaveData data = JsonUtility.FromJson<TimeSaveData>(json);

        return new TimeDomainDTO(data.CurrentTime, data.NextDifficultyChangeTime);
    }
}

[Serializable]
public struct TimeSaveData
{
    public float CurrentTime;
    public float NextDifficultyChangeTime;
}
