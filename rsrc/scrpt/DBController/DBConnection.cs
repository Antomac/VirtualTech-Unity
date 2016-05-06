using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DBConnection : MonoBehaviour {

	public static GameObject _mysql;
	public string host;
	public string db;
	public string usr;
	public string pwd;
	public bool pooling;
	public string con_str {get; set;}
	public MySqlConnection con {get; set;}
	public MySqlCommand cmd {get; set;}
	public MySqlDataReader dat_rdr {get; set;}
	
	public DBConnection() {
		pooling = true;
		con = null;
		cmd = null;
	}

	void Awake() {
		DontDestroyOnLoad(this.gameObject);

		con_str = "Server=" + host;
		con_str += ";Database=" + db;
		con_str += ";User=" + usr;
		con_str += ";Password=" + pwd;
		con_str += ";Pooling=";

		if (!pooling) {
			con_str += "false;";
		} else {
			con_str += "true;";
		}

		try {
			con = new MySqlConnection(con_str);

			con.Open();
			Debug.Log("MySqlConnection: " + getConState());
		} catch(Exception ex) {
			Debug.Log(ex);
		}
	}

	void OnApplicationQuit() {

		if (con != null) {
			if (con.State.ToString() != "Closed") {
				con.Close();
				Debug.Log("MySqlConnection: " + getConState());
			}

			con.Dispose();
		}
	}

	public string getConState() {

		return con.State.ToString();
	}
}
