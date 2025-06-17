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

    public List<RankingDTO> GetRankList(int count)
    {

    }
}
