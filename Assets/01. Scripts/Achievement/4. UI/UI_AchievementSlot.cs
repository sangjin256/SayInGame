using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UI_AchievementSlot : MonoBehaviour
{
    public TextMeshProUGUI NameTextUI;
    public TextMeshProUGUI DescriptionTextUI;
    public TextMeshProUGUI RewardCountTextUI;
    public Button RewardDClaimButton;
    public TextMeshProUGUI ProgressTextUI;
    public Slider ProgressSlider;
    public TextMeshProUGUI RewardClaimDateUI;

    private AchievementDTO _achievementDTO;

    public void Refresh(AchievementDTO achievement)
    { 
        _achievementDTO = achievement;
        NameTextUI.text = achievement.Name;
        DescriptionTextUI.text = achievement.Description;
        RewardCountTextUI.text = achievement.RewardAmount.ToString();
        ProgressSlider.value = (float)achievement.CurrentValue / achievement.GoalValue;
        ProgressTextUI.text = $"{achievement.CurrentValue} / {achievement.GoalValue}";

        RewardDClaimButton.interactable = achievement.CanClaimReward();
    }

    public void ClaimReward()
    {
        if (AchievementManager.Instance.TryClaimReward(_achievementDTO))
        {
            // ÀÌÆåÆ®
        }
        else
        {
            // ÁøÇàµµ ºÎÁ· ÆË¾÷
        }
    }
}
