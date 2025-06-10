using UnityEngine;
using System.Collections.Generic;

public class UI_Achievement : MonoBehaviour
{
    [SerializeField]
    private List<UI_AchievementSlot> _slots;

    [SerializeField]
    private UI_AchievementNotification _notification;

    private void Start()
    {
        Refresh();

        AchievementManager.Instance.OnDataChanged += Refresh;
        AchievementManager.Instance.OnNewAchievementRewarded += ShowNotification;
    }

    private void Refresh()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;

        for(int i = 0; i < achievements.Count; i++)
        {
            _slots[i].Refresh(achievements[i]);
        }
    }

    private void ShowNotification(AchievementDTO achievement)
    {

    }
}
