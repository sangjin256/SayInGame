using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    public GameObject AttendanceSlotPrefab;
    public Transform SlotParent;
    public TextMeshProUGUI AccumulateAttendanceText;

    private List<UI_AttendanceSlot> _attendacneSlots;
    [SerializeField]
    private List<UI_AttendanceSlot> _accumulateAttendanceSlots;

    public void Awake()
    {
        AttendanceManager.Instance.OnDataLoaded += InitSlots;
        AttendanceManager.Instance.OnDataChanged += Refresh;
    }

    private void InitSlots()
    {
        AttendanceCalendarDTO attendance = AttendanceManager.Instance.Attendance;
        AttendanceSO so = AttendanceManager.Instance.currentData;
        _attendacneSlots = new List<UI_AttendanceSlot>();

        for (int i = 0; i < so.Attendances.Count; i++)
        {
            UI_AttendanceSlot slot = Instantiate(AttendanceSlotPrefab, SlotParent).GetComponent<UI_AttendanceSlot>();
            slot.Init(so.Attendances[i].Day, attendance, so, false);
            _attendacneSlots.Add(slot);
        }

        for(int i = 0; i < so.AccumulateAttendances.Count; i++)
        {
            _accumulateAttendanceSlots[i].Init(so.AccumulateAttendances[i].Day, attendance, so, true);
        }

        AccumulateAttendanceText.text = $"현재 {attendance.AccumulatedAttendanceDay.ToString()}회 출석";

        gameObject.SetActive(false);
    }

    public void OnClickClaimAll()
    {
        for(int i = 0; i < _attendacneSlots.Count; i++)
        {
            AttendanceManager.Instance.TryClaimReward(_attendacneSlots[i].Day);
        }

        for(int i = 0; i < _accumulateAttendanceSlots.Count; i++)
        {
            AttendanceManager.Instance.TryClaimAccumulateReward(_accumulateAttendanceSlots[i].Day);
        }
    }

    private void Refresh()
    {
        AttendanceCalendarDTO attendance = AttendanceManager.Instance.Attendance;

        for (int i = 0; i < attendance.Entries.Count; i++)
        {
            _attendacneSlots[i].Refresh(attendance, false);
        }

        for(int i = 0; i < attendance.AccumulateEntries.Count; i++)
        {
            _accumulateAttendanceSlots[i].Refresh(attendance, true);
        }

        AccumulateAttendanceText.text = $"현재 {attendance.AccumulatedAttendanceDay.ToString()}회 출석";
    }
}
