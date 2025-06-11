using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class AccountManager : BehaviourSingleton<AccountManager>
{
    private Account _myAccount;
    public AccountDTO CurrencAccount => _myAccount.ToDTO();

    private AccountRepository _accountRepository;
    private const string SALT = "12315";

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    private void Init()
    {
        _accountRepository = new AccountRepository();
    }

    public Result TryRegister(string email, string nickname, string password)
    {
        AccountDTO accountDTO = _accountRepository.Find(email);
        if(accountDTO != null)
        {
            return new Result(false, "이미 가입한 이메일입니다.");
        }

        // 비밀번호 규칙 검증

        string encryptedPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickname, encryptedPassword);
        _accountRepository.Save(account.ToDTO());

        return new Result(true);
    }

    public bool TryLogin(string email, string password)
    {
        AccountDTO accountDTO = _accountRepository.Find(email);

        if (CryptoUtil.Verify(password, accountDTO.Password, SALT))
        {
            _myAccount = new Account(accountDTO.Email, accountDTO.NickName, accountDTO.Password);
            return true;
        }

        return false;
    }
}
