using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checker
{
    public static void checkSceneAndLoginAndInternetStatus(int sceneNumber)
    {
        if (SceneManager.GetActiveScene().buildIndex != sceneNumber)
        {
            throw new NotReachableSceneException("it's not my scene:" + sceneNumber);
        }
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            throw new NotReachableInternetException("not reachable");
        }
        // 로그인이 안되어있을 때 종료
        if (SingletonClass.Instance.bLogin == false)
        {
            throw new NotLoginException("not login");
        }
    }
}
