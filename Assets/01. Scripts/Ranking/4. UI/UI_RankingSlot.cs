using UnityEngine;
using TMPro;

public class UI_RankingSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void RefreshRankingSlot(int rank, string name, int score)
    {
        rankText.text = rank.ToString("N1");
        nameText.text = name;
        scoreText.text = score.ToString("N1");
    }
}
