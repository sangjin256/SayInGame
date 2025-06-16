using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : BehaviourSingleton<TimeManager>
{
    private TimeDomain _time;
    private TimeRepository _repository;

    public TimeDomainDTO TimeDTO => new TimeDomainDTO(_time);
    public Action OnDifficultyChanged;

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
    }

    public void Update()
    {
        _time.AddTime(Time.deltaTime);

        if (_time.CheckDifficultyChange())
        {
            OnDifficultyChanged?.Invoke();
        }
    }
}
