using System;
using System.Collections.Generic;
using System.Data;
using Database.Dto;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Database.Service
{
    public class UserService
    {
        internal const string SELECT_ALL = "select * from user";
        internal const string SELECT_BY_USER_ID = "select * from user where user_id = ";
        internal const string UPDATE_USER = "update user set ";
        internal const string INSERT_USER = "insert into user values ";

        // Global variables
        private static UserService instance = null;

        public static UserService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(UserService))
                    {
                        if (instance == null)
                        {
                            instance = new UserService();
                        }
                    }
                }
                return instance;
            }
        }

        private UserService()
        {
        }

        private List<string> GetDataReaderColumnNames(IDataReader rdr)
        {
            var columnNames = new List<string>();
            for (int i = 0; i < rdr.FieldCount; i++)
                columnNames.Add(rdr.GetName(i));
            return columnNames;
        }

        public int InsertUser(User user)
        {
            int ret = 0;
            string query = INSERT_USER + "(" + 
                user.id + ",'" + 
                user.nick_name + "','" + 
                user.email + "','" + 
                user.country + "','" + 
                user.user_id + "','" + 
                user.user_image + "'," + 
                user.visit_count + ",'" + 
                user.user_name + "')";
            Debug.Log("Insert query : " + query);
            ret = MySqlConnector.Instance.DoNonQuery(query);
            return ret;
        }

        public int UpdateUserById(int id, string key, string value)
        {
            string query = UPDATE_USER + key + " = '" + value + "' where id = " + id;
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("ret is " + ret);
            return ret;
        }

        public int UpdateUserById(int id, string key, int value)
        {
            string query = UPDATE_USER + key + " = " + value + " where id = " + id;
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("ret is " + ret);
            return ret;
        }

        public User GetUserByUserId(string _user_id)
        {
            User user = null;
            string query = SELECT_BY_USER_ID + "'" + _user_id + "'";
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
                int id = int.Parse(reader["id"].ToString());
                string nickName = reader["nick_name"].ToString();
                string email = reader["email"].ToString();
                string country = reader["country"].ToString();
                string user_id = reader["user_id"].ToString();
                string user_image = reader["user_image"].ToString();
                int visit_count = int.Parse(reader["visit_count"].ToString());
                string user_name = reader["user_name"].ToString();

                Debug.Log("Set data on the user");
                user = new User{ 
                    id = id, 
                    nick_name = nickName, 
                    email = email, 
                    country = country, 
                    user_id = user_id, 
                    user_image = user_image, 
                    visit_count = visit_count, 
                    user_name = user_name 
                };
            });
            Debug.Log("return user"); 
            return user;
        }
    }
}
