using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardMgr : MonoBehaviour
{
    public AndroidSet androidSet;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("LeaderBoardMgr.Start()");
    }

    public void ShowLeaderboard()
    {
        Debug.Log("LeaderBoardMgr.ShowLeaderboard()");
        if (SingletonClass.Instance.bLogin == true)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            androidSet.ShowToast("먼저 로그인을 해주세요", true);
        }
    }
}
