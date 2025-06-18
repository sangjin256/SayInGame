using Firebase;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;

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
            }
            else
            {
                Debug.LogError($"파이어베이스 연결에 실패했습니다. ${dependencyStatus}");
            }
        });
    }
}
