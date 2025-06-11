using UnityEngine;
using System;
using System.Text.RegularExpressions;
using Unity.Collections;

public class Account
{
    public readonly string Email;
    public readonly string NickName;
    public readonly string Password;

    public Account(string email, string nickName, string password)
    {
        // 규칙을 객체로 캡슐화해서 분리한다.
        // 그래서 도메인과 UI는 모두 "이 규칙을 만족하니?" 물으면 된다.
        // 캡슐화한 규칙 : 명세
        if(CheckSpecification(email, nickName, password))
        {
            Email = email;
            NickName = nickName;
            Password = password;
        }
    }

    private bool CheckSpecification(string email, string nickName, string password)
    {
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickName))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        return true;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, NickName, Password);
    }
}