using TMPro;
using UnityEngine;

public class UI_AchievementNotification : MonoBehaviour
{
    public TextMeshProUGUI RewardCountTextUI;
    public TextMeshProUGUI NameTextUI;

    public void Refresh(AchievementDTO achievement)
    {
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        NameTextUI.text = achievement.Name;
    }
}
