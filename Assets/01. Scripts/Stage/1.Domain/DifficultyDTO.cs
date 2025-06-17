public class DifficultyDTO
{
    public int TID; //TID
    public int NextDifficultyStartTime; // 다음 난이도 시간(초)
    public int DifficultyLevel; // 난이도
    public string DifficultyText; // 난이도 설명: 쉬움, 보통, 어려움...
    public float EnemyCountMultiplier; // 몹 수 배수
    public float EnemyHealthMultiplier; // 몹 체력 배수
    public float EnemyDamageMultiplier; // 몹 데미지 배수
    public float EliteSpawnRate; // 엘리트 스폰 확률
    public float EnemySpawnFrequency; // 몹 스폰 빈도

    public DifficultyDTO(Difficulty difficulty)
    {
        TID = difficulty.TID;
        NextDifficultyStartTime = difficulty.NextDifficultyStartTime;
        DifficultyLevel = difficulty.DifficultyLevel;
        DifficultyText = difficulty.DifficultyText;
        EnemyCountMultiplier = difficulty.EnemyCountMultiplier;
        EnemyHealthMultiplier = difficulty.EnemyHealthMultiplier;
        EnemyDamageMultiplier = difficulty.EnemyDamageMultiplier;
        EliteSpawnRate = difficulty.EliteSpawnRate;
        EnemySpawnFrequency = difficulty.EnemySpawnFrequency;
    }
}