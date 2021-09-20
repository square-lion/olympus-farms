using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimalAI : MonoBehaviour {

	public int speed;
	public float moveTime;
	private float curMoveTime;
	public float waitTime;
	private float curWaitTime;
	public bool moving;

	public Vector3 reset;

	public Pasture pasture;

	Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody>();

		curMoveTime = moveTime;
		curWaitTime = waitTime;

		transform.position = reset;

	}

	void FixedUpdate(){
		if(moving){
			rb.velocity = transform.rotation * Vector3.forward * Time.deltaTime * speed;
			curMoveTime -= Time.deltaTime;

			if(curMoveTime < 0){
				moving = false;
				curWaitTime = waitTime;
			}
		}else{
			rb.velocity = Vector3.zero;
			curWaitTime -= Time.deltaTime;
			if(curWaitTime < 0){
				moving = true;
				curMoveTime = moveTime;
				PickDirection();
			}
		}
	}
	public void PickDirection(){
		var r = Quaternion.Euler(0, Random.Range(0,361), 0);
		transform.rotation = r;
	}

	public void TurnAround(){
		transform.position = reset;
	}

	void OnTriggerEnter(Collider other){
		if(other.CompareTag("Fence")){
			TurnAround();
		}
	}

	void OnMouseDown(){
		if (EventSystem.current.IsPointerOverGameObject())
			return;
    	if(Input.GetKeyDown(KeyCode.Mouse0)){
			pasture.OnMouseDown();
    	}
	}
}
