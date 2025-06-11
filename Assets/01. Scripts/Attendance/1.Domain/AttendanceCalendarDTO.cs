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
}