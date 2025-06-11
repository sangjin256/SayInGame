using System;

public class DailyAttendanceEntry
{
    public bool IsChecked { get; private set; }
    public bool IsRewardClaimed { get; private set; }

    public DailyAttendanceEntry()
    {
        IsChecked = false;
        IsRewardClaimed = false;
    }
    
    public DailyAttendanceEntry(bool isChecked, bool isRewardClaimed)
    {
        IsChecked = isChecked;
        IsRewardClaimed = isRewardClaimed;
    }

    public void Attendance()
    {
        IsChecked = true;
    }

    public void ClaimReward()
    {
        if (IsChecked && !IsRewardClaimed)
        {
            IsRewardClaimed = true;
        }
        else
        {
            throw new Exception("Cannot claim reward: either not checked in or already claimed.");
        }
    }
}
