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
        AccountDTO currentAccount = AccountManager.Instance.CurrencAccount;
        
        _rankingBoard.UpdateRank(currentAccount.Email, currentAccount.NickName, killCount);

        _repository.Save(_rankingBoard.ToDTO());
    }

    public List<RankingDTO> GetSortedRankList(int count)
    {
        return _rankingBoard.GetSortedRankList(count);
    }

    public int GetPlayerRankNumber()
    {
        AccountDTO currentAccount = AccountManager.Instance.CurrencAccount;
        
        return _rankingBoard.GetRankNumberByEmail(currentAccount.Email, currentAccount.NickName);
    }

    public RankingDTO GetPlayerRankData()
    {
        AccountDTO currentAccount = AccountManager.Instance.CurrencAccount;
        return _rankingBoard.GetRankDataByEmail(currentAccount.Email, currentAccount.NickName);
    }
}
