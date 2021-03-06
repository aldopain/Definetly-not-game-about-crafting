using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DatabaseManager : MonoBehaviour {
	private IDbConnection dbc;
	private IDbCommand dbcm;
	private IDataReader dbr;
	public String db = "Craft.db"; 
	public String table = "Inventory";

	//конструктор, принимающий имена базы данных и таблицы в ней
	public DatabaseManager(String dbName, String tableName){
		db = dbName;
		table = tableName;
	}

	void Close(){
		dbr.Close ();
		dbr = null;
		dbc.Close ();
		dbc = null;
		dbcm.Dispose ();
		dbcm = null;
	}

	//тестовая функция
	public void AddRow(String columns, String[] values) {
		OpenConnection();
		Insert(columns, values);
	}

	//открывает соединение с бд
	void OpenConnection(){
		dbc = new SqliteConnection("URI=file:" + Application.dataPath + "/Databases/" + db);
		dbc.Open();
		dbcm = dbc.CreateCommand ();
	}

	//додавляет строки в таблицу
	void Insert(String columns, String[] values){
		String v = "";
		for(int i = 0; i < values.Length - 1; i++)
			v += "'" + values[i] + "', ";
		v += "'" + values[values.Length - 1] + "'";
		dbcm.CommandText = "INSERT INTO " + table + " (" + columns + ") VALUES (" + v + ")";
		dbr = dbcm.ExecuteReader();
		dbr.Close();
	}

	//считывает столбцы columns из таблицы
	//если columns.Equals("*") == true, то считает все столбцы
	void Read(String columns){
		dbcm.CommandText = "SELECT " + columns + " FROM " + table;
		dbr = dbcm.ExecuteReader();
	}

	//возвращает List, в котором в качестве List<String> представлены строки таблицы
	public List<List<String>> getData(String columns){
		OpenConnection();
		Read(columns);
		List<List<String>> data = new List<List<String>> ();
		while (dbr.Read())
		{
			List<String> buf = new List<String> ();
			for(int i = 0; i < dbr.FieldCount; i++){
				buf.Add (dbr[i].ToString());
			}
			data.Add (buf);
		}
		dbr.Close ();
		return data;
	}
}
