using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public GameObject avatar;
	public GameObject main_cam;
	public CharacterController avatar_con {get; set;}
	public float mov_spd {get; set;}
	public float jump_str {get; set;}
	public float cam_sen {get; set;}
	public Vector3 mov_dir {get; set;}
	public float rot_x {get; set;}
	public float rot_y {get; set;}
	public float rot_z {get; set;}
	public float mov_x {get; set;}
	public float mov_y {get; set;}
	public float mov_z {get; set;}
	public float jump_y {get; set;}

	void Start () {
		avatar_con = avatar.GetComponent<CharacterController>();
		jump_str = 4;
		cam_sen = 90;
		rot_x = 0;
		rot_y = 0;
		rot_z = 0;
		mov_x = 0;
		mov_y = 0;
		mov_z = 0;
	}
	
	void Update () {

		mov_x = 0;
		mov_y = 0;
		mov_z = 0;

		if (avatar_con.isGrounded) {
			mov_spd = 20;

			if (Input.GetKeyDown(KeyCode.Space)) {

				jump_y = avatar.transform.localPosition.y;
				jump_y += 4;

				while (avatar.transform.localPosition.y <= jump_y) {
					setPos(avatar, 
						avatar.transform.localPosition.x, 
						avatar.transform.localPosition.y + (jump_str * Time.deltaTime), 
						avatar.transform.localPosition.z);
				}
			}

			if (Input.GetKey(KeyCode.LeftShift)) {
				mov_spd = 40;
			}		
		} else {

			if (Input.GetKey(KeyCode.LeftShift)) {
				mov_spd = 20;
			}

			mov_spd = 10;
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			rot_y -= cam_sen * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.RightArrow)) {
			rot_y += cam_sen * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			rot_x -= cam_sen * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			rot_x += cam_sen * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.W)) {
			mov_z += mov_spd * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.S)) {
			mov_z -= mov_spd * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A)) {
			mov_x -= (mov_spd / 2) * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.D)) {
			mov_x += (mov_spd / 2) * Time.deltaTime;
		}

		rot_x -= Input.GetAxis("Mouse Y") * cam_sen * Time.deltaTime;
		rot_y += Input.GetAxis("Mouse X") * cam_sen * Time.deltaTime;
		rot_x = Mathf.Clamp(rot_x, -60, 60);

		setRot(avatar, 
			0, 
			rot_y, 
			rot_z);
		setRot(main_cam, 
			rot_x, 
			0, 
			rot_z);
		move(avatar, 
			mov_x, 
			mov_y, 
			mov_z);
		move(avatar, 
				0, 
				-2, 
				0);
	}

	private void move (GameObject go, 
		float x, 
		float y, 
		float z) {

		CharacterController go_con;

		mov_dir = new Vector3(x, 
			y, 
			z);
		mov_dir = transform.TransformDirection(mov_dir);
		mov_dir = mov_dir * mov_spd * Time.deltaTime;
		go_con = go.GetComponent<CharacterController>();

		go_con.Move(mov_dir);
	}

	private void jump(GameObject go) {

	}

	private void setPos (GameObject go, 
		float x, 
		float y, 
		float z) {
		go.transform.localPosition = new Vector3(x, y, z);
	}

	private void setRot(GameObject go, 
		float x, 
		float y, 
		float z) {

		go.transform.localEulerAngles = new Vector3(x, 
			y, 
			z);
	}
}
