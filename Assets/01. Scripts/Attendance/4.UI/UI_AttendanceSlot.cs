using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_AttendanceSlot : MonoBehaviour
{
    public TextMeshProUGUI AttendanceInfoText;
    public TextMeshProUGUI RewardInfoText;
    public TextMeshProUGUI RewardAmountText;
    public Button RewardButton;

    public void Init(int day, AttendanceCalendarDTO dto, AttendanceSO so)
    {
        if (dto.Entries[day - 1].IsRewardClaimed)
        {
            AttendanceInfoText.text = "È¹µæ ¿Ï·á";
            RewardButton.interactable = false;
        }
        else if (dto.Entries[day - 1].IsChecked)
        {
            AttendanceInfoText.text = "È¹µæ °¡´É";
            RewardButton.interactable = false;
        }
        else
        {
            AttendanceInfoText.text = $"{day}ÀÏÂ÷";
            RewardButton.interactable = true;
        }

        if (so.Attendances[day - 1].Type == ECurrencyType.Gold) RewardInfoText.text = "°ñµå";
        else if (so.Attendances[day - 1].Type == ECurrencyType.Diamond) RewardInfoText.text = "´ÙÀÌ¾Æ";

        RewardAmountText.text = so.Attendances[day - 1].Value.ToString();
    }

    public void Refresh(AttendanceCalendarDTO attendance)
    {
        //AttendanceInfoText.text = // ÀÏÀÚ or È¹µæ ¿Ï·á or È¹µæ °¡´É
    }
}
