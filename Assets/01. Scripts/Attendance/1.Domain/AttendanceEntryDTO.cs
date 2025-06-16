public class AttendanceEntryDTO
{
    public readonly bool IsChecked;
    public readonly bool IsRewardClaimed;

    public AttendanceEntryDTO(bool isChecked = false, bool isRewardClaimed = false)
    {
        IsChecked = isChecked;
        IsRewardClaimed = isRewardClaimed;
    }
    
    public AttendanceEntryDTO(AttendanceEntry entry)
    {
        IsChecked = entry.IsChecked;
        IsRewardClaimed = entry.IsRewardClaimed;
    }
    
    public AttendanceEntryDTO(AttendanceEntrySaveData saveData)
    {
        IsChecked = saveData.IsChecked;
        IsRewardClaimed = saveData.IsRewardClaimed;
    }

    public AttendanceEntrySaveData ToSaveData()
    {
        return new AttendanceEntrySaveData
        {
            IsChecked = this.IsChecked,
            IsRewardClaimed = this.IsRewardClaimed
        };
    }
}