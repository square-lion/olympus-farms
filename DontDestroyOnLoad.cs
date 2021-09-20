using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

	void Awake(){
		if(FindObjectsOfType<DontDestroyOnLoad>().Length == 1)
			DontDestroyOnLoad(this.gameObject);
		else
			Destroy(this.gameObject);
	}
}
