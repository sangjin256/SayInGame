using UnityEngine;

public class TimeDomain
{
    private float _currentTime;
    public float CurrentTime => _currentTime;
    private float _nextDifficultyChangeTime;
    public float NextDifficultyChangeTime => _nextDifficultyChangeTime;

    public TimeDomain(float nextDifficultyChangeTime)
    {
        _currentTime = 0f;
        _nextDifficultyChangeTime = nextDifficultyChangeTime;
    }

    public TimeDomain(float currentTime, float nextDifficultyChangeTime)
    {
        _currentTime = currentTime;
        _nextDifficultyChangeTime = nextDifficultyChangeTime;
    }

    public void ResetTime()
    {
        _currentTime = 0;
    }

    public void AddTime(float time)
    {
        _currentTime += time;
    }

    public bool CheckDifficultyChange()
    {
        if (_currentTime > _nextDifficultyChangeTime) return true;
        return false;
    }
}
