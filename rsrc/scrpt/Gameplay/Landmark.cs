using UnityEngine;
using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Collections.Generic;
//using System.Security.Cryptography;
using MySql.Data;
using MySql.Data.MySqlClient;

public class Landmark : MonoBehaviour {

	public GameObject avatar { get; set; }
	public int lam_key;
	private DBLandmark db_lam;

	public float ds { get; set; }

	public float max_ds;

	public float angle { get; set; }
		
	void Start ()
	{
		avatar = GameObject.FindGameObjectWithTag("avatar");
		angle = 30f;
		db_lam = new DBLandmark();
		db_lam = db_lam.get(lam_key);
	}
	
	void Update ()
	{
		ds = calcDs (avatar, 
			this.gameObject);
	}

	void OnGUI ()
	{
		if (ds <= max_ds && isFacing (avatar, this.gameObject)) {
			GUI.Label (new Rect (50, 50, 500, 500), db_lam.lam_nam + "\n" + db_lam.lam_des);
		}

		if (lam_key == 3) {GUI.Label (new Rect (50, 50, 500, 500), "\n\n\n\n" + ds);}
	}

	private bool isFacing (GameObject go1, 
		GameObject go2)
	{

		return Vector3.Angle (go1.transform.forward, go2.transform.position - go1.transform.position) < angle;
	}

	private Vector3 getPos (GameObject go)
	{

		return go.transform.localPosition;
	}

	private float calcDs (GameObject go1, 
		GameObject go2)
	{

		return Vector3.Distance (getPos (go1), 
			getPos (go2));
	}
}
