using UnityEngine;

public class AccountRepository
{
    public const string SAVE_PREFIX = "ACCOUNT_";

    public void Save(AccountDTO accountDTO)
    {
        AccountSaveData data = new AccountSaveData(accountDTO);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_PREFIX + data.Email, json);
    }

    public AccountDTO Find(string email)
    {
        if(!PlayerPrefs.HasKey(SAVE_PREFIX + email))
        {
            return null;
        }

        AccountSaveData data = JsonUtility.FromJson<AccountSaveData>(PlayerPrefs.GetString(SAVE_PREFIX + email));
        return new AccountDTO(data.Email, data.NickName, data.Password);
    }
}

public class AccountSaveData
{
    public string Email;
    public string NickName;
    public string Password;

    public AccountSaveData(AccountDTO account)
    {
        Email = account.Email;
        NickName = account.NickName;
        Password = account.Password;
    }
}