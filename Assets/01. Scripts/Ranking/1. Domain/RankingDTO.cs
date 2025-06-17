using UnityEngine;

public class RankingDTO
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly int KillCount;

    public RankingDTO(string email, string nickname, int killCount)
    {
        Email = email;
        Nickname = nickname;
        KillCount = killCount;
    }
}
