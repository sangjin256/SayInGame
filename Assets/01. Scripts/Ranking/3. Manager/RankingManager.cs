using System.Collections.Generic;
using Unity.Tutorials.Core.Editor;
using UnityEngine;

public class RankingManager : BehaviourSingleton<RankingManager>
{
    private RankingRepository _repository;
    private RankingBoard _rankingBoard;

    private void Start()
    {
        Init();   
    }

    private void Init()
    {
        _repository = new RankingRepository();
        RankingBoardDTO loadedRankingBoard = _repository.Load();

        if(loadedRankingBoard == null)
        {
            _rankingBoard = new RankingBoard();
        }
        else
        {
            _rankingBoard = new RankingBoard(loadedRankingBoard);
        }
    }

    public void AddKillCount(int killCount)
    {
        string email = AccountManager.Instance.GetCurrentEmail();
        _rankingBoard.UpdateRank(email, killCount);

        _repository.Save(_rankingBoard.ToDTO());
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
