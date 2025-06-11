using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "AttendanceSO", menuName = "Scriptable Objects/AttendanceSO")]
public class AttendanceSO : ScriptableObject
{
    public int Month;
    public List<AttendanceInfo> Attendances;
    public List<AttendanceInfo> AccumulateAttendances;
}

[Serializable]
public struct AttendanceInfo
{
    public int Day;
    public ECurrencyType Type;
    public int Value;
}
