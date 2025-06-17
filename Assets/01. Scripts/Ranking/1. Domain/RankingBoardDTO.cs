using System.Collections.Generic;
using UnityEngine;

public class RankingBoardDTO
{
    public readonly Dictionary<string, RankingDTO> RankDic;

    public RankingBoardDTO(Dictionary<string, RankingDTO> rankDic)
    {
        RankDic = rankDic;
    }
}
