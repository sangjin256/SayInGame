using Unity.Tutorials.Core.Editor;
using UnityEngine;
using System;

public class Ranking
{
    public readonly string Email;
    public readonly string Nickname;
    private int _killCount;
    public int KillCount => _killCount;

    public Ranking(string email, string nickname, int killCount)
    {
        if (email.IsNullOrEmpty())
        {
            throw new Exception("이메일은 비어있을 수 없습니다.");
        }
        if (nickname.IsNullOrEmpty())
        {
            throw new Exception("닉네임은 비어있을 수 없습니다.");
        }
        if(killCount < 0)
        {
            throw new Exception("킬카운트는 음수일 수 없습니다.");
        }

        Email = email;
        Nickname = nickname;
        _killCount = killCount;
    }

    public void AddKillCount(int count)
    {
        if(count <= 0)
        {
            throw new Exception("카운트는 0 이하일 수 없습니다.");
        }

        _killCount += count;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(Email, Nickname, _killCount);
    }
}
