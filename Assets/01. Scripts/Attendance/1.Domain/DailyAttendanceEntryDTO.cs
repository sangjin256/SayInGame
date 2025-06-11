public class DailyAttendanceEntryDTO
{
    public readonly bool IsChecked;
    public readonly bool IsRewardClaimed;

    public DailyAttendanceEntryDTO(DailyAttendanceEntry entry)
    {
        IsChecked = entry.IsChecked;
        IsRewardClaimed = entry.IsRewardClaimed;
    }
}