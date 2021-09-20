using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour {

	public string plotName;
	public string cropName;
	public int level;

	public GameObject product;
	public bool stocked;
	public int price;

	public int[] levelYields;

	PlotScreen plotScreen;
	void Awake(){
		plotScreen = FindObjectOfType<PlotScreen>();
		product.SetActive(false);
	}

	public void Yield(){
		product.SetActive(false);
		if(stocked){
			stocked = false;
			if(cropName == "Wheat")
				FindObjectOfType<GameController>().wheatAmount += (int)(levelYields[level] * FindObjectOfType<GameController>().fieldBlessing);
			else if (cropName == "Potato")
				FindObjectOfType<GameController>().potatoAmount += (int)(levelYields[level] * FindObjectOfType<GameController>().fieldBlessing);
			else
				FindObjectOfType<GameController>().carrotAmount += (int)(levelYields[level] * FindObjectOfType<GameController>().fieldBlessing);
		}else{
			return;
		}
	}

	void OnMouseOver(){
		if (EventSystem.current.IsPointerOverGameObject())
			return;
    	if(Input.GetKeyDown(KeyCode.Mouse0)){
			plotScreen.gameObject.SetActive(true);
			plotScreen.Open(plotName, true, this.gameObject);
    	}
	}

	public void BuySeeds(){
		product.SetActive(true);
		stocked = true;
	}
}
