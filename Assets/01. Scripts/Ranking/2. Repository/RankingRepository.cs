using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RankingRepository
{
    private const string SAVE_KEY = nameof(RankingRepository);

    public void Save(RankingBoardDTO rankingBoardDTO)
    {
        var saveData = new RankingBoardSaveData(rankingBoardDTO.RankDic);
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
        var saveDataLoaded = JsonUtility.FromJson<RankingBoardSaveData>(json);

        // 변환
        var rankDic = saveDataLoaded.ToDTO();

        return new RankingBoardDTO(rankDic);
    }
}

[Serializable]
public struct RankingBoardSaveData
{
    public List<RankingSaveData> RankList;

    public RankingBoardSaveData(Dictionary<string, RankingDTO> rankDic)
    {
        RankList = new List<RankingSaveData>();
        foreach (var pair in rankDic)
        {
            RankList.Add(new RankingSaveData(pair.Value));
        }
    }

    // 역변환용 메서드
    public Dictionary<string, RankingDTO> ToDTO()
    {
        var result = new Dictionary<string, RankingDTO>();
        foreach (var rank in RankList)
        {
            result[rank.Email] = rank.ToDTO();
        }
        return result;
    }
}

[Serializable]
public struct RankingSaveData
{
    public string Email;
    public string Nickname;
    public int KillCount;

    public RankingSaveData(RankingDTO dto)
    {
        Email = dto.Email;
        Nickname = dto.Nickname;
        KillCount = dto.KillCount;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(Email, Nickname, KillCount);
    }
}