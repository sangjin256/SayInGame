using System;
using System.Collections.Generic;
using UnityEngine;

public class AttendanceCalendar
{
    public string Email;
    
    private string _lastAttendanceDate;
    public string LastAttendanceDate => _lastAttendanceDate;
    
    private int _accumulatedAttendanceDay;
    public int AccumulatedAttendanceDay => _accumulatedAttendanceDay;
    
    private Dictionary<int, DailyAttendanceEntry> _entries;
    public IReadOnlyDictionary<int, DailyAttendanceEntry> Entries => _entries;

    public AttendanceCalendar(string email, int maximumAttendanceDay)
    {
        Email = email;
        _lastAttendanceDate = DateTime.MinValue.Date.ToString();
        _accumulatedAttendanceDay = 0;
        _entries = new Dictionary<int, DailyAttendanceEntry>();
        for (int i = 1; i <= maximumAttendanceDay; i++)
        {
            _entries.Add(i, new DailyAttendanceEntry());
        }
    }

    public AttendanceCalendar(string email, string lastAttendanceDate, int accumulatedAttendanceDay, Dictionary<int, DailyAttendanceEntry> entries)
    {
        Email = email;
        _lastAttendanceDate = lastAttendanceDate;
        _accumulatedAttendanceDay = accumulatedAttendanceDay;
        _entries = entries;
    }
    
    public bool TryAttendance(DateTime current)
    {
        DateTime lastAttendanceDate = DateTime.Parse(_lastAttendanceDate);
        if (lastAttendanceDate < current.Date)
        {
            Debug.Log(_lastAttendanceDate);
            Debug.Log(current);
            _lastAttendanceDate = current.Date.ToString();
            ++_accumulatedAttendanceDay;
            if (_accumulatedAttendanceDay <= _entries.Count)
            {
                _entries[_accumulatedAttendanceDay].Attendance();
                return true;
            }
        }

        return false;
    }
    
    public bool TryClaimReward(int day)
    {
        if (_entries.TryGetValue(day, out var entry))
        {
            return entry.TryClaimReward();
        }
        else
        {
            throw new Exception($"No attendance entry found for day {day}.");
        }

        return false;
    }
}
