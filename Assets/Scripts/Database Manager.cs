using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManager : MonoBehaviour {

	public static List<List<String>> getDB(String[] neededColumns, String dbName, String tableName){
		string conn = "URI=file:" + Application.dataPath + "/Databases/" + dbName;
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open();
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = getDataFromRow (neededColumns, tableName);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		List<List<String>> arg = new List<List<string>> ();
		int rowNum = 0;
		while (reader.Read ()) {
			arg.Add (new List<string> ());
			for (int i = 0; i < neededColumns.Length; i++) {
				print (i);
				arg [rowNum].Add (reader.GetString (i));
			}
			rowNum++;
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
		return arg;
	}

	static String getDataFromRow(String[] neededColumns, String tableName){
		String retVal = "SELECT ";
		foreach(String current in neededColumns)
			retVal += current + ", ";
		retVal = retVal.Remove (retVal.Length - 2);
		print(retVal + " FROM " + tableName);
		return retVal + " FROM " + tableName;
	}
}
