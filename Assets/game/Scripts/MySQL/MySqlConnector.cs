using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

public class MySqlConnector
{
    public delegate void Callback(MySqlDataReader reader);

    // Global variables
    private static MySqlConnection dbConnection;
    private static MySqlConnector instance = null;

    public static MySqlConnector Instance
    {
        get
        {
            if (instance == null)
            {
                lock (typeof(MySqlConnector))
                {
                    if (instance == null)
                    {
                        instance = new MySqlConnector();
                    }
                }
            }
            return instance;
        }
    }

    public MySqlConnector()
    {
    }

    // MySQL Query
    public void DoQuery(string sqlQuery, Callback readable)
    {
        string connectionString = "Server=sunyoungj.iptime.org;" +
            "Database=ab001;" +
            "User ID=kim.n.jung82;" +
            "Password= rla&wjd!;" +
            "Pooling=false";
        using (MySqlConnection db = new MySqlConnection(connectionString))
        {
            MySqlDataReader reader;
            using (MySqlCommand myCommand = new MySqlCommand(sqlQuery, db))
            {
                try
                {
                    db.Open();
                    Debug.Log("Connected to database. - " + connectionString);
                }
                catch (Exception e)
                {
                    Debug.Log("Exception!");
                    Debug.Log(e);
                    return;
                }
                reader = myCommand.ExecuteReader();
                Debug.Log("execute command - " + sqlQuery);
                while (reader.Read())
                {
                    readable(reader);
                    //Debug.Log(reader);
                    //Debug.Log(reader.GetString(0));
                    ////data 파싱
                    //string temp = reader["nick_name"].ToString();
                    //Debug.Log("nick_name : " + temp);
                    //temp = reader["email"].ToString();
                    //Debug.Log("email : " + temp);
                }
                reader.Close();
                db.Close();
            }
        }
    }
}