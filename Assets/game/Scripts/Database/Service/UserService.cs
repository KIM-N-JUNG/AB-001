using System;
using System.Collections.Generic;
using System.Data;
using Ab001.Database.Dto;
using Ab001.Util;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Ab001.Database.Service
{
    public class UserService
    {
        internal const string SELECT_ALL = "select * from user";
        internal const string SELECT_BY_USER_ID = "select * from user where user_id = ";
        internal const string SELECT_BY_USER_NICKNAME = "select * from user where nick_name = ";
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
            string query = "insert into user (auth, email, country, user_id, user_image, user_name, last_date) values ('" +
            user.auth + "','" +
            user.email + "'," +
            user.country + ",'" +
            user.user_id + "','" +
            user.user_image + "','" +
            user.user_name + "','" +
            user.last_date.ToString("yyyyMMddhhmmss") + "')";
            ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("InsertUser() ret is " + ret);
            return ret;
        }

        public int UpdateUserByUserId(string user_id, string key, string value)
        {
            string query = UPDATE_USER + key + " = '" + value + "', last_date = '" + DateTimeManager.Instance.getKoreaTimeFromUTCNow().ToString("yyyyMMddhhmmss") + "' where user_id = '" + user_id + "'";
			int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("UpdateUserByUserId() ret is " + ret);
            return ret;
        }
        public int UpdateUserByUserId(string user_id, string key, int value)
        {
            string query = UPDATE_USER + key + " = " + value + ", last_date = '" + DateTimeManager.Instance.getKoreaTimeFromUTCNow().ToString("yyyyMMddhhmmss") + "' where user_id = '" + user_id + "'";
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("UpdateUserByUserId() ret is " + ret);
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
				string auth = reader["auth"].ToString();
                string email = reader["email"].ToString();
                int country = int.Parse(reader["country"].ToString());
                string user_id = reader["user_id"].ToString();
                string user_image = reader["user_image"].ToString();
                int visit_count = int.Parse(reader["visit_count"].ToString());
                string user_name = reader["user_name"].ToString();
                string last_date = reader["last_date"].ToString();
                Debug.Log("Set data on the user");
                user = new User {
					user_id = user_id,
					auth = auth,
					user_name = user_name,
                    email = email,
                    country = country,
                    user_image = user_image,
                    visit_count = visit_count,
                    last_date = Convert.ToDateTime(last_date)
                };
            });
            Debug.Log("return user"); 
            return user;
        }

        public int deleteUser(User user)
        {
            string query = String.Format("delete from user where user_id = '{0}'", user.user_id);
            int ret = MySqlConnector.Instance.DoNonQuery(query);
            Debug.Log("deleteUser() ret is " + ret);
            return ret;
        }
    }
}
