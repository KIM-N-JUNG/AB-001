using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using Ab001.Util;
using Ab001.Database.Service;
using Ab001.Database.Dto;
using UnityEngine.UI;
using System.Threading;
using System.Collections.Generic;

public class MyRank : MonoBehaviour
{
    public Text rank;
    public Text nickName;
    public Text score;
    public Text time;
    public Text level;

    private RankInfo myRankInfo = new RankInfo();

    // Use this for initialization
    void Start()
    {
        // Use this for initialization
        Debug.Log("MyRank Start");
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.RANK_BOARD)
        {
            return;
        }

        LoadMyRank();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadMyRank()
    {
        List<Ab001Score> list = ScoreService.Instance.FindAllScoreByScoreDateInCurrentWeek(0);
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (list[i].user_id.Equals(MainMenu.userInfo.user_id))
            {
                break;
            }
        }

        if (i / 100000 >= 1)
        {
            rank.fontSize = 140;
        }

        if (i >= list.Count)
        {
            rank.text = "-";
            score.text = "";
            time.text = "";
            level.text = "";
        }
        else
        {
            rank.text = String.Format("{0:#,###}", i + 1);
            score.text = String.Format("{0:#,###}", list[i].score);
            time.text = String.Format("{0:#,#.##}", list[i].time) + "\"";
            level.text = Constant.LEVEL[list[i].level];
            level.color = Constant.COLOR[list[i].level];
        }
        nickName.text = MainMenu.userInfo.nick_name;
    }
}
