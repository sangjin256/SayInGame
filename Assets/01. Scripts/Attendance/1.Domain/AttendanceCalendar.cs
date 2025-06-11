using System;
using System.Collections.Generic;

public class AttendanceCalendar
{
    public string Email;
    
    private DateTime _lastAttendanceDate;
    public DateTime LastAttendanceDate => _lastAttendanceDate;
    
    private int _accumulatedAttendanceDay;
    public int AccumulatedAttendanceDay => _accumulatedAttendanceDay;
    
    private Dictionary<int, DailyAttendanceEntry> _entries;
    public IReadOnlyDictionary<int, DailyAttendanceEntry> Entries => _entries;

    public AttendanceCalendar(string email, int maximumAttendanceDay)
    {
        Email = email;
        _lastAttendanceDate = DateTime.MinValue.Date;
        _accumulatedAttendanceDay = 0;
        _entries = new Dictionary<int, DailyAttendanceEntry>();
        for (int i = 1; i <= maximumAttendanceDay; i++)
        {
            _entries.Add(i, new DailyAttendanceEntry());
        }
    }

    public AttendanceCalendar(string email, DateTime lastAttendanceDate, int accumulatedAttendanceDay, Dictionary<int, DailyAttendanceEntry> entries)
    {
        Email = email;
        _lastAttendanceDate = lastAttendanceDate;
        _accumulatedAttendanceDay = accumulatedAttendanceDay;
        _entries = entries;
    }
    
    public void Attendance(DateTime current)
    {
        if (_lastAttendanceDate < current.Date)
        {
            _lastAttendanceDate = current.Date;
            ++_accumulatedAttendanceDay;
            if (_accumulatedAttendanceDay < _entries.Count)
            {
                _entries[_accumulatedAttendanceDay].Attendance();
            }
        }
        else
        {
            throw new Exception("Cannot attend: already attended today.");
        }
    }
}
