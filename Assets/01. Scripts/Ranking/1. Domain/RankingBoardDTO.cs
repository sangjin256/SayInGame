using System.Collections.Generic;
using UnityEngine;

public class RankingBoardDTO
{
    public readonly Dictionary<string, RankingDTO> RankDic;

    public RankingBoardDTO(Dictionary<string, RankingDTO> rankDic)
    {
        if(rankDic == null)
        {
            RankDic = new Dictionary<string, RankingDTO>();
        }
        else RankDic = rankDic;
    }
}
