using UnityEngine;

public class TimeDomainDTO
{
    public readonly float CurrentTime;
    public readonly float NextDifficultyChangeTime;

    public TimeDomainDTO(float currentTime, float nextDifficultyChangeTime)
    {
        CurrentTime = currentTime;
        NextDifficultyChangeTime = nextDifficultyChangeTime;
    }

    public TimeDomainDTO(TimeDomain time)
    {
        CurrentTime = time.CurrentTime;
        NextDifficultyChangeTime = time.NextDifficultyChangeTime;
    }
}
