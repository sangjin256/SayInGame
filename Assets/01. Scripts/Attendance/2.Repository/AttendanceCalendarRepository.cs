using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DailyAttendanceEntrySaveData
{
    public int Day;
    public bool IsChecked;
    public bool IsRewardClaimed;
}

[Serializable]
public class AttendanceCalendarSaveData
{
    public string Email;
    public string LastAttendanceDate;
    public int AccumulatedAttendanceDay;
    public SaveDatas<DailyAttendanceEntrySaveData> DailyAttendanceEntries;
    
    public AttendanceCalendarSaveData()
    {
        DailyAttendanceEntries = new SaveDatas<DailyAttendanceEntrySaveData>();
    }
}

public class AttendanceCalendarRepository
{
    private const string SAVE_KEY = nameof(AttendanceCalendarRepository);

    private string GetKey(string email)
    {
        return $"{SAVE_KEY}_{email}";
    }
    
    public void Save(AttendanceCalendarDTO attendanceCalendarDTO)
    {
        var saveData = attendanceCalendarDTO.ToSaveData();
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(GetKey(attendanceCalendarDTO.Email), json);
    }

    public AttendanceCalendarDTO Load(string email)
    {
        string key = GetKey(email);

        if (!PlayerPrefs.HasKey(key))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(key);
        var saveDataLoaded = JsonUtility.FromJson<AttendanceCalendarSaveData>(json);
        return new AttendanceCalendarDTO(saveDataLoaded);
    }
}
