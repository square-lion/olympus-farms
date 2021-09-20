using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

		public GameObject settings;
		
		public void Play(){
			SceneManager.LoadScene("Tutorial");
		}

		public void OpenSettings(){
			settings.SetActive(true);
		}

		public void CloseSettings(){
			settings.SetActive(false);
		}
}
