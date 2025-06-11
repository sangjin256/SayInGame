using System;
using System.Collections.Generic;

public class AttendanceCalendarDTO
{
    public readonly string Email;
    public readonly DateTime LastAttendanceDate;
    public readonly int AccumulatedAttendanceDay;
    public readonly Dictionary<int, DailyAttendanceEntryDTO> Entries;
    
    public AttendanceCalendarDTO(AttendanceCalendar calendar)
    {
        Email = calendar.Email;
        LastAttendanceDate = calendar.LastAttendanceDate;
        AccumulatedAttendanceDay = calendar.AccumulatedAttendanceDay;
        Entries = new Dictionary<int, DailyAttendanceEntryDTO>();
        
        foreach (var entry in calendar.Entries)
        {
            Entries.Add(entry.Key, new DailyAttendanceEntryDTO(entry.Value));
        }
    }
    
    public AttendanceCalendarDTO(AttendanceCalendarSaveData saveData)
    {
        Email = saveData.Email;
        LastAttendanceDate = saveData.LastAttendanceDate;
        AccumulatedAttendanceDay = saveData.AccumulatedAttendanceDay;
        Entries = new Dictionary<int, DailyAttendanceEntryDTO>();

        foreach (var item in saveData.DailyAttendanceEntries.DataList)
        {
            Entries[item.Day] = new DailyAttendanceEntryDTO(item.IsChecked, item.IsRewardClaimed);
        }
    }

    public AttendanceCalendarSaveData ToSaveData()
    {
        var saveData = new AttendanceCalendarSaveData
        {
            Email = Email,
            LastAttendanceDate = LastAttendanceDate,
            AccumulatedAttendanceDay = AccumulatedAttendanceDay,
            DailyAttendanceEntries = new SaveDatas<DailyAttendanceEntrySaveData>()
        };

        foreach (var kvp in Entries)
        {
            saveData.DailyAttendanceEntries.DataList.Add(new DailyAttendanceEntrySaveData
            {
                Day = kvp.Key,
                IsChecked = kvp.Value.IsChecked,
                IsRewardClaimed = kvp.Value.IsRewardClaimed
            });
        }

        return saveData;
    }
}