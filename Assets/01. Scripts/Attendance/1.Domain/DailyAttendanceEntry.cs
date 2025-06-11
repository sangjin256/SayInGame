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
        if (!IsChecked)
        {
            IsChecked = true;
        }
    }

    private bool CanClaimReward()
    {
        return IsChecked && !IsRewardClaimed;
    }

    public bool TryClaimReward()
    {
        if (CanClaimReward())
        {
            IsRewardClaimed = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
