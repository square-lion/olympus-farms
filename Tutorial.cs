using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	private int line = 0;
	public Text text;

	public GameObject animBox;
	public GameObject cropBox;

	public string[] lines = {
		"Hello! Welcome to Olympus Farms! You just bought this farm! Hooray!",
		"You have the deed to this farm for 10 years.",
		"Each year, you must feed your sheep, pigs, and cows.",
		"You must also replant your wheat, potatoes, and carrots.",
		"You must use the wheat, potatoes, and carrots to feed your animals. Wheat feeds sheep, potatoes feed pigs, and carrots feed cows.",
		"You must also sell your animals to get money. Money buys more seeds and upgrades for your farm.",
		"You can upgrade your animal pastures to fit more animals. And you can upgrade your fields to get more crops.",
		"That's about it! Click the arrow when you want to go to the next year. Have fun! I'll see you in 10 years."};

	void Awake(){
		text.text = lines[line];
	}

	public void nextLine(){
		line++;
		if(line == 8){
			SceneManager.LoadScene("Game");
			return;
		}
		if(line == 2)
			animBox.SetActive(true);
		else
			animBox.SetActive(false);

		if(line == 3)
			cropBox.SetActive(true);
		else
			cropBox.SetActive(false);

		text.text = lines[line];
	}
}
