using System;
using UnityEngine;

public class Difficulty
{
    public readonly int TID; //TID
    public readonly int NextDifficultyStartTime; // 다음 난이도 시간(초)
    public readonly int DifficultyLevel; // 난이도
    public readonly string DifficultyText; // 난이도 설명: 쉬움, 보통, 어려움...
    public readonly float EnemyCountMultiplier; // 몹 수 배수
    public readonly float EnemyHealthMultiplier; // 몹 체력 배수
    public readonly float EnemyDamageMultiplier; // 몹 데미지 배수
    public readonly float EliteSpawnRate; // 엘리트 스폰 확률
    public readonly float EnemySpawnFrequency; // 몹 스폰 빈도
    
    public Difficulty(int tid, int difficultyStartTime, int difficultyLevel, string difficultyText,
        float enemyCountMultiplier, float enemyHealthMultiplier, float enemyDamageMultiplier, float eliteSpawnRate, float enemySpawnFrequency)
    {
        // 예외 처리
        if (tid <= 0)
        {
            throw new Exception("TID는 0보다 커야 합니다.");
        }

        if (difficultyStartTime < 0)
        {
            throw new Exception("DifficultyStartTime은 0 이상이어야 합니다.");
        }

        if (difficultyLevel <= 0)
        {
            throw new Exception("DifficultyLevel은 0보다 커야 합니다.");
        }

        if (string.IsNullOrWhiteSpace(difficultyText))
        {
            throw new Exception("DifficultyText는 비어 있을 수 없습니다.");
        }

        if (enemyCountMultiplier <= 0)
        {
            throw new Exception("EnemyCountMultiplier는 0보다 커야 합니다.");
        }

        if (enemyHealthMultiplier <= 0)
        {
            throw new Exception("EnemyHealthMultiplier는 0보다 커야 합니다.");
        }

        if (enemyDamageMultiplier <= 0)
        {
            throw new Exception("EnemyDamageMultiplier는 0보다 커야 합니다.");
        }

        if (eliteSpawnRate < 0 || eliteSpawnRate > 1)
        {
            throw new Exception("EliteSpawnRate는 0 이상 1 이하이어야 합니다.");
        }

        if (enemySpawnFrequency <= 0)
        {
            throw new Exception("EnemySpawnFrequency는 0보다 커야 합니다.");
        }
        
        TID = tid;
        NextDifficultyStartTime = difficultyStartTime;
        DifficultyLevel = difficultyLevel;
        DifficultyText = difficultyText;
        EnemyCountMultiplier = enemyCountMultiplier;
        EnemyHealthMultiplier = enemyHealthMultiplier;
        EnemyDamageMultiplier = enemyDamageMultiplier;
        EliteSpawnRate = eliteSpawnRate;
        EnemySpawnFrequency = enemySpawnFrequency;
    }

    public DifficultyDTO ToDTO()
    {
        return new DifficultyDTO(this);
    }
}
