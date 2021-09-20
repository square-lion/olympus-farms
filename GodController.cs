using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodController : MonoBehaviour {

	public GameObject camera;
	public Vector3 alterSpot;

	public GameObject textBox;
	public bool firstTime;

	public string sacrifice;

	public void Sacrifice(){
		FindObjectOfType<AudioSource>().Stop();
		camera.GetComponent<CameraMovement>().enabled = false;
		camera.transform.position = alterSpot;
		pickSacrifice();

		textBox.SetActive(true);
		Text t = textBox.transform.GetChild(1).GetComponent<Text>();

		if(firstTime){
			t.text = "The Gods... Require something... a sacrifice. They ask for an offering in the form a sheep. Accept and recive good fortune, but decline, and be punished";
		}else{
			string anim =sacrifice.Substring(0,1);
			int amount = int.Parse(sacrifice.Substring(1));
			if(anim == "S")
				anim = "Sheep";
			else if (anim == "P")
				anim = "Pig";
			else if(anim == "C")
				anim = "Cow";
			else{
				t.text = "This is it, you have sacrificed everything.";
			}
			t.text = "The Gods demand " + amount + " " + anim + " or there will be punishment.";
		}
	}
	public void pickSacrifice(){
		GameController game = FindObjectOfType<GameController>();

		if(firstTime){
			sacrifice = "S1";
			return;
		}

		int animal = Random.Range(0,3);
		char anim = 'S';
		switch(animal){
			case 0: anim = 'S'; break;
			case 1: anim = 'P'; break;
			case 2: anim = 'C'; break;
		}
		var amount = Random.Range(1, 4);

		//New Code
		if(anim == 'S' && game.pastures[0].currentAnimalNum <= 0)
			anim = 'P';
		if(anim == 'P' && game.pastures[1].currentAnimalNum <= 0)
			anim = 'C';
		if(anim == 'C' && game.pastures[2].currentAnimalNum <= 0)
			anim = 'S';
		if(anim == 'S' && game.pastures[0].currentAnimalNum <= 0)
			anim = 'P';

		if(anim == 'S' && game.pastures[0].currentAnimalNum <= amount)
			amount = Random.Range(1, game.pastures[0].currentAnimalNum);
		if(anim == 'P' && game.pastures[1].currentAnimalNum <= amount)
			amount = Random.Range(1, game.pastures[1].currentAnimalNum);
		if(anim == 'C' && game.pastures[2].currentAnimalNum <= amount)
			amount = Random.Range(1, game.pastures[2].currentAnimalNum);


		//Old Code

		/*
		if(anim == 'S' && game.pastures[0].currentAnimalNum <= 0){
			while(anim == 'S' && game.pastures[0].currentAnimalNum < amount)
				amount = Random.Range(1, 4);
		}
		else if(anim == 'S')
		{
			while(anim == 'S'){
				animal = Random.Range(0,3);
				switch(animal){
					case 0: anim = 'S'; break;
					case 1: anim = 'P'; break;
					case 2: anim = 'C'; break;
				}
			}
		}
		if(anim == 'P' && game.pastures[1].currentAnimalNum <= 0){
			while(anim == 'P' && game.pastures[1].currentAnimalNum < amount)
				amount = Random.Range(1, 4);
		}
		else if(anim == 'P'){
			while(anim == 'P'){
				animal = Random.Range(0,3);
				switch(animal){
					case 0: anim = 'S'; break;
					case 1: anim = 'P'; break;
					case 2: anim = 'C'; break;
				}
			}
		}
		if(anim == 'C' && game.pastures[2].currentAnimalNum <= 0){
			while(anim == 'C' && game.pastures[2].currentAnimalNum < amount)
				amount = Random.Range(1, 4);
		}
		else if(anim == 'C'){
			while(anim == 'C'){
				animal = Random.Range(0,3);
				switch(animal){
					case 0: anim = 'S'; break;
					case 1: anim = 'P'; break;
					case 2: anim = 'C'; break;
				}
			}
		}
		 */
		sacrifice = "" + anim + amount;
	}

	public void accept(){
		GameController game = FindObjectOfType<GameController>();
		string anim = sacrifice.Substring(0,1);
		int num = int.Parse(sacrifice.Substring(1));

		if(anim == "S"){
			game.pastures[0].currentAnimalNum -= num;
		}
		else if(anim == "P"){
			game.pastures[1].currentAnimalNum -= num;
		}
		else{
			game.pastures[2].currentAnimalNum -= num;
		}
		textBox.SetActive(false);
		firstTime = false;
		camera.GetComponent<CameraMovement>().enabled = true;
		ChooseBlessing();
		game.NewTurn();
	}

	public void decline(){
		textBox.SetActive(false);
		firstTime = false;
		camera.GetComponent<CameraMovement>().enabled = true;
		ChoosePunishment();
		FindObjectOfType<GameController>().NewTurn();
	}

	public void ChooseBlessing(){
		GameController game = FindObjectOfType<GameController>();
		int chance = Random.Range(0,2);
		if(chance == 0){
			game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "Your offering to the Gods seemed to have no reward.";
			game.fieldBlessing = 1;
			game.pastureBlessing = 1;
		}else{
			chance = Random.Range(0,2);
			if(chance == 0){
				game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "For seemingly no reason the rainy season was longer this year. Each field has produced double the amount of crops.";
				game.fieldBlessing = 2;
				game.pastureBlessing = 1;
			}
			else{
				game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "For seemingly no reason your animals have produced double the amount of offspring then usual.";
				game.pastureBlessing = 2;
				game.fieldBlessing =1;
			}
		}
	}

	public void ChoosePunishment(){
		GameController game = FindObjectOfType<GameController>();
		int chance = Random.Range(0,2);
		if(chance == 0){
			game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "Your disobediences to the Gods seemed to have no punishment.";
			game.pastureBlessing = 1;
			game.fieldBlessing = 1;
		}else{
			chance = Random.Range(0,2);
			if(chance == 0){
				game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "Dispite your best efforts to stop it, a disease has run through your pastures and kill half of your animals. What could of caused such an outbreak.";
				game.pastureBlessing = -1;
				game.fieldBlessing = 1;
			}
			else{
				game.yearlyReport.transform.GetChild(3).GetComponent<Text>().text = "The rainy season was unbaribly short this year. You were only able to save half of your crops. What could of caused such a drought.";
				game.fieldBlessing = .5f;
				game.pastureBlessing = 1;
			}
		
		}
	}
}