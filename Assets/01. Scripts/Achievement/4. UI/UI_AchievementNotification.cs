using TMPro;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    public TextMeshProUGUI RewardCountTextUI;
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;

    public void Refresh(AchievementDTO achievement)
    {
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        DescriptionTextUI.text = achievement.Description;
        NameTextUI.text = achievement.Name;
    }
}
