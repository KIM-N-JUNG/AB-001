using System;
using System.Collections.Generic;
using System.Data;
using Ab001.Database.Dto;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Ab001.Database.Service
{
    public class R_UserGameService
    {
        // Global variables
        private static R_UserGameService instance = null;

        public static R_UserGameService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(R_UserGameService))
                    {
                        if (instance == null)
                        {
                            instance = new R_UserGameService();
                        }
                    }
                }
                return instance;
            }
        }

        private R_UserGameService()
        {
        }

        private List<string> GetDataReaderColumnNames(IDataReader rdr)
        {
            var columnNames = new List<string>();
            for (int i = 0; i < rdr.FieldCount; i++)
                columnNames.Add(rdr.GetName(i));
            return columnNames;
        }

        public int InsertR_UserGame(R_UserGame userGame)
        {
            int ret = 0;
            string query = String.Format("insert into r_user_game (user_id, game_code, nick_name, create_date) values ('{0}', '{1}', '{2}', '{3}')"
                , userGame.user_id
                , userGame.game_code
                , userGame.nick_name
                , userGame.create_date.ToString("yyyyMMddhhmmss"));
            ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("InsertR_UserGame() ret is " + ret);
            return ret;
        }

        public int UpdateUserByUserId(string user_id, string key, string value)
        {
            string query = String.Format("update r_user_game set {0} = '{1}' where user_id = '{2}'", key, value, user_id);
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("UpdateUserByUserId() ret is " + ret);
            return ret;
        }
        public int UpdateUserByGameCode(string game_code, string key, int value)
        {
            string query = String.Format("update r_user_game set {0} = '{1}' where game_code = '{2}'", key, value, game_code);
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("UpdateUserByGameCode() ret is " + ret);
            return ret;
        }

        private List<R_UserGame> GetUserGameList(string query)
        {
            List<R_UserGame> userGameList = new List<R_UserGame>();
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
                /// 
                string user_id = reader["user_id"].ToString();
                string game_code = reader["game_code"].ToString();
                string nick_name = reader["nick_name"].ToString();
                string create_date = reader["create_date"].ToString();
                R_UserGame userGame = new R_UserGame
                {
                    user_id = user_id,
                    game_code = game_code,
                    nick_name = nick_name,
                    create_date = Convert.ToDateTime(create_date)
                };
                userGameList.Add(userGame);
            });
            return userGameList;
        }

        public List<R_UserGame> GetUserGame()
        {
            string query = String.Format("select * from r_user_game");
            return GetUserGameList(query);
        }

        public List<R_UserGame> GetUserGameByUserId(string user_id)
        {
            string query = String.Format("select * from r_user_game where user_id = '{0}'", user_id);
            return GetUserGameList(query);
        }

        private string ListToString(List<string> list)
        {
            string[] str = list.ToArray();
            string ret = "'";
            ret += string.Join("','", str);
            ret += "'";
            return ret;
        }
        public List<R_UserGame> GetUserGameByUserIdContains(List<string> user_ids)
        {
            string query = String.Format("select * from r_user_game where user_id in ({0})", ListToString(user_ids));
            return GetUserGameList(query);
        }

        public List<R_UserGame> GetUserGameByGameCode(string game_code)
        {
            string query = String.Format("select * from r_user_game where game_code = '{0}'", game_code);
            return GetUserGameList(query);
        }

        public List<R_UserGame> GetUserGameByNickName(string nick_name)
        {
            string query = String.Format("select * from r_user_game where nick_name = '{0}'", nick_name);
            return GetUserGameList(query);
        }

        public R_UserGame GetUserGameByUserIdAndGameCode(string user_id, string game_code)
        {
            string query = String.Format("select * from r_user_game where user_id = '{0}' and game_code = '{1}'", user_id, game_code);
            List<R_UserGame> list = GetUserGameList(query);
            if (list.Count == 0)
            {
                return null;
            }
            else if (list.Count == 1)
            {
                return list[0];
            }
            else
            {
                throw new NotSupportedException(string.Format("Should be allowed only one user_id({0}) per game({1})!!", user_id, game_code));
            }
        }

        public R_UserGame GetUserGameByNickNameAndGameCode(string nick_name, string game_code)
        {
            string query = String.Format("select * from r_user_game where nick_name = '{0}' and game_code = '{1}'", nick_name, game_code);
            List<R_UserGame> list = GetUserGameList(query);
            if (list.Count == 0)
            {
                return null;
            }
            else if (list.Count == 1)
            {
                return list[0];
            }
            else
            {
                throw new NotSupportedException(string.Format("Should be allowed only one nickName({0}) per game({1})!!", nick_name, game_code));
            }
        }
    }
}
