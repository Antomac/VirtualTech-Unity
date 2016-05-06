using UnityEngine;
using System.Collections;

public class LIGUI : MonoBehaviour {

	public DBConnection _mysql;
	
	void OnGUI() {
		GUI.Label(new Rect(10, 
			10, 
			300, 
			100), 
		"MySQL Connection State:" + _mysql.getConState());

		//Debug.Log("MySQL Connection State:" + _mysql.getConState());
	}
}
