using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Tutorials.Core.Editor;
using UnityEngine;
using Unity.FPS.Game;

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

        List<int> accumulateDays = currentData.AccumulateAttendances?.Select(x => x.Day).ToList();
        if(accumulateDays == null)
        {
            throw new Exception("누적 출석 데이터가 없습니다.");
        }

        _repository = new AttendanceCalendarRepository();
        AttendanceCalendarDTO loadedData = _repository.Load(email);
        if (loadedData == null)
        {
            _attendanceCalendar = new AttendanceCalendar(email, currentData.Attendances.Count, accumulateDays);
        }
        else
        {
            Dictionary<int, AttendanceEntry> entries = loadedData.Entries.ToDictionary(x => x.Key, x => new AttendanceEntry(x.Value.IsChecked, x.Value.IsRewardClaimed));
            Dictionary<int, AttendanceEntry> accumulateEntries = loadedData.AccumulateEntries.ToDictionary(x => x.Key, x => new AttendanceEntry(x.Value.IsChecked, x.Value.IsRewardClaimed));
            _attendanceCalendar = new AttendanceCalendar(email, loadedData.LastAttendanceDate, loadedData.AccumulatedAttendanceDay, entries, accumulateEntries);
        }

        OnDataLoaded?.Invoke();
    }

    public void Attend()
    {
        if (_attendanceCalendar.TryAttendance(DateTime.Today))
        {
            AchievementEvent achieveEvent = Events.AchievementEvent;
            achieveEvent.condition = EAchievementCondition.AttendanceCount;
            achieveEvent.value = 1;
            EventManager.Broadcast(achieveEvent);

            _repository.Save(new AttendanceCalendarDTO(_attendanceCalendar));
            OnDataChanged?.Invoke();
        }
    }
    
    public bool TryClaimReward(int day)
    {
        if (_attendanceCalendar.TryClaimReward(day))
        {
            AttendanceInfo attendanceInfo = currentData.Attendances[day - 1];
            CurrencyManager.Instance.Add(attendanceInfo.Type, attendanceInfo.Value);
            OnDataChanged?.Invoke();
            _repository.Save(new AttendanceCalendarDTO(_attendanceCalendar));
            return true;
        }

        return false;
    }
}
