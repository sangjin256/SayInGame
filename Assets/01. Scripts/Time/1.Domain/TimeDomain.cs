using UnityEngine;
using System;

public class TimeDomain
{
    private float _currentTime;
    public float CurrentTime => _currentTime;
    private float _nextDifficultyChangeTime;
    public float NextDifficultyChangeTime => _nextDifficultyChangeTime;

    public TimeDomain(float nextDifficultyChangeTime)
    {
        if(nextDifficultyChangeTime <= 0)
        {
            throw new Exception("다음 난이도 시간은 0 이하일 수 없습니다.");
        }

        _currentTime = 0f;
        _nextDifficultyChangeTime = nextDifficultyChangeTime;
    }

    public TimeDomain(float currentTime, float nextDifficultyChangeTime)
    {
        if (nextDifficultyChangeTime <= 0)
        {
            throw new Exception("다음 난이도 시간은 0 이하일 수 없습니다.");
        }

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

    public bool TryDifficultyChange(float nextDifficultyTime)
    {
        if (_currentTime > _nextDifficultyChangeTime)
        {
            _nextDifficultyChangeTime = nextDifficultyTime;
            return true;
        }
        return false;
    }
}
