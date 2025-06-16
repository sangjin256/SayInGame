using System;

public class AttendanceEntry
{
    public bool IsChecked { get; private set; }
    public bool IsRewardClaimed { get; private set; }

    public AttendanceEntry()
    {
        IsChecked = false;
        IsRewardClaimed = false;
    }
    
    public AttendanceEntry(bool isChecked, bool isRewardClaimed)
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
