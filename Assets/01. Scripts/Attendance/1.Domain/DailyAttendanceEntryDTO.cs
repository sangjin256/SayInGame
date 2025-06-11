public class DailyAttendanceEntryDTO
{
    public readonly bool IsChecked;
    public readonly bool IsRewardClaimed;

    public DailyAttendanceEntryDTO(DailyAttendanceEntry entry)
    {
        IsChecked = entry.IsChecked;
        IsRewardClaimed = entry.IsRewardClaimed;
    }
    
    public DailyAttendanceEntryDTO(DailyAttendanceEntrySaveData saveData)
    {
        IsChecked = saveData.IsChecked;
        IsRewardClaimed = saveData.IsRewardClaimed;
    }

    public DailyAttendanceEntrySaveData ToSaveData()
    {
        return new DailyAttendanceEntrySaveData
        {
            IsChecked = this.IsChecked,
            IsRewardClaimed = this.IsRewardClaimed
        };
    }
}