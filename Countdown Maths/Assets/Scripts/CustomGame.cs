using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomGame : MonoBehaviour {

	public static int[] numberArray = new int[6];
	public static int[] transferArray = new int[6];
	int maxAmountOfNumbers = 6;
	int numberOfHighNumbers = 0;
	int numberOfSmall = 0;
	int currentPosition = 0;

	public GameObject MainMenu;
	public GameObject CustomGameSetup;
	public GameObject GameCanvas;

	public GameObject bigNumberAlert;
	public GameObject enoughNumbersAlert;




	public Text number1;
	public Text number2;
	public Text number3;
	public Text number4;
	public Text number5;
	public Text number6;

	public static bool reset = false;


	// Use this for initialization
	void Start () {
		ClearArray ();
		DisplayNumbers ();
		reset = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (reset) {
			Start ();
		}
		DisplayNumbers ();

	}

	public void OnHighClick(){
		if (currentPosition < maxAmountOfNumbers && numberOfHighNumbers < 4) {
			addNumberToArray (GenerateHighNumber ());
			numberOfHighNumbers++;
		} else if (numberOfHighNumbers >= 4) {
			bigNumberAlert.SetActive (true);
		} else if (currentPosition >= maxAmountOfNumbers) {
			enoughNumbersAlert.SetActive(true);
		}
	}

	public void OnLowClick(){
		if (currentPosition < maxAmountOfNumbers) {
			addNumberToArray(GenerateLowNumber());
			numberOfSmall++;
		}else if (currentPosition >= maxAmountOfNumbers) {
			enoughNumbersAlert.SetActive(true);
		}
	}

	void DisplayNumbers(){
		if (numberArray [0] != 0) {
			number1.text = numberArray [0].ToString ();
		} else {
			number1.text = "";
		}
		if (numberArray [1] != 0) {
			number2.text = numberArray [1].ToString ();
		}else {
			number2.text = "";
		}
		if (numberArray [2] != 0) {
			number3.text = numberArray [2].ToString ();
		}else {
			number3.text = "";
		}
		if (numberArray [3] != 0) {
			number4.text = numberArray [3].ToString ();
		}else {
			number4.text = "";
		}
		if (numberArray [4] != 0) {
			number5.text = numberArray [4].ToString ();
		}else {
			number5.text = "";
		}
		if (numberArray [5] != 0) {
			number6.text = numberArray [5].ToString ();
		}else {
			number6.text = "";
		}
	}

	void addNumberToArray(int number){
		if (currentPosition < 6) {
			numberArray [currentPosition] = number;
			currentPosition++;
		}



	}

	int GenerateHighNumber(){
		int random = Random.Range (1, 5);
		int number = 0;
		switch (random) {
		case 1:
			if(!checkIfArrayContains(25)){
			number = 25;
			}else{
				number = GenerateHighNumber();
			}
			break;
		case 2:
			if(!checkIfArrayContains(50)){
				number = 50;
			}else{
				number = GenerateHighNumber();
			}
			break;
		case 3: 
			if(!checkIfArrayContains(75)){
				number = 75;
			}else{
				number = GenerateHighNumber();
			}
			break;
		case 4:
			if(!checkIfArrayContains(100)){
				number = 100;
			}else{
				number = GenerateHighNumber();
			}
			break;
		}

		return number;

	}

	int generateHighNumberAtEnd(){
		int random = Random.Range (1,5);
		switch (random) {
		case 1:
			return 25;
		case 2:
			return 50;
		case 3:
			return 75;
		case 4:
			return 100;
		}
		return 0;

	}

	int GenerateLowNumber(){
		int number = 0;
		int random = Random.Range (1, 11);

		switch (random) {
		case 1:
			if(!checkIfArrayContains(1)){
				number = 1;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 2:
			if(!checkIfArrayContains(2)){
				number = 2;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 3:
			if(!checkIfArrayContains(3)){
				number = 3;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 4:
			if(!checkIfArrayContains(4)){
				number = 4;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 5:
			if(!checkIfArrayContains(5)){
				number = 5;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 6:
			if(!checkIfArrayContains(6)){
				number = 6;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 7:
			if(!checkIfArrayContains(7)){
				number = 7;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 8:
			if(!checkIfArrayContains(8)){
				number = 8;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 9:
			if(!checkIfArrayContains(9)){
				number = 9;
			}else{
				number = GenerateLowNumber();
			}
			break;
		case 10:
			if(!checkIfArrayContains(10)){
				number = 10;
			}else{
				number = GenerateLowNumber();
			}
			break;

		}

		return number;

	}

	void ClearArray(){

		for (int i = 0; i < maxAmountOfNumbers; i++) {
			numberArray[i] = 0;
		}
			}
	

	public void OnCustomGameSetupBackButtonClick(){
		ClearArray ();
		DisplayNumbers ();
		numberOfHighNumbers = 0;
		currentPosition = 0;
		MainMenu.SetActive(true);
		CustomGameSetup.SetActive(false);

		
	}

	public void OnCustomGameSetupConfirmButtonClick(){
		for (int i = 0; i < numberArray.Length; i++) {
			Debug.Log(numberArray[i] + " before");
		}
		Game.numberOfBigs = numberOfHighNumbers;
		Game.numberOfSmalls = numberOfSmall;
		currentPosition = 0;
		numberOfHighNumbers = 0;
		numberOfSmall = 0;
		GameCanvas.SetActive (true);
		Game.numberArray = numberArray;
		Game.reset = true;
		Game.startTime = Time.time;
		CustomGameSetup.SetActive (false);

	}

	public void OnBigNumberAlertOkButtonClick(){
		bigNumberAlert.SetActive (false);
	}

	bool checkIfArrayContains(int number){
		for (int i = 0; i<maxAmountOfNumbers; i++) {
			if(numberArray[i] == number){
				return true;
			}

		}
		return false;

	}

	public void OnEnoughNumbersAlertOkButtonClick(){
		enoughNumbersAlert.SetActive (false);
	}




}
