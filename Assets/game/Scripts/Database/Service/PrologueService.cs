using System;
using System.Collections.Generic;
using System.Data;
using Database.Dto;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Database.Service
{
    public class PrologueService
    {
        internal const string SELECT_ALL = "select * from prologue";
        internal const string SELECT_BY_ID = "select * from prologue where id = ";


        // Global variables
        private static PrologueService instance = null;

        public static PrologueService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (typeof(PrologueService))
                    {
                        if (instance == null)
                        {
                            instance = new PrologueService();
                        }
                    }
                }
                return instance;
            }
        }

        private PrologueService()
        {
        }

        private List<string> GetDataReaderColumnNames(IDataReader rdr)
        {
            var columnNames = new List<string>();
            for (int i = 0; i < rdr.FieldCount; i++)
                columnNames.Add(rdr.GetName(i));
            return columnNames;
        }

        public Prologue GetPrologueByContentTypeAndLanguage(string _contentType, int _language)
        {
            Prologue prologue = null;
            string query = "select * from prologue where content_type = '" + _contentType + "' and language = " + _language;
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
                string contentType = reader["content_type"].ToString();
                string content = reader["content"].ToString();
                int language = int.Parse(reader["language"].ToString());

                Debug.Log("Set data on the prologue");
                prologue = new Prologue{
                    id = id,
                    content_type = contentType,
                    content = content,
                    language = language
                };
            });
            Debug.Log("return Prologue"); 
            return prologue;
        }
    }
}
