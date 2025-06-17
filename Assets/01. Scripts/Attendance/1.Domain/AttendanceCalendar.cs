using System;
using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class AttendanceCalendar
{
    public string Email;
    
    private string _lastAttendanceDate;
    public string LastAttendanceDate => _lastAttendanceDate;
    
    private int _accumulatedAttendanceDay;
    public int AccumulatedAttendanceDay => _accumulatedAttendanceDay;
    
    private Dictionary<int, AttendanceEntry> _entries;
    public IReadOnlyDictionary<int, AttendanceEntry> Entries => _entries;

    private Dictionary<int, AttendanceEntry> _accumulateEntries;
    public IReadOnlyDictionary<int, AttendanceEntry> AccumulateEntries => _accumulateEntries;



    public AttendanceCalendar(string email, int maximumAttendanceDay, List<int> AccumulateDays)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("이메일은 비어있을 수 없습니다.");
        }

        if(maximumAttendanceDay <= 0)
        {
            throw new Exception("최대 출석일은 0 이하일 수 없습니다.");
        }

        if (AccumulateDays == null)
        {
            throw new Exception("누적 출석일은 비어있을 수 없습니다.");
        }

        Email = email;
        _lastAttendanceDate = DateTime.MinValue.Date.ToString();
        _accumulatedAttendanceDay = 0;
        _entries = new Dictionary<int, AttendanceEntry>();
        _accumulateEntries = new Dictionary<int, AttendanceEntry>();

        for (int i = 1; i <= maximumAttendanceDay; i++)
        {
            _entries.Add(i, new AttendanceEntry());
        }

        for(int i = 0; i < AccumulateDays.Count; i++)
        {
            _accumulateEntries.Add(AccumulateDays[i], new AttendanceEntry());
        }
    }

    public AttendanceCalendar(string email, string lastAttendanceDate, int accumulatedAttendanceDay, Dictionary<int, AttendanceEntry> entries, Dictionary<int, AttendanceEntry> accumulateEntries)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("이메일은 비어있을 수 없습니다.");
        }

        if (lastAttendanceDate.IsNullOrEmpty())
        {
            throw new Exception("최근 출석일은 비어있을 수 없습니다.");
        }

        if (accumulatedAttendanceDay < 0)
        {
            throw new Exception("누적 출석일은 0보다 작을 수 없습니다.");
        }

        Email = email;
        _lastAttendanceDate = lastAttendanceDate;
        _accumulatedAttendanceDay = accumulatedAttendanceDay;
        _entries = entries;
        _accumulateEntries = accumulateEntries;
    }
    
    public bool TryAttendance(DateTime current)
    {
        if (current == new DateTime())
        {
            throw new Exception("출석 체크하는 date가 지정되지 않았습니다.");
        }

        DateTime lastAttendanceDate = DateTime.Parse(_lastAttendanceDate);
        if (lastAttendanceDate < current.Date)
        {
            _lastAttendanceDate = current.Date.ToString();
            ++_accumulatedAttendanceDay;

            foreach (var attendance in AccumulateEntries)
            {
                if (attendance.Key < _accumulatedAttendanceDay)
                {
                    attendance.Value.Attendance();
                }
            }

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
        if(day <= 0)
        {
            throw new Exception("day는 0 이하일 수 없습니다.");
        }

        if (_entries.TryGetValue(day, out var entry))
        {
            return entry.TryClaimReward();
        }
        else
        {
            throw new Exception($"No attendance entry found for day {day}.");
        }
    }

    public bool TryClaimAccumulateReward(int day)
    {
        if (day <= 0)
        {
            throw new Exception("day는 0 이하일 수 없습니다.");
        }

        if (_accumulateEntries.TryGetValue(day, out var entry))
        {
            return entry.TryClaimReward();
        }
        else
        {
            throw new Exception($"No attendance entry found for day {day}.");
        }
    }
}
