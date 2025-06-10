using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;

public class UI_Achievement : MonoBehaviour
{
    public GameObject AchievementSlotPrefab;
    public Transform SlotParent;
    private List<UI_AchievementSlot> _slots;

    [SerializeField]
    private UI_AchievementNotification _notification;
    private RectTransform _notificationRectTransform;
    private Vector3 _notificationStartPosition;

    private void Start()
    {
        InitSlots();

        _notificationRectTransform = _notification.GetComponent<RectTransform>();
        _notificationStartPosition = _notificationRectTransform.anchoredPosition;

        AchievementManager.Instance.OnDataChanged += Refresh;
        AchievementManager.Instance.OnNewAchievementRewarded += ShowNotification;
    }

    private void InitSlots()
    {
        List<AchievementDTO> achievements = AchievementManager.Instance.Achievements;
        _slots = new List<UI_AchievementSlot>();

        for (int i = 0; i < achievements.Count; i++)
        {
            UI_AchievementSlot slot = Instantiate(AchievementSlotPrefab, SlotParent).GetComponent<UI_AchievementSlot>();
            slot.Refresh(achievements[i]);
            _slots.Add(slot);
        }
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
        _notification.Refresh(achievement);
        StopAllCoroutines();

        _notificationRectTransform.anchoredPosition = _notificationStartPosition;
        StartCoroutine(Notification_Coroutine());
    }

    private IEnumerator Notification_Coroutine()
    {
        _notification.gameObject.SetActive(true);
        yield return _notificationRectTransform.DOAnchorPos(new Vector2(_notificationRectTransform.anchoredPosition.x, 0), 0.4f).WaitForCompletion();
        yield return new WaitForSeconds(2f);
        yield return _notificationRectTransform.DOAnchorPos(_notificationStartPosition, 0.4f).WaitForCompletion();
        _notification.gameObject.SetActive(false);
    }
}
