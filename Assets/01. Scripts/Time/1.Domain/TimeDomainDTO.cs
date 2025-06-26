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
        if(time == null)
        {
            CurrentTime = 0;
            NextDifficultyChangeTime = 0;
        }
        else
        {
            CurrentTime = time.CurrentTime;
            NextDifficultyChangeTime = time.NextDifficultyChangeTime;
        }
    }
}
