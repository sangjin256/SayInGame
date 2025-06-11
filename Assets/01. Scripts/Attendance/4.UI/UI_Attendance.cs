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

    public void Start()
    {
        InitSlots();
    }

    private void InitSlots()
    {
        //List<AttendanceDTO> attendances = AttendanceManager.Instance.Attendances;
        //_slots = new List<UI_AttendanceSlot>();

        //for (int i = 0; i < attendances.Count; i++)
        //{
        //    UI_AttendanceSlot slot = Instantiate(AttendanceSlotPrefab, SlotParent).GetComponent<UI_AttendanceSlot>();
        //    slot.Init(attendances[i]);
        //    _slots.Add(slot);
        //}
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
