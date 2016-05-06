using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
//using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

public class DBLandmark  {

	public int lam_key { get; set; }
	public string lam_nam { get; set; }
	public string lam_des { get; set; }

	public string host { get; set; }
	
	public string db { get; set; }
	
	public string usr { get; set; }
	
	public string pwd { get; set; }
	
	public bool pooling;
	
	public string con_str { get; set; }
	
	public string q { get; set; }

	public DBLandmark() {
		host = "localhost";
		db = "msw_db";
		usr = "root";
		pwd = "n0m3l0";
		//pwd = "1234";
		pooling = true;
		con_str = "";
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
	}

	public ArrayList listAll ()
	{
		
		MySqlConnection con;
		MySqlCommand cmd;
		MySqlDataReader dat_rdr;
		ArrayList list;
		
		con = null;
		q = "";
		cmd = null;
		dat_rdr = null;
		list = new ArrayList ();
		
		list.Add ("tamal");
		
		con = new MySqlConnection(con_str);
		cmd = con.CreateCommand();
		q += "SELECT * FROM landmark;";
		
		try {
			con.Open();
		} catch (Exception ex) {
			Debug.Log (ex);
		}
		
		cmd.CommandText = q;
		dat_rdr = cmd.ExecuteReader ();
		
		while (dat_rdr.Read()) {
			DBLandmark lm;
			
			lm = new DBLandmark();
			
			lm.lam_nam = dat_rdr["lam_nam"].ToString();
			lm.lam_des = dat_rdr["lam_des"].ToString();
			
			list.Add (lm);
		}
		
		return list;
	}
	
	public DBLandmark get (int lam_key)
	{
		
		MySqlConnection con;
		MySqlCommand cmd;
		MySqlDataReader dat_rdr;
		DBLandmark db_lam;
		
		con = null;
		q = "";
		cmd = null;
		dat_rdr = null;
		db_lam = new DBLandmark();
		
		try {
			con = new MySqlConnection(con_str);
			cmd = con.CreateCommand();
			q += "SELECT * FROM landmark ";
			q += "WHERE lam_key = @lam_key;";
			cmd.Parameters.AddWithValue("@lam_key", lam_key);
			con.Open();
			
			cmd.CommandText = q;
			dat_rdr = cmd.ExecuteReader ();
			
			while (dat_rdr.Read()) {
				db_lam.lam_nam = dat_rdr["lam_nam"].ToString();
				db_lam.lam_des = dat_rdr["lam_des"].ToString();
			}
		} catch (Exception ex) {
			Debug.Log (ex);
		}
		
		
		return db_lam;
	}

}
