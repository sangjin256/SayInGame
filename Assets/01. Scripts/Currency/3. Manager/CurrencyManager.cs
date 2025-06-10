using System;
using System.Collections.Generic;
using System.Linq;
using Unity.FPS.Game;
using UnityEngine;

// 아키텍처 : 설계 그 잡채(설계마다 철학이 있다.)
// 디자인 패턴 : 설계를 구현하는 과정에서 쓰이는 패턴

public class CurrencyManager : BehaviourSingleton<CurrencyManager>
{
    private Dictionary<ECurrencyType, Currency> _currenyDic;

    // 도메인에 변화가 있을 때 호출되는 액션
    public Action OnDataChanged;

    private CurrencyRepository _repository;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _repository = new CurrencyRepository();

        List<CurrencyDTO> loadedCurrencyList = _repository.Load();
        _currenyDic = new();
        if(loadedCurrencyList == null)
        {
            _currenyDic = new();
            for (int i = 0; i < (int)ECurrencyType.Count; i++)
            {
                ECurrencyType type = (ECurrencyType)i;
                // 골드, 다이아몬드 등을 0 값으로 생성
                Currency curreny = new Currency(type, 0);
                _currenyDic.Add(type, curreny);
            }
        }
        else
        {
            foreach (CurrencyDTO data in loadedCurrencyList)
            {
                Currency currency = new Currency(data.Type, data.Value);
                _currenyDic.Add(currency.Type, currency);
            }
        }

    }

    private List<CurrencyDTO> ToDTOList()
    {
        return _currenyDic.ToList().ConvertAll(x => new CurrencyDTO(x.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currenyDic[type]);
    }

    // 도메인 유효성 검사는 도메인에 있어야지, 매니저에 있으면 안된다.
    public void Add(ECurrencyType type, int value)
    {
        _currenyDic[type].Add(value);

        AchievementEvent achieveEvent = Events.AchievementEvent;
        achieveEvent.condition = EAchievementCondition.GoldCollect;
        achieveEvent.value = value;
        EventManager.Broadcast(achieveEvent);

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();
    }

    public bool TryBuy(ECurrencyType type, int value)
    {
        if (_currenyDic[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDTOList());
        OnDataChanged?.Invoke();
        return true;
    }
}
