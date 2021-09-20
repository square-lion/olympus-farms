using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pasture : MonoBehaviour {

	public string plotName;
	public string animalName;
	public int currentAnimalNum;
	public int level;

	public GameObject animal;
	public List<GameObject> animals;

	public bool fed;
	public int price;
	public int animalPrice;

	public int[] levelYields;

	PlotScreen plotScreen;
	GameController game;
	void Awake(){
		plotScreen = FindObjectOfType<PlotScreen>();
		game = FindObjectOfType<GameController>();
	}

	void Update(){
		if(animalName == "Sheep")
			game.sheepAmount = currentAnimalNum;
		else if(animalName == "Pig")
			game.pigAmount = currentAnimalNum;
		else
			game.cowAmount = currentAnimalNum;
		
		if(animals.Count < currentAnimalNum){
			var a = Instantiate(animal, transform.position, transform.rotation);
			animals.Add(a);
			a.GetComponent<AnimalAI>().pasture = this;
		}else if(animals.Count > currentAnimalNum){
			var a = animals[animals.Count-1];
			animals.Remove(a);
			Destroy(a);
		}
		 
	}

	public void Yield(){
		if(fed){
			if(currentAnimalNum % 2 == 0)
				currentAnimalNum += (int)(currentAnimalNum / 2 * game.pastureBlessing);
			else
				currentAnimalNum += (int)((currentAnimalNum - 1)/2 * game.pastureBlessing);
		
			if(currentAnimalNum > levelYields[level])
				currentAnimalNum = levelYields[level];
			
			fed = false;
		}
		else{
			Debug.Log(game.pastureBlessing);
			if(game.pastureBlessing < 0)
				currentAnimalNum/=4;
			else if(game.pastureBlessing > 1)
				return;
			else
				currentAnimalNum/=2;
		}
		if(animalName == "Sheep")
			game.sheepAmount = currentAnimalNum;
		else if(animalName == "Pig")
			game.pigAmount = currentAnimalNum;
		else
			game.cowAmount = currentAnimalNum;
	}

	public void OnMouseDown(){
		if (EventSystem.current.IsPointerOverGameObject())
			return;
    	if(Input.GetKeyDown(KeyCode.Mouse0)){
			plotScreen.gameObject.SetActive(true);
			plotScreen.Open(plotName, false, this.gameObject);
    	}
	}

	public void Feed(){
		fed = true;
	}
}
