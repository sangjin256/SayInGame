using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RankingRepository
{
    private const string SAVE_KEY = nameof(RankingRepository);

    public void Save(RankingBoardDTO rankingBoardDTO)
    {
        var saveData = new SaveDatas<RankingSaveData>();
        saveData.DataList.Add(new RankingSaveData(rankingBoardDTO.RankDic));
        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(SAVE_KEY, json);
    }

    public RankingBoardDTO Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(SAVE_KEY);
        var saveDataLoaded = JsonUtility.FromJson<SaveDatas<RankingSaveData>>(json);
        return new RankingBoardDTO(saveDataLoaded.DataList[0].RankDic);
    }
}

[Serializable]
public struct RankingSaveData
{
    public Dictionary<string, RankingDTO> RankDic;
    public RankingSaveData(Dictionary<string, RankingDTO> rankDic)
    {
        RankDic = rankDic;
    }
}