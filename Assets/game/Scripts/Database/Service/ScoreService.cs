using System;
using System.Collections.Generic;
using System.Data;
using Ab001.Database.Dto;
using Ab001.Util;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Ab001.Database.Service
{
	public class ScoreService
	{
		internal const string SELECT_ALL = "select * from user";
		internal const string SELECT_BY_USER_ID = "select * from user where user_id = ";
		internal const string SELECT_BY_USER_NICKNAME = "select * from user where nick_name = ";
		internal const string UPDATE_USER = "update user set ";
		internal const string INSERT_USER = "insert into user values ";

		// Global variables
		private static ScoreService instance = null;

		public static ScoreService Instance
		{
			get
			{
				if (instance == null)
				{
					lock (typeof(ScoreService))
					{
						if (instance == null)
						{
							instance = new ScoreService();
						}
					}
				}
				return instance;
			}
		}

		private ScoreService()
		{
		}

		private List<string> GetDataReaderColumnNames(IDataReader rdr)
		{
			var columnNames = new List<string>();
			for (int i = 0; i < rdr.FieldCount; i++)
				columnNames.Add(rdr.GetName(i));
			return columnNames;
		}

		public int InsertScore(Ab001Score score)
		{
			int ret = 0;
			string query = String.Format("insert into ab001_score (user_id, message, score, level, score_date, time) values ('{0}', '{1}', {2}, {3}, '{4}', {5})",
				score.user_id, score.message, score.score, score.level, score.score_date.ToString("yyyyMMddhhmmss"), score.time);
			ret = MySqlConnector.Instance.DoNonQuery(query);
			Debug.Log("InsertScore() ret is " + ret);
			return ret;
		}

		public int UpdateScore(Ab001Score score)
		{
			int ret = 0;
			string query = String.Format("update ab001_score score = {0}, level = {1}, time = {2}, score_date = {3} where id = {4}"
				, score.score
				, score.level
				, score.time
				, DateTimeManager.Instance.getKoreaTimeFromUTCNow().ToString("yyyyMMddhhmmss")
				, score.id);

			ret = MySqlConnector.Instance.DoNonQuery(query);
			Debug.Log("UpdateScore() ret is " + ret);
			return ret;
		}

        public List<Ab001Score> FindScoreByScoreDateToday()
        {
            List<Ab001Score> list = new List<Ab001Score>();
            return list;
        }

        public List<Ab001Score> FindScoreByScoreDateContain(DateTime begin, DateTime end)
        {
            List<Ab001Score> list = new List<Ab001Score>();

            string query = String.Format("select t1.* from ab001_score as t1, " +
                                         "(select user_id, max(score) as max_score " +
                                             "from (select * from ab001_score " +
                                             "where score_date " +
                                                 "between date('{0}') and date('{1}') + 1) as week_table " +
                                         "group by user_id) as t2 " +
                                         "where t1.user_id = t2.user_id and t1.score = t2.max_score order by t1.score desc",
            begin.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));
            Debug.Log("query : " + query);

            Ab001Score score = null;
            MySqlConnector.Instance.DoSelectQuery(query, (MySqlDataReader reader) =>
            {
                // 데이터 없음
                if (reader == null)
                {
                    Debug.Log("No data");
                    return;
                }

                /////////// for debuging ///////////
                Debug.Log("Parsing data");
                //List<string> columns = GetDataReaderColumnNames(reader);
                //foreach (string col in columns)
                //{
                //    Debug.Log(col);
                //}
                //Debug.Log("reader: " + columns.ToString());
                /////////// for debuging ///////////

                int id = int.Parse(reader["id"].ToString());
                string user_id = reader["user_id"].ToString();
                string message = reader["message"].ToString();
                int _score = int.Parse(reader["score"].ToString());
                int level = int.Parse(reader["level"].ToString());
                string score_date = reader["score_date"].ToString();
                int time = int.Parse(reader["time"].ToString());
                Debug.Log("Set data on the ab001_score");
                score = new Ab001Score
                {
                    id = id,
                    user_id = user_id,
                    message = message,
                    score = _score,
                    level = level,
                    score_date = Convert.ToDateTime(score_date),
                    time = time
                };

                list.Add(score);
            });
            return list;
        }

		public Ab001Score FindScoreByScoreDateContain(DateTime date)
		{
			string query = String.Format("select * from ab001_score where score_date like '%{0}%'"
				, date.ToString("yyyyMMdd"));

            Ab001Score score = null;
			MySqlConnector.Instance.DoSelectQuery(query, (MySqlDataReader reader) =>
			{
				// 데이터 없음
				if (reader == null)
				{
					Debug.Log("No data");
					return;
				}

				/////////// for debuging ///////////
				Debug.Log("Parsing data");
				//List<string> columns = GetDataReaderColumnNames(reader);
				//foreach (string col in columns)
				//{
				//    Debug.Log(col);
				//}
				//Debug.Log("reader: " + columns.ToString());
				/////////// for debuging ///////////

				int id = int.Parse(reader["id"].ToString());
				string user_id = reader["user_id"].ToString();
				string message = reader["message"].ToString();
				int _score = int.Parse(reader["score"].ToString());
				int level = int.Parse(reader["level"].ToString());
				string score_date = reader["score_date"].ToString();
				int time = int.Parse(reader["time"].ToString());
				Debug.Log("Set data on the ab001_score");
				score = new Ab001Score
                {
					id = id,
					user_id = user_id,
					message = message,
					score = _score,
					level = level,
					score_date = Convert.ToDateTime(score_date),
					time = time
				};
			});
			Debug.Log("return score");
			return score;
		}
	}
}
