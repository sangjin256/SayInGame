using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class AttendanceManager : BehaviourSingleton<AttendanceManager>
{
    private AttendanceCalendar _attendanceCalendar;
    public AttendanceCalendarDTO Attendance => new AttendanceCalendarDTO(_attendanceCalendar);
    // �������丮 �߰�
    private AttendanceCalendarRepository _repository;
    [SerializeField]
    private List<AttendanceSO> _metaDatas;
    public AttendanceSO currentData;

    public Action OnDataChanged;
    public Action OnDataLoaded;

    public void Start()
    {
        Init();
        Attend();
        OnDataChanged?.Invoke();
    }

    public void Init()
    {
        DateTime now = DateTime.Now;
        int currentMonth = now.Month;
        currentData = _metaDatas.Find(x => x.Month == now.Month);
        if(currentData == null)
        {
            throw new Exception("SO �����Ͱ� �����ϴ�.");
        }

        string email = AccountManager.Instance.CurrencAccount?.Email;
        if (email.IsNullOrEmpty())
        {
            throw new Exception("�α��� �����Ͱ� �����ϴ�.");
        }

        _repository = new AttendanceCalendarRepository();
        AttendanceCalendarDTO loadedData = _repository.Load(email);
        if (loadedData == null)
        {
            _attendanceCalendar = new AttendanceCalendar(email, currentData.Attendances.Count);
        }
        else
        {
            Dictionary<int, DailyAttendanceEntry> entries = loadedData.Entries.ToDictionary(x => x.Key, x => new DailyAttendanceEntry(x.Value.IsChecked, x.Value.IsRewardClaimed));
            _attendanceCalendar = new AttendanceCalendar(email, loadedData.LastAttendanceDate, loadedData.AccumulatedAttendanceDay, entries);
        }

        OnDataLoaded?.Invoke();
    }

    public void Attend()
    {
        _attendanceCalendar.Attendance(DateTime.Today);
    }
    
    public bool TryClaimReward(int day)
    {
        if (_attendanceCalendar.TryClaimReward(day))
        {
            AttendanceInfo attendanceInfo = currentData.Attendances[day - 1];
            CurrencyManager.Instance.Add(attendanceInfo.Type, attendanceInfo.Value);
            OnDataChanged?.Invoke();
            return true;
        }

        return false;
    }
}
