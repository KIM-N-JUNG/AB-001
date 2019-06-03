using System;
using System.Collections.Generic;
using System.Data;
using Database.Dto;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Database.Service
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

        public int InsertScore(Database.Dto.Score score)
        {
            int ret = 0;
            string query = String.Format("insert into score (user_id, message, score, level, score_date) values ('{0}', '{1}', {2}, {3}, '{4}')",
                score.user_id, score.message, score.score, score.level, score.score_date.ToString("yyyyMMddhhmmss"));
            ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("InsertScore() ret is " + ret);
            return ret;
        }
    }
}
