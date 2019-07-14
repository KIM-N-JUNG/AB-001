using System;
using System.Collections.Generic;
using System.Data;
using Ab001.Database.Dto;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Ab001.Database.Service
{
    public class GameService
    {
        internal const string SELECT_ALL = "select * from user";
        internal const string SELECT_BY_USER_ID = "select * from user where user_id = ";
        internal const string SELECT_BY_USER_NICKNAME = "select * from user where nick_name = ";
        internal const string UPDATE_USER = "update user set ";
        internal const string INSERT_USER = "insert into user values ";

        // Global variables
        private static GameService instance = null;

        public static GameService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(GameService))
                    {
                        if (instance == null)
                        {
                            instance = new GameService();
                        }
                    }
                }
                return instance;
            }
        }

        private GameService()
        {
        }

        private List<string> GetDataReaderColumnNames(IDataReader rdr)
        {
            var columnNames = new List<string>();
            for (int i = 0; i < rdr.FieldCount; i++)
                columnNames.Add(rdr.GetName(i));
            return columnNames;
        }

        public Game GetGameByCode(string _code)
        {
            Game game = null;
            string query = String.Format("select * from {0} where code = '{1}'", "game", _code);
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
                string code = reader["code"].ToString();
                string version = reader["version"].ToString();
                string updated_date = reader["updated_date"].ToString();
                game = new Game {
                    code = code,
                    version = version,
                    updated_date = Convert.ToDateTime(updated_date)
                };
            });
            return game;
        }
    }
}
