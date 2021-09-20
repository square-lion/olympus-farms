using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public int speed;

	void Update () {
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			transform.Translate(new Vector3(-speed * Time.deltaTime,0,0));
		}
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			transform.Translate(new Vector3(0,0,speed * Time.deltaTime));
		}
		if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			transform.Translate(new Vector3(0,0,-speed * Time.deltaTime));
		}
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -23, 13), Mathf.Clamp(transform.position.y, 5,15), Mathf.Clamp(transform.position.z, -21.5f,30));
	}
}
