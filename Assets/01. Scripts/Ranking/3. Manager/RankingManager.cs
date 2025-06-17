using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class RankingManager : BehaviourSingleton<RankingManager>
{
    //private RankingBoardRepository _repository;
    private RankingBoard _rankingBoard;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {
        //_repository = new RankingBoardRepository();
        // ·Îµå
        _rankingBoard = new RankingBoard();
    }

    public void AddKillCount(int killCount)
    {
        string email = AccountManager.Instance.GetCurrentEmail();
        _rankingBoard.UpdateRank(email, killCount);
    }

    public List<RankingDTO> GetSortedRankList(int count)
    {
        return _rankingBoard.GetSortedRankList(count);
    }

    public int GetPlayerRankNumber()
    {
        string email = AccountManager.Instance.GetCurrentEmail();
        return _rankingBoard.GetRankNumberByEmail(email);
    }

    public RankingDTO GetPlayerRankData()
    {
        string email = AccountManager.Instance.GetCurrentEmail();
        return _rankingBoard.GetRankDataByEmail(email);
    }
}
