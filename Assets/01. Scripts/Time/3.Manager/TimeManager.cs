using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : BehaviourSingleton<TimeManager>
{
    private TimeDomain _time;
    private TimeRepository _repository;
    private float _nextSecondTick = 1f;
    private bool _isLoaded;

    public TimeDomainDTO TimeDTO => new TimeDomainDTO(_time);
    public Action OnDifficultyChanged;
    public Action OnSeconds;

    private void Start()
    {
        Global.Instance.OnDataLoaded += Init;
    }

    public void Init()
    {
        _repository = new TimeRepository();

        TimeDomainDTO loadedTimeDTO = _repository.Load();
        if(loadedTimeDTO == null)
        {
            _time = new TimeDomain(DataTable.Instance.GetTimeData(10000).NextTime);
        }
        else
        {
            _time = new TimeDomain(loadedTimeDTO.CurrentTime, loadedTimeDTO.NextDifficultyChangeTime);
        }

        _isLoaded = true;
    }

    public void Update()
    {
        if (_isLoaded)
        {
            _time.AddTime(Time.deltaTime);

            if (_time.CurrentTime >= _nextSecondTick)
            {
                OnSeconds?.Invoke();
                _nextSecondTick = _time.CurrentTime + 1f;
            }


            if (_time.CheckDifficultyChange())
            {
                OnDifficultyChanged?.Invoke();
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                _time.AddTime(59f);
            }
        }
    }
}
