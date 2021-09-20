using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public int Speed;
	Rigidbody rb;
	public bool right;
	public bool car;

	public float time = 10f;

	void Awake(){
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate(){
		time -= Time.deltaTime;
		if(time <=0)
			Destroy(this.gameObject);

		if(right){
			rb.velocity = Vector3.right * Speed * Time.deltaTime;
			if(car)
				transform.rotation = Quaternion.Euler(-90,180,0);
			else
				transform.rotation = Quaternion.Euler(0,180,0);
		}else{
			rb.velocity = Vector3.right * -Speed * Time.deltaTime;
			if(car)
				transform.rotation = Quaternion.Euler(-90,0,0);
			else
				transform.rotation = Quaternion.Euler(180,180,180);
		}
	}
}
