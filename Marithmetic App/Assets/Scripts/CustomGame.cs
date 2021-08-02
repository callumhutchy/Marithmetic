using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
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

	public GameObject alertPanel;
	public TMP_Text alertText;
	public string bigNumberAlertString;
	public string enoughNumbersAlertString;

	public GameObject EasyButton;
	public GameObject HardButton;

	public GameObject EasyDisabledButton;
	public GameObject HardDisabledButton;

	public Text number1;
	public Text number2;
	public Text number3;
	public Text number4;
	public Text number5;
	public Text number6;

	Text[] numberTexts;

	public static bool reset = false;

	// Use this for initialization
	void Start () {

		numberTexts = new Text[] { number1, number2, number3, number4, number5, number6 };

		ClearArray ();
		DisplayNumbers ();
		reset = false;
		OnEasyButtonClick ();
	}

	// Update is called once per frame
	void Update () {
		if (reset) {
			Start ();
		}
		DisplayNumbers ();

	}

	public void OnHighClick () {
		if (currentPosition < maxAmountOfNumbers && numberOfHighNumbers < 4) {
			addNumberToArray (GenerateHighNumber ());
			numberOfHighNumbers++;
		} else if (numberOfHighNumbers >= 4) {
			alertText.text = bigNumberAlertString;
			alertPanel.SetActive (true);
		} else if (currentPosition >= maxAmountOfNumbers) {
			alertText.text = enoughNumbersAlertString;
			alertPanel.SetActive (true);
		}
	}

	public void OnLowClick () {
		if (currentPosition < maxAmountOfNumbers) {
			addNumberToArray (GenerateLowNumber ());
			numberOfSmall++;
		} else if (currentPosition >= maxAmountOfNumbers) {
			alertText.text = enoughNumbersAlertString;
			alertPanel.SetActive (true);
		}
	}

	void DisplayNumbers () {
		for(int i =0; i < 6;i++){
			numberTexts[i].text = (numberArray[i] != 0) ? numberArray[i].ToString() : "";
		}
	}

	void addNumberToArray (int number) {
		if (currentPosition < 6) {
			numberArray[currentPosition] = number;
			currentPosition++;
		}
	}

	int GenerateHighNumber () {
		int random = Random.Range (0, 4);
		int[] bigNumbers = new int[] { 25, 50, 75, 100 };
		return checkIfArrayContainsBig (bigNumbers[random]);
	}

	int GenerateLowNumber () {
		int random = Random.Range (1, 11);
		return checkIfArrayContainsLow (random);
	}

	void ClearArray () {
		for (int i = 0; i < maxAmountOfNumbers; i++) {
			numberArray[i] = 0;
		}
	}

	public void OnCustomGameSetupBackButtonClick () {
		ClearArray ();
		DisplayNumbers ();
		numberOfHighNumbers = 0;
		currentPosition = 0;
		MainMenu.SetActive (true);
		CustomGameSetup.SetActive (false);

	}

	public void OnCustomGameSetupConfirmButtonClick () {
		for (int i = 0; i < numberArray.Length; i++) {
			Debug.Log (numberArray[i] + " before");
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

	public void OnBigNumberAlertOkButtonClick () {
		alertPanel.SetActive (false);
	}

	int checkIfArrayContainsBig (int number) {
		if (!numberArray.Contains (number)) {
			return number;
		} else {
			return GenerateHighNumber ();
		}
	}

	int checkIfArrayContainsLow (int number) {
		if (!numberArray.Contains (number)) {
			return number;
		} else {
			return GenerateLowNumber ();
		}
	}

	public void OnEnoughNumbersAlertOkButtonClick () {
		alertPanel.SetActive (false);
	}

	public void OnEasyButtonClick () {
		EasyDisabledButton.SetActive (true);
		PlayerPrefs.SetString (References.DIFFICULTY_LEVEL, "easy");
		HardDisabledButton.SetActive (false);
	}
	public void OnHardButtonClick () {
		HardDisabledButton.SetActive (true);
		PlayerPrefs.SetString (References.DIFFICULTY_LEVEL, "hard");
		EasyDisabledButton.SetActive (false);
	}
}