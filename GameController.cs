using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public int turn;
	public Text yearText;

	public int money;
	public Text moneyText;

	private int oldMoney;

	public GameObject message;
	public Vector3 cameraStart;

	//Field
	public int wheatAmount;
	public int potatoAmount;
	public int carrotAmount;

	private int oldWheatAmount;
	private int oldPotatoAmount;
	private int oldCarrotAmount;

	//Field Text
	public Text wheatText;
	public Text potatoText;
	public Text carrotText;

	//Pasture
	public int sheepAmount;
	public int pigAmount;
	public int cowAmount;

	private int oldSheepAmount;
	private int oldPigAmount;
	private int oldCowAmount;

	//Pasture Text
	public Text sheepText;
	public Text pigText;
	public Text cowText;

	public int[] ungradePrices;

	//All points
	public Field[] fields;
	public Pasture[] pastures;

	public float fieldBlessing = 1;
	public float pastureBlessing = 1;



	//End of turn
	bool check1;
	bool check2;

	//Cars
	public CarController[] cars;
	public float spawnDelay;
	float currentSpawnDelay;
	public Vector3[] carPositions;

	//Other
	public GameObject yearlyReport;
	public GameObject gameOverScreen;
	public GameObject winScreen;

	void Start(){
		currentSpawnDelay = spawnDelay;
		oldMoney = money;

		oldMoney = money;

		oldWheatAmount = wheatAmount;
		oldPotatoAmount = potatoAmount;
		oldCarrotAmount = carrotAmount;

		oldSheepAmount = sheepAmount;
		oldPigAmount = pigAmount;
		oldCowAmount = cowAmount;
	}

	void Update(){
		currentSpawnDelay -= Time.deltaTime;
		if(currentSpawnDelay <= 0){
			var car = Instantiate(cars[Random.Range(0, cars.Length)], carPositions[Random.Range(0,2)], new Quaternion(-90,180,0,1)).GetComponent<CarController>();
			if(car.transform.position == carPositions[0]){
				car.right = true;
			}else{
				car.right = false;
			}
			currentSpawnDelay = spawnDelay;
		}

		yearText.text= "Year "  + (turn);
		moneyText.text = "" + money;
		wheatText.text = "" + wheatAmount;
		potatoText.text = "" + potatoAmount;
		carrotText.text = "" + carrotAmount;
		sheepText.text = "" + sheepAmount;
		pigText.text = "" + pigAmount;
		cowText.text = "" + cowAmount;
	}

	public void NewTurn(){
		FindObjectOfType<WatchAd>().ShowAd();

		FindObjectOfType<Camera>().transform.parent.position = cameraStart;
		FindObjectOfType<AudioSource>().Play();
		
		foreach(Field f in fields){
			if(f.cropName == "Wheat")
				f.Yield();
			else if(f.cropName == "Potato")
				f.Yield();
			else
				f.Yield();
		}
		foreach(Pasture p in pastures){
			if(p.animalName == "Sheep")
				p.Yield();
			else if(p.animalName == "Pig")
				p.Yield();
			else
				p.Yield();
		}
		 
		yearlyReport.SetActive(true);

		yearlyReport.transform.GetChild(1).GetComponent<Text>().text = "Year " + (turn);
		yearlyReport.transform.GetChild(2).GetComponent<Text>().text = 
		"Money: $" + money + " (" + (money-oldMoney).ToString("+#;-#;0")  + ") \n\n"

		+ "Wheat: " + wheatAmount+ " (" + (wheatAmount-oldWheatAmount).ToString("+#;-#;0")  + ") \n"
		+ "Potato: " + potatoAmount + " (" + (potatoAmount-oldPotatoAmount).ToString("+#;-#;0")  + ")\n"
		+ "Carrot: " + carrotAmount + " (" + (carrotAmount-oldCarrotAmount).ToString("+#;-#;0")  + ")\n\n"

		+ "Sheep: " + sheepAmount + " (" + (sheepAmount-oldSheepAmount).ToString("+#;-#;0") + ")\n"
		+ "Pigs: " + pigAmount + " (" + (pigAmount-oldPigAmount).ToString("+#;-#;0") + ")\n"
		+ "Cows: " + cowAmount + " (" + (cowAmount-oldCowAmount).ToString("+#;-#;0")  + ")\n";

		oldMoney = money;

		oldWheatAmount = wheatAmount;
		oldPotatoAmount = potatoAmount;
		oldCarrotAmount = carrotAmount;

		oldSheepAmount = sheepAmount;
		oldPigAmount = pigAmount;
		oldCowAmount = cowAmount;

		turn++;
	}

	public void EndTurn(){
		foreach(Field j in fields){
			if(j.stocked != true && check1 != true){
				if(j.cropName == "Wheat" && sheepAmount == 0)
					break;
				else if(j.cropName == "Potato" && pigAmount == 0)
					break;
				else if(j.cropName == "Carrot" && cowAmount == 0)
					break;
				else{
				Message("Not all of your fields have been replanted. Do your wish to go on anyway?");
				message.transform.GetChild(2).gameObject.SetActive(true);
				message.transform.GetChild(3).gameObject.SetActive(false);
				return;
				}
			}
		}
		foreach(Pasture p in pastures){
			if(p.fed != true && check2 != true && p.currentAnimalNum > 0){
				Message("Not of your animals have been fed. Do you wish to go on anyway?");
				message.transform.GetChild(2).gameObject.SetActive(false);
				message.transform.GetChild(3).gameObject.SetActive(true);
				return;
			}
		}
		if(sheepAmount < 2 && pigAmount < 2 && cowAmount < 2){
			gameOverScreen.SetActive(true);
			Destroy(FindObjectOfType<DontDestroyOnLoad>().gameObject);
		}
		if(turn == 10){
			winScreen.SetActive(true);
		}

		check1 = false;
		check2 = false;
		if(FindObjectOfType<PlotScreen>() != null)
			FindObjectOfType<PlotScreen>().gameObject.SetActive(false);
		Sacrifice();
	}

	public void Sacrifice(){
		FindObjectOfType<GodController>().Sacrifice();
	}

	public void Message(string mess){
		message.SetActive(true);
		message.transform.GetChild(1).GetComponent<Text>().text = mess;
	}

	public void Check1Yes(){
		check1 = true;
		message.SetActive(false);
		EndTurn();
	}
	public void Check2Yes(){
		check2 = true;
		message.SetActive(false);
		EndTurn();
	}
	public void CheckNo(){
		check1 = false;
		check2 = false;
		message.SetActive(false);
	}

	public void YearlyReportClose(){
		yearlyReport.SetActive(false);
	}

	public void BackToMenu(){
		SceneManager.LoadScene("Menu");
	}
}