using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    // 속성
    // - 타이머 텍스트
    [SerializeField] private TextMeshProUGUI timerText;

    void Update()
    {
        //float currentTime = TimeManager.Instance.GetTime();
        //int minutes = Mathf.FloorToInt(currentTime / 60f);
        //int seconds = Mathf.FloorToInt(currentTime % 60f);

        //timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
