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
	DatabaseManager(String dbName, String tableName){
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
	void Start(){
		//String[] values = {"qwer", "qqq"};
		//String columns = "ID, Count";
		//AddRow (columns, values);
	}

	public void test1(){
		AddRow ("ID, Count", new string[]{"qwe!", "qqqq!"});
		Close ();
		print ("done");
	}

	public void test2(){
		List<ArrayList> data = getData ("*");
		int i = 0;
		foreach(ArrayList a in data){
			print (i + ") " + a[0] + " - " + a[1]);
			i++;
		}
		Close ();
		print ("done " + data.Count);
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

	//возвращает List, в котором в качестве ArrayList представлены строки таблицы
	public List<ArrayList> getData(String columns){
		OpenConnection();
		Read(columns);
		List<ArrayList> data = new List<ArrayList> ();
		while (dbr.Read())
		{
			ArrayList buf = new ArrayList ();
			for(int i = 0; i < dbr.FieldCount; i++){
				buf.Add (dbr[i]);
			}
			data.Add (buf);
		}
		dbr.Close ();
		return data;
	}
}
