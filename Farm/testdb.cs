using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class testdb : MonoBehaviour
{
    void Start()
    {
        testdb_1();
    }
        void testdb_1()
    {
        string conn = "URI=file:" + Application.dataPath + "/farm_db.db"; //Path to database.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Open connection to the database.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT * FROM items";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string vName = reader.GetString(0);
            int vClassID = reader.GetInt32(1);
            int vReadiness = reader.GetInt32(2);
            int vTeamperature = reader.GetInt32(3);
            int vHumidity = reader.GetInt32(4);
            int vAddons = reader.GetInt32(5);

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}

