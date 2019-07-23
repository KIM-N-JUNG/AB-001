using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Threading;

public class MySqlConnector
{
    public delegate void Callback(MySqlDataReader reader);

    // Global variables
    private static MySqlConnection dbConnection;
    private static MySqlConnector instance = null;

    private static Thread thread = null;
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

    private MySqlConnector()
    {
    }

    public int DoNonQuery(string sqlQuery)
    {
        int ret = 0;
        string connectionString = "Server=sunyoungj.iptime.org;" +
            "Database=ab001;" +
            "Port=3306;" +
            "User ID=kim.n.jung82;" +
            "Password= dmschdQoal!;" +
            "Pooling=false";
        using (MySqlConnection db = new MySqlConnection(connectionString))
        {
            using (MySqlCommand myCommand = new MySqlCommand(sqlQuery, db))
            {
                try
                {
                    db.Open();
                    //Debug.Log("Connected to database. - " + connectionString);
                    ret = myCommand.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    db.Close();
                    Debug.Log("Exception!");
                    Debug.Log(e.Message);
                    throw e;
                }
                finally
                {
                    db.Close();
                }


            }
        }
        return ret;
    }

    // MySQL Query
    public void DoSelectQuery(string sqlQuery, Callback readable)
    {
        string connectionString = "Server=sunyoungj.iptime.org;" +
            "Database=ab001;" +
            "Port=3306;" +
            "User ID=kim.n.jung82;" +
            "Password= dmschdQoal!;" +
            "Pooling=false";
        using (MySqlConnection db = new MySqlConnection(connectionString))
        {
            MySqlDataReader reader;
            using (MySqlCommand myCommand = new MySqlCommand(sqlQuery, db))
            {
                try
                {
                    db.Open();
                    //Debug.Log("Connected to database. - " + connectionString);

                    reader = myCommand.ExecuteReader();
                    //Debug.Log("execute command - " + sqlQuery);

                    //Debug.Log("visibleFieldCount : " + reader.VisibleFieldCount);
                    bool hasRows = reader.HasRows;
                    //Debug.Log("has Rows? - " + (hasRows ? "true" : "false"));

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (readable != null)
                                readable(reader);
                        }
                    }
                    else
                    {
                        if (readable != null)
                            readable(null);
                    }
                    reader.Close();
                }
                catch (MySqlException e)
                {
                    db.Close();
                    Debug.Log("Exception!");
                    Debug.Log(e.Message);
                    Debug.Log(e.ToString());
                    throw new DatabaseConnectionException("can't access database", e);
                }
                finally
                {
                    db.Close();
                }
            }
        }
    }
}