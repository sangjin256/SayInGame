using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;
using System.Collections.Generic;
using System;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    private void Start()
    {
        Init();
    }

    // 파이어베이스 내 프로젝트에 연결
    private void Init()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");
                _app = Firebase.FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();
            }
            else
            {
                Debug.LogError($"파이어베이스 연결에 실패했습니다. ${dependencyStatus}");
            }
        });
    }

    private void Register()
    {
        string email = "test@gmail.com";
        string password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"회원가입에 실패했습니다: {task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("회원가입에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);
            return;
        });
    }

    private void Login()
    {
        string email = "test@gmail.com";
        string password = "123456";
        
        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"로그인에 실패했습니다: {task.Exception.Message}");
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("로그인에 성공했습니다: {0} ({1})", result.User.DisplayName, result.User.UserId);

            NicknameChange();
            GetMyRanking();
        });
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        var profile = new UserProfile
        {
            DisplayName = "teemo"
        };

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("닉네임 변경에 실패했습니다.");
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        var  user = _auth.CurrentUser;
        if (user == null) return;

        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, "firebase");
    }

    private void AddMyRanking()
    {
        Ranking ranking = new Ranking("sangjin256@naver.com", "SLee", 6020);
        Dictionary<string, object> rankingDic = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.KillCount }
        };


        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDic).ContinueWithOnMainThread(task => {
            Debug.Log(String.Format("Added or Updated document with ID: {0}.", task.Id));
        });
    }

    private void GetMyRanking()
    {
        string email = "sangjin256@naver.com";

        DocumentReference docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                Dictionary<string, object> rankingDIc = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in rankingDIc)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }
}
