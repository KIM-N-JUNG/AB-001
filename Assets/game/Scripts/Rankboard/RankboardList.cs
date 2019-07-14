using UnityEngine;
using System.Collections;
using Ab001.Database.Dto;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using Ab001.Util;
using Ab001.Database.Service;
using System.Linq;

class RankInfo
{
	public string nick_name { get; set; }
	public Ab001Score score { get; set; }

	public override string ToString()
	{
		return String.Format("RankInfo[nick_name {0}, {1}]", nick_name, score.ToString());
	}
}

public class RankboardList : MonoBehaviour
{
	public GameObject rankItemObject;
    public Transform Content;

    List<RankInfo> rankInfoList = new List<RankInfo>();
    private string[] LEVEL = { "EASY", "NORMAL", "HARD", "CRAZY" };

    // Use this for initialization
    void Start()
    {
        Debug.Log("RankboardConstructor Awake");
        if (SceneManager.GetActiveScene().buildIndex != (int)Constant.SceneNumber.RANK_BOARD)
        {
            return;
        }
        LoadRankList();
    }

    public void LoadRankList()
	{

		DateTime begin = DateTimeManager.Instance.getKoreaTimeFromUTCNow();
		int week = DateTimeManager.Instance.GetWeeksOfYear(begin);
		begin = DateTimeManager.Instance.GetFirstDateOfWeek(begin.Year, week - 1);
		begin = begin.AddDays(-1);
		DateTime end = begin.AddDays(7);

        // score
        List<Ab001Score> scoreList = ScoreService.Instance.FindScoreByScoreDateContain(begin, end);

        List<string> user_ids = new List<string>();
        for (int i=0; i<scoreList.Count; i++)
        {
            user_ids.Add(scoreList[i].user_id);
        }

        // nickname
        List<R_UserGame> userGameList = R_UserGameService.Instance.GetUserGameByUserIdContains(user_ids);

        for (int i=0; i< scoreList.Count; i++)
        {
            // rankList
            IEnumerable<R_UserGame> query =
                from userGame in userGameList
                where userGame.user_id == scoreList[i].user_id
                select userGame;

            foreach (R_UserGame userGame in query)
            {
                RankInfo ri = new RankInfo();
                ri.nick_name = userGame.nick_name;
                ri.score = scoreList[i];
                rankInfoList.Add(ri);
            }
        }
        Debug.Log("Loading done");
        foreach (RankInfo ri in rankInfoList)
        {
            Debug.Log(ri);
        }
        Binding();
    }

    public void Binding()
    {
        Debug.Log("Binding()");
        for (int i=0; i<rankInfoList.Count; i++)
        {
            GameObject tempRankItemObject = Instantiate(this.rankItemObject) as GameObject;
            RankItemObject tempItemObject = tempRankItemObject.GetComponent<RankItemObject>();
            tempItemObject.score.text = rankInfoList[i].score.score.ToString();
            tempItemObject.time.text = rankInfoList[i].score.time.ToString();
            tempItemObject.message.text = rankInfoList[i].score.message;
            tempItemObject.level.text = LEVEL[rankInfoList[i].score.level];
			tempItemObject.rank.text = (i + 1).ToString();
            tempItemObject.nickName.text = rankInfoList[i].nick_name;
            // 1등
            if (i == 0)
            {
                tempItemObject.ribbon.sprite = Resources.Load<Sprite>("Image/rankboard/ribbon_1st");
				tempItemObject.scrollView.SetActive(false);
            }
            // 2등
            else if (i == 1)
            {
                tempItemObject.ribbon.sprite = Resources.Load<Sprite>("Image/rankboard/ribbon_2nd");
				tempItemObject.scrollView.SetActive(false);
			}
            // 3등
            else if (i == 2)
            {
                tempItemObject.ribbon.sprite = Resources.Load<Sprite>("Image/rankboard/ribbon_3rd");
				tempItemObject.scrollView.SetActive(false);
			}

            // add rank item on the list
            tempRankItemObject.transform.SetParent(this.Content);
            tempRankItemObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }   
    }
}
