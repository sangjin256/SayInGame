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
    //private 

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
            slot.Init(so.Attendances[i].Day, attendance, so);
            _attendacneSlots.Add(slot);
        }

        AccumulateAttendanceText.text = $"현재 {attendance.AccumulatedAttendanceDay.ToString()}회 출석";
    }

    private void Refresh()
    {
        AttendanceCalendarDTO attendance = AttendanceManager.Instance.Attendance;

        for (int i = 0; i < attendance.Entries.Count; i++)
        {
            _attendacneSlots[i].Refresh(attendance);
        }

        AccumulateAttendanceText.text = $"현재 {attendance.AccumulatedAttendanceDay.ToString()}회 출석";
    }
}
