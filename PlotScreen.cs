using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotScreen : MonoBehaviour {
	public Text plotName;
	public Button upgrade;
	public Button sell;
	public Button feed;
	public Button buySeeds;
	public Text capacity;
	public Text level;

	public GameObject sellMenu;

	public GameObject current;
	GameController game;

	void Start(){
		game = FindObjectOfType<GameController>();
		gameObject.SetActive(false);
	}

	void Update(){
		if(current != null && current.GetComponent<Field>() != null){
			Field cur = current.GetComponent<Field>();
			level.text = "Level " + (cur.level  + 1);
			buySeeds.transform.GetChild(0).GetComponent<Text>().text = "Buy Seeds - $" + cur.price;
			if(cur.stocked)
				capacity.text = current.GetComponent<Field>().levelYields[current.GetComponent<Field>().level] + " next harvest";

			upgrade.transform.GetChild(0).GetComponent<Text>().text = "Upgrade - $" + game.ungradePrices[cur.level];
		}
		else if(current != null && current.GetComponent<Pasture>() != null){
			Pasture cur = current.GetComponent<Pasture>();
			level.text = "Level " + (cur.level  + 1);
			capacity.text = cur.currentAnimalNum + "/" + cur.levelYields[cur.level];
			if(cur.animalName == "Sheep")
				feed.transform.GetChild(0).GetComponent<Text>().text = "Feed - " + (cur.price * cur.currentAnimalNum) + " Wheat";
			else if(cur.animalName == "Pig")
				feed.transform.GetChild(0).GetComponent<Text>().text = "Feed - " + (cur.price * cur.currentAnimalNum) + " Potatos";
			else
				feed.transform.GetChild(0).GetComponent<Text>().text = "Feed - " + (cur.price * cur.currentAnimalNum) + " Carrots";
			upgrade.transform.GetChild(0).GetComponent<Text>().text = "Upgrade - $" + game.ungradePrices[cur.level];
		}
	}

	public void Open(string plot, bool field, GameObject clicked){
		current = clicked;
		plotName.text = plot;
		capacity.text = "";
		if(field){
			Field selected = clicked.GetComponent<Field>();
			sell.gameObject.SetActive(false);
			feed.gameObject.SetActive(false);
			buySeeds.gameObject.SetActive(true);
			level.text = "Level " + (selected.level  + 1);
			if(selected.stocked){
				capacity.text = current.GetComponent<Field>().levelYields[current.GetComponent<Field>().level] + " next harvest";
				buySeeds.gameObject.SetActive(false);
			}
		}else{
			Pasture selected = clicked.GetComponent<Pasture>();
			sell.gameObject.SetActive(true);
			feed.gameObject.SetActive(true);
			buySeeds.gameObject.SetActive(false);
			level.text = "Level " + (selected.level + 1);
			capacity.text = selected.currentAnimalNum + "/" + selected.levelYields[selected.level];
			if(selected.fed){
				feed.gameObject.SetActive(false);
			}
			if(selected.currentAnimalNum == 2 || selected.currentAnimalNum == 0)
				sell.gameObject.SetActive(false);
		}
	}
	
	public void Close(){
		gameObject.SetActive(false);
	}

	public void BuySeeds(){
		if(game.money >= current.GetComponent<Field>().price){
			buySeeds.gameObject.SetActive(false);
			game.money -= current.GetComponent<Field>().price;
			current.GetComponent<Field>().BuySeeds();
			game.moneyText.text = "Money: " + game.money;
			capacity.text = current.GetComponent<Field>().levelYields[current.GetComponent<Field>().level] + " next harvest";
			
		}
	}
	public void Feed(){
		var cur = current.GetComponent<Pasture>();
		if(cur.animalName == "Sheep"){
			if(game.wheatAmount >= cur.price * cur.currentAnimalNum){
				game.wheatAmount -= cur.price * cur.currentAnimalNum;
				cur.Feed();
				feed.gameObject.SetActive(false);
				game.wheatText.text = "Wheat: " + game.wheatAmount;
				Close();
			}
		}
		else if(cur.animalName == "Pig"){
			if(game.potatoAmount >= cur.price * cur.currentAnimalNum){
				game.potatoAmount -= cur.price * cur.currentAnimalNum;
				cur.Feed();
				feed.gameObject.SetActive(false);
				game.potatoText.text = "Potato: " + game.potatoAmount;
				Close();
			}
		}
		else{
			if(game.carrotAmount >= cur.price * cur.currentAnimalNum){
				game.carrotAmount -= cur.price * cur.currentAnimalNum;
				cur.Feed();
				feed.gameObject.SetActive(false);
				game.carrotText.text = "Carrot: " + game.carrotAmount;
				Close();
			}
		}
	}
	public void Upgrade(){
		if(current.GetComponent<Field>() != null && current.GetComponent<Field>().level != 4 && game.money >= game.ungradePrices[current.GetComponent<Field>().level]){
			level.text = "Level " + (current.GetComponent<Field>().level + 1);
			game.money -= game.ungradePrices[current.GetComponent<Field>().level];
			current.GetComponent<Field>().level++;
		}else if(current.GetComponent<Pasture>() != null && current.GetComponent<Pasture>().level != 4 && game.money >= game.ungradePrices[current.GetComponent<Pasture>().level]){
			current.GetComponent<Pasture>().level++;
			level.text = "Level " + (current.GetComponent<Pasture>().level + 1);
			game.money -= game.ungradePrices[current.GetComponent<Pasture>().level];
		}
	}

	public void Sell(){
		sellMenu.SetActive(true);
	}
}
