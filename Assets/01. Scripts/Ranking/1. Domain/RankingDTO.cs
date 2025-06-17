using UnityEngine;

public class RankingDTO
{
    public readonly string Email;
    public readonly int KillCount;

    public RankingDTO(string email, int killCount)
    {
        Email = email;
        KillCount = killCount;
    }
}
