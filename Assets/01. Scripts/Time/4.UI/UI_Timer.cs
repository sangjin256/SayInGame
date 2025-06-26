using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    // 속성
    // - 타이머 텍스트
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI stageText;

    [SerializeField] private RectTransform _diffIndexTransform;
    [SerializeField] private float _playTime = 1500f;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Start()
    {
        _startPos = _diffIndexTransform.anchoredPosition;
        _endPos = _startPos + new Vector2(-_diffIndexTransform.rect.width, 0f);

        TimeManager.Instance.OnSeconds += TimerRefresh;
    }

    public void TimerRefresh()
    {
        float currentTime = TimeManager.Instance.TimeDTO.CurrentTime;
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
        float t = Mathf.Clamp01(TimeManager.Instance.TimeDTO.CurrentTime / _playTime);
        _diffIndexTransform.anchoredPosition = Vector2.Lerp(_startPos, _endPos, t);
    }

    public void StageRefresh()
    {
        // 스테이지 변화 시 적용
    }
}
