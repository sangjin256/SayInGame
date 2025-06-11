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
    private List<UI_AttendanceSlot> _accumulatedAttendacneSlots;

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
    }

    private void Refresh()
    {
        //List<AttendanceDTO> attendances = AttendanceManager.Instance.Attendances;

        //for (int i = 0; i < attendances.Count; i++)
        //{
        //    _slots[i].Refresh(attendances[i]);
        //}

        // AccumulateAttendanceText.text = 
    }
}
