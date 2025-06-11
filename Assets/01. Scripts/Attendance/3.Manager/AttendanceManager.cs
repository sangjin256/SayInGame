using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class AttendanceManager : BehaviourSingleton<AttendanceManager>
{
    private AttendanceCalendar _attendanceCalendar;
    public AttendanceCalendarDTO Attendance => new AttendanceCalendarDTO(_attendanceCalendar);
    // 레포지토리 추가
    //private AttendanceRepository _repository;
    [SerializeField]
    private List<AttendanceSO> _metaDatas;
    public AttendanceSO currentData;

    public Action OnDataChanged;
    public Action OnDataLoaded;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        DateTime now = DateTime.Now;
        int currentMonth = now.Month;
        currentData = _metaDatas.Find(x => x.Month == now.Month);
        if(currentData == null)
        {
            throw new Exception("SO 데이터가 없습니다.");
        }

        string email = AccountManager.Instance.CurrencAccount?.Email;
        if (email.IsNullOrEmpty())
        {
            throw new Exception("로그인 데이터가 없습니다.");
        }

        _repository = new AttendanceRepository();
        AttendanceCalendarDTO loadedData = _repository.Load();
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
}
