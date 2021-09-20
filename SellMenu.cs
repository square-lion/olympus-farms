using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellMenu : MonoBehaviour {

	public Slider slider;
	public Text title;
	public Text amount;

	Pasture cur;

	void OnEnable(){
		slider.value = 1;
		cur = FindObjectOfType<PlotScreen>().current.GetComponent<Pasture>();
		title.text = "Sell " + cur.animalName;
		slider.maxValue = cur.currentAnimalNum - 2;
	}
	void Update(){
		amount.text = slider.value + " - $" + (cur.animalPrice * slider.value);
	}

	public void Sell(){
		GameController game = FindObjectOfType<GameController>();
		PlotScreen plot = FindObjectOfType<PlotScreen>();
		cur.currentAnimalNum -= (int)slider.value;
		game.money += (int)(slider.value * cur.animalPrice);
		game.moneyText.text = "Money: " + game.money;
		FindObjectOfType<PlotScreen>().capacity.text = cur.currentAnimalNum + "/" + cur.levelYields[cur.level];
		if(cur.currentAnimalNum == 1)
			FindObjectOfType<PlotScreen>().sell.gameObject.SetActive(false);
		if(plot.current.GetComponent<Pasture>().currentAnimalNum == 2 || plot.current.GetComponent<Pasture>().currentAnimalNum == 0)
				plot.sell.gameObject.SetActive(false);
		gameObject.SetActive(false);
		
	}

	public void Close(){
		gameObject.SetActive(false);
	}
}
