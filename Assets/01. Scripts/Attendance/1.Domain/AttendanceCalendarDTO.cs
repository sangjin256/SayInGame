using System;
using System.Collections.Generic;

public class AttendanceCalendarDTO
{
    public readonly string Email;
    public readonly string LastAttendanceDate;
    public readonly int AccumulatedAttendanceDay;
    public readonly Dictionary<int, AttendanceEntryDTO> Entries;
    public readonly Dictionary<int, AttendanceEntryDTO> AccumulateEntries;

    public AttendanceCalendarDTO(AttendanceCalendar calendar)
    {
        Email = calendar.Email;
        LastAttendanceDate = calendar.LastAttendanceDate;
        AccumulatedAttendanceDay = calendar.AccumulatedAttendanceDay;
        Entries = new Dictionary<int, AttendanceEntryDTO>();
        AccumulateEntries = new Dictionary<int, AttendanceEntryDTO>();


        foreach (var entry in calendar.Entries)
        {
            Entries.Add(entry.Key, new AttendanceEntryDTO(entry.Value));
        }

        foreach(var entry in calendar.AccumulateEntries)
        {
            AccumulateEntries.Add(entry.Key, new AttendanceEntryDTO(entry.Value));
        }
    }
    
    public AttendanceCalendarDTO(AttendanceCalendarSaveData saveData)
    {
        Email = saveData.Email;
        LastAttendanceDate = saveData.LastAttendanceDate;
        AccumulatedAttendanceDay = saveData.AccumulatedAttendanceDay;
        Entries = new Dictionary<int, AttendanceEntryDTO>();
        AccumulateEntries = new Dictionary<int, AttendanceEntryDTO>();

        foreach (var item in saveData.DailyAttendanceEntries.DataList)
        {
            Entries[item.Day] = new AttendanceEntryDTO(item.IsChecked, item.IsRewardClaimed);
        }

        foreach(var item in saveData.AccumulateAttendanceEntries.DataList)
        {
            AccumulateEntries[item.Day] = new AttendanceEntryDTO(item.IsChecked, item.IsRewardClaimed);
        }
    }

    public AttendanceCalendarSaveData ToSaveData()
    {
        var saveData = new AttendanceCalendarSaveData
        {
            Email = Email,
            LastAttendanceDate = LastAttendanceDate,
            AccumulatedAttendanceDay = AccumulatedAttendanceDay,
            DailyAttendanceEntries = new SaveDatas<AttendanceEntrySaveData>(),
            AccumulateAttendanceEntries = new SaveDatas<AttendanceEntrySaveData>()
        };

        saveData.DailyAttendanceEntries.DataList = new List<AttendanceEntrySaveData>();
        saveData.AccumulateAttendanceEntries.DataList = new List<AttendanceEntrySaveData>();
        foreach (var kvp in Entries)
        {
            saveData.DailyAttendanceEntries.DataList.Add(new AttendanceEntrySaveData
            {
                Day = kvp.Key,
                IsChecked = kvp.Value.IsChecked,
                IsRewardClaimed = kvp.Value.IsRewardClaimed
            });
        }

        foreach(var kvp in AccumulateEntries)
        {
            saveData.AccumulateAttendanceEntries.DataList.Add(new AttendanceEntrySaveData
            {
                Day = kvp.Key,
                IsChecked = kvp.Value.IsChecked,
                IsRewardClaimed = kvp.Value.IsRewardClaimed
            });
        }

        return saveData;
    }
}