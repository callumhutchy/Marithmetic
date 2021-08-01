using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Game : MonoBehaviour {

	public static int[] numberArray = new int[6];
	ArrayList totalArrayList = new ArrayList();

	public GameObject GameCanvas;
	public GameObject MainMenuCanvas;
	public GameObject GameCompleteCanvas;

	public static bool reset = false;


	public static int numberOfBigs = 0;
	public static int numberOfSmalls = 0;

	public Text number1, number2, number3, number4, number5, number6;

	public Text number1Disabled, number2Disabled, number3Disabled, number4Disabled, number5Disabled, number6Disabled;

	public Text mathsTextArea;
	string mathsTextString = "";

	int mathsTextLine = 1;
	int currentTextNumber = 0;

	public GameObject button1, button2, button3, button4, button5, button6;

	public GameObject button1Disabled, button2Disabled, button3Disabled, button4Disabled, button5Disabled, button6Disabled;

	public GameObject button1Grey, button2Grey, button3Grey, button4Grey, button5Grey, button6Grey;

	public GameObject addButton, subtractButton, multiplyButton, divideButton;

	public GameObject addButtonDisabled, subtractButtonDisabled, multiplyButtonDisabled, divideButtonDisabled;

	public GameObject subtractAlert;
	public GameObject divisionAlert;
	public GameObject homeAlert;

	public GameObject timeTotal;

	GameObject tempButton1;
	GameObject tempButton1Disabled;
	GameObject tempButton2;
	GameObject tempButton2Disabled;
	GameObject tempButton2Grey;
	GameObject tempOperationButton;
	GameObject tempOperationButtonDisabled;

	bool number1selected = false;
	bool operationselected = false;
	bool number2selected = false;


	int userNumber1 = 0;
	int userNumber2 = 0;
	string userOperation;
	int usersTotal = 0;

	public static float startTime;
	float updatedTime;
	float finalTime;
	string textTime;


	int totalNumber;
	public Text totalNumberText;

	public void OnGameBackButtonClick(){
		homeAlert.SetActive (true);

	}


	// Use this for initialization
	void Start () {
		Reset ();
		totalNumber = GenerateSuitableNumber ();
		totalNumberText.text = totalNumber.ToString ();

		reset = false;

	}

	void Reset(){
		totalArrayList.Clear (); 
		numberArray = CustomGame.numberArray;
		number1selected = operationselected = number2selected = false;
		ResetButtons ();
		ResetTextArea ();
		
		for (int i = 0; i< numberArray.Length; i++) {
			
			totalArrayList.Add(numberArray[i]);
			
		}
		DisplayNumbers ();
		//	totalArrayList = ShuffleList (totalArrayList);
		totalArrayList.CopyTo (numberArray);

	}

	ArrayList ShuffleList(ArrayList numbers){
		ArrayList randomList = new ArrayList();


		int randomIndex = 0;
		while (numbers.Count > 0) {
			randomIndex = Random.Range(0,numbers.Count);
			randomList.Add(numbers[randomIndex]);
			numbers.RemoveAt(randomIndex);

		}
		return randomList;
	}

	void ClearArray(){
		numberArray = new int[6];
	}

	void ConvertTime(){


	}


	// Update is called once per frame
	void Update () {

		if (reset) {
			ClearArray();
			Start();
			reset = false;
			startTime = Time.time;
			
		}

		updatedTime = Time.time - startTime;

		Debug.Log (updatedTime.ToString ());
		int minutes = (int) updatedTime / 60;
		int seconds = (int) updatedTime % 60;

		textTime = string.Format ("{0:00}:{1:00}", minutes, seconds);

		timeTotal.GetComponentInChildren<Text> ().text = textTime;



		if (number1selected && operationselected && number2selected) {

			usersTotal = applyOperation();
			if(usersTotal == totalNumber){
				ClearArray ();
				DisplayNumbers ();

				if(PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) < 100 && totalNumber >= 100){
					PlayerPrefs.SetInt(References.LOWEST_NUMBER_SOLVED, totalNumber);
				}else if(PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) >= 100 && PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) > totalNumber){
					PlayerPrefs.SetInt(References.LOWEST_NUMBER_SOLVED, totalNumber);
				}
				if(PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) < 100 && totalNumber >= 100){
					PlayerPrefs.SetInt(References.HIGHEST_NUMBER_SOLVED, totalNumber);
				}else if(PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) >= 100 && PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) < totalNumber){
					PlayerPrefs.SetInt(References.HIGHEST_NUMBER_SOLVED, totalNumber);
				}

				PlayerPrefs.SetInt(References.SMALL_NUMBERS_USED, PlayerPrefs.GetInt(References.SMALL_NUMBERS_USED) + numberOfSmalls);

				PlayerPrefs.SetInt(References.BIG_NUMBERS_USED, PlayerPrefs.GetInt(References.BIG_NUMBERS_USED) + numberOfBigs);

				finalTime = updatedTime;

				if(PlayerPrefs.GetInt(References.FASTEST_TIME) > (int) finalTime ){
					PlayerPrefs.SetInt(References.FASTEST_TIME, (int) finalTime);
				}else if(PlayerPrefs.GetInt(References.FASTEST_TIME) < 1){
					PlayerPrefs.SetInt(References.FASTEST_TIME, (int) finalTime);

				}

				PlayerPrefs.SetInt(References.RECENT_TIME, (int)finalTime);


				if(numberOfBigs == 4){
					PlayerPrefs.SetInt(References.HOW_MANY_BIG, PlayerPrefs.GetInt(References.HOW_MANY_BIG) + 1);
				}



				PlayerPrefs.SetInt(References.TOTAL_NUMBERS_SOLVED, PlayerPrefs.GetInt(References.TOTAL_NUMBERS_SOLVED) + 1);
				Reset ();
				ClearArray ();
				DisplayNumbers ();
				GameCompleteCanvas.SetActive(true);
				GameCanvas.SetActive(false);

			}

			if(usersTotal == -1){
				subtractAlert.SetActive(true);
				ClearLastLine();
				tempButton1Disabled.SetActive(false);
				tempButton2Disabled.SetActive(false);
				tempOperationButtonDisabled.SetActive(false);
				tempOperationButton.GetComponent<Button>().enabled = true;
				tempButton1.GetComponent<Button>().enabled = true;
				tempButton2.GetComponent<Button>().enabled = true;
				number1selected = operationselected = number2selected = false;
			}else if(usersTotal == -2){

				divisionAlert.SetActive(true);
				ClearLastLine();
				tempButton1Disabled.SetActive(false);
				tempButton2Disabled.SetActive(false);
				tempOperationButtonDisabled.SetActive(false);
				tempOperationButton.GetComponent<Button>().enabled = true;
				tempButton1.GetComponent<Button>().enabled = true;
				tempButton2.GetComponent<Button>().enabled = true;
				number1selected = operationselected = number2selected = false;

			}else{

				tempButton1.GetComponentInChildren<Text>().text = usersTotal.ToString();
				tempButton1Disabled.GetComponentInChildren<Text>().text = usersTotal.ToString();
				tempButton1Disabled.SetActive(false);
				tempButton1.GetComponent<Button>().enabled = true;
				tempButton2Disabled.SetActive(false);
				tempButton2Grey .SetActive(true);
				tempOperationButtonDisabled.SetActive(false);
				tempOperationButton.GetComponent<Button>().enabled = true;
				addToTextArea("= " + usersTotal.ToString());
				number1selected = operationselected = number2selected = false;

			}



		}


	}

	void DisplayNumbers(){
		if (numberArray [0] != 0) {
			number1.text = numberArray [0].ToString ();
			number1Disabled.text = numberArray [0].ToString ();
		} else {
			number1.text = "";
		}
		if (numberArray [1] != 0) {
			number2.text = numberArray [1].ToString ();
			number2Disabled.text = numberArray [1].ToString ();
		}else {
			number2.text = "";
		}
		if (numberArray [2] != 0) {
			number3.text = numberArray [2].ToString ();
			number3Disabled.text = numberArray [2].ToString ();
		}else {
			number3.text = "";
		}
		if (numberArray [3] != 0) {
			number4.text = numberArray [3].ToString ();
			number4Disabled.text = numberArray [3].ToString ();
		}else {
			number4.text = "";
		}
		if (numberArray [4] != 0) {
			number5.text = numberArray [4].ToString ();
			number5Disabled.text = numberArray [4].ToString ();
		}else {
			number5.text = "";
		}
		if (numberArray [5] != 0) {
			number6.text = numberArray [5].ToString ();
			number6Disabled.text = numberArray [5].ToString ();
		}else {
			number6.text = "";
		}
	}

	int GenerateSuitableNumber(){
		bool notSuitable = true;


		while (notSuitable) {
			int tempNumber = GenerateTotalNumber ();
			if (tempNumber < 1000) { 
				Debug.Log ("Its less than 1000");
				if (tempNumber > 99) {
					Debug.Log ("It was suitable");
					return tempNumber;
				}
				Debug.Log (tempNumber + " lol");
				Debug.Log ("Recreate");
			}

		}
		return 1;
	}

	int GenerateTotalNumber(){
			bool listContains1 = false;
			int returnValue = 0;
		ArrayList tempArray = new ArrayList();
		tempArray.Clear ();
		tempArray.AddRange (totalArrayList);
			while(!listContains1){
			Debug.Log("1 ArrayList count = " + tempArray.Count);
			int index1 = Random.Range(0, tempArray.Count);
			int temp1 = (int) tempArray[index1];
			tempArray.RemoveAt(index1);
			Debug.Log("2 ArrayList count = " + tempArray.Count);
			int index2 = Random.Range(0, tempArray.Count);
			int temp2 = (int) tempArray[index2];
			tempArray.RemoveAt(index2);
			Debug.Log("3 ArrayList count = " + tempArray.Count);

			int newNumber = applyRandomOperation(temp1, temp2);
			tempArray.Add(newNumber);
			Debug.Log("4 ArrayList count = " + tempArray.Count);




			if(tempArray.Count < 2){
					listContains1 = true;
				returnValue = (int) tempArray[tempArray.Count-1];
				}

			}
		Debug.Log (returnValue);
		return returnValue;



	}

	int applyRandomOperation(int number1, int number2){
		if (PlayerPrefs.GetString (References.DIFFICULTY_LEVEL) == "easy") {
			
			int randomOperation = Random.Range (1, 3);
			switch(randomOperation){
			case 1:
				return addOperation(number1,number2);
			case 2:
				if(number1 - number2 > 0){
					return subtractOperation(number1,number2);
				}else if(number2 - number1 > 0){
					return subtractOperation(number2,number1);
				}else{
					return addOperation(number1,number2);
				}
			}
		}
		else if(PlayerPrefs.GetString(References.DIFFICULTY_LEVEL) == "medium"){
			int randomOperation = Random.Range (1, 4);
			switch(randomOperation){
			case 1:
				return addOperation(number1,number2);
			case 2:
				if(number1 - number2 > 0){
					return subtractOperation(number1,number2);
				}else if(number2 - number1 > 0){
					return subtractOperation(number2,number1);
				}else{
					int randomOperation2 = Random.Range(1,3);
					switch(randomOperation2){
					case 1:
						return addOperation(number1,number2);
					case 2:
						return multiplyOperation(number1, number2);
					}
				}
				return 1;
			case 3:
				return multiplyOperation(number1,number2);
			}
		}
		else if(PlayerPrefs.GetString(References.DIFFICULTY_LEVEL) == "hard"){
			int randomOperation = Random.Range (1, 4);
			switch(randomOperation){
			
			case 1:
				if(number1 - number2 > 0){
					return subtractOperation(number1,number2);
				}else if(number2 - number1 > 0){
					return subtractOperation(number2,number1);
				}else{
						return multiplyOperation(number1, number2);
				}
				return 1;
			case 2:
				return multiplyOperation(number1,number2);
			case 3:
				if(number1/number2 > 1 && number2/number1 > 1){
					return divideOperation(number1,number2);
				}else if(number1-number2>0||number2-number1>0){
					int randomOperation2 = Random.Range(1,3);
					switch(randomOperation2){
					case 1:
						return multiplyOperation(number1, number2);
					case 2:
						return subtractOperation(number1,number2);
					}
				}
				return 1;
			}
		}
		return 1;
	}

	int applyOperation(){
		switch (userOperation) {
		case "add":
			return userNumber1 + userNumber2;
		case "subtract":
					if(userNumber1 - userNumber2 > 0){
			return userNumber1 - userNumber2;
					}else{
						return -1;
					}
		case "multiply":
			return userNumber1 * userNumber2;
		case "divide":
			if(userNumber1 % userNumber2 == 0){
				return userNumber1 / userNumber2;
			}else{
				return -2;
			}

		default:
			return 1;

		}

	}

	int addOperation(int number1, int number2){
		return number1 + number2;
	}

	int multiplyOperation(int number1, int number2){
		return number1 * number2;
	}

	int subtractOperation(int number1,int number2){
		if (number1 - number2 < 0 && number2 - number1 > 0) {
			return number2 - number1;
		} else {
			return number1 - number2;
		}
	}

	int divideOperation(int number1, int number2){
		if (number1 % number2 == 0 && number1 / number2 >=1) {
			return number1 / number2;
		} else if (number2 % number1 == 0 && number2 / number1 >=1) {
			return number2 / number1;
		} else {
			return number1 /number2;
		}

	}

	void ResetTextArea(){
		mathsTextString = "";
		mathsTextArea.text = mathsTextString;
		currentTextNumber = 0;
		mathsTextLine = 1;

	}

	void ClearLastLine(){
		mathsTextLine--;
		currentTextNumber = 0;
		mathsTextString = mathsTextString.Substring (0, mathsTextString.LastIndexOf (tempButton1.GetComponentInChildren<Text>().text.ToString() + " " + tempOperationButton.GetComponentInChildren<Text>().text.ToString() + " " + tempButton2.GetComponentInChildren<Text>().text.ToString() + " "));
		Debug.Log (mathsTextString);
		mathsTextArea.text = mathsTextString;
	}

	void addToTextArea(string text){
		mathsTextString += text.ToString() + " ";
		currentTextNumber++;
		if (currentTextNumber == 4) {
			mathsTextLine++;
			mathsTextString = mathsTextString + "\n";
			currentTextNumber = 0;
		}
		Debug.Log (mathsTextString);
		mathsTextArea.text = mathsTextString;
	}

	void takeAwayText(string removeChar){
		mathsTextString = mathsTextString.Substring (0, mathsTextString.LastIndexOf (removeChar + " "));
		currentTextNumber--;
		Debug.Log (mathsTextString);
		mathsTextArea.text = mathsTextString;

	}

	public void ResetButtons(){
		button1.SetActive (true);
		button1.GetComponent<Button> ().enabled = true;
		button1Disabled.SetActive (false);
		button1Grey.SetActive (false);
		button2.SetActive (true);
		button2.GetComponent<Button> ().enabled = true;
		button2Disabled.SetActive (false);
		button2Grey.SetActive (false);
		button3.SetActive (true);
		button3.GetComponent<Button> ().enabled = true;
		button3Disabled.SetActive (false);
		button3Grey.SetActive (false);
		button4.SetActive (true);
		button4.GetComponent<Button> ().enabled = true;
		button4Disabled.SetActive (false);
		button4Grey.SetActive (false);
		button5.SetActive (true);
		button5.GetComponent<Button> ().enabled = true;
		button5Disabled.SetActive (false);
		button5Grey.SetActive (false);
		button6.SetActive (true);
		button6.GetComponent<Button> ().enabled = true;
		button6Disabled.SetActive (false);
		button6Grey.SetActive (false);
		addButton.SetActive (true);
		addButton.GetComponent<Button> ().enabled = true;
		addButtonDisabled.SetActive (false);
		subtractButton.SetActive (true);
		subtractButton.GetComponent<Button> ().enabled = true;
		subtractButtonDisabled.SetActive (false);
		multiplyButton.SetActive (true);
		multiplyButton.GetComponent<Button> ().enabled = true;
		multiplyButtonDisabled.SetActive (false);
		divideButton.SetActive (true);
		divideButton.GetComponent<Button> ().enabled = true;
		divideButtonDisabled.SetActive (false);

	}

	public void OnNumberButton1Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button1.GetComponent<Button> ().enabled = false;
			addToTextArea(button1.GetComponentInChildren<Text> ().text);
			button1Disabled.SetActive (true);
			number1selected = true;
			tempButton1 = button1;
			tempButton1Disabled = button1Disabled;
			userNumber1 = int.Parse (button1.GetComponentInChildren<Text> ().text);
		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button1.GetComponent<Button> ().enabled = false;
			addToTextArea(button1.GetComponentInChildren<Text> ().text);
			button1Disabled.SetActive (true);
			number2selected = true;
			tempButton2 = button1;
			tempButton2Disabled = button1Disabled;
			tempButton2Grey = button1Grey;
			userNumber2 = int.Parse (button1.GetComponentInChildren<Text> ().text);
		} 
	}
	public void OnNumberButton2Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button2.GetComponent<Button> ().enabled = false;
			addToTextArea(button2.GetComponentInChildren<Text> ().text);
			button2Disabled.SetActive (true);
			number1selected = true;
			tempButton1 = button2;
			tempButton1Disabled = button2Disabled;
			userNumber1 = int.Parse (button2.GetComponentInChildren<Text> ().text);

		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button2.GetComponent<Button> ().enabled = false;
			addToTextArea(button2.GetComponentInChildren<Text> ().text);
			button2Disabled.SetActive (true);
			number2selected = true;
			tempButton2 = button2;
			tempButton2Disabled = button2Disabled;
			tempButton2Grey = button2Grey;
			userNumber2 = int.Parse (button2.GetComponentInChildren<Text> ().text);
		}  

	}
	public void OnNumberButton3Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button3.GetComponent<Button> ().enabled = false;
			addToTextArea(button3.GetComponentInChildren<Text> ().text);
			button3Disabled.SetActive(true);
			number1selected = true;
			tempButton1 = button3;
			tempButton1Disabled = button3Disabled;
			userNumber1 = int.Parse( button3.GetComponentInChildren<Text>().text);
		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button3.GetComponent<Button> ().enabled = false;
			addToTextArea(button3.GetComponentInChildren<Text> ().text);
			button3Disabled.SetActive(true);
			number2selected = true;
			tempButton2 = button3;
			tempButton2Disabled = button3Disabled;
			tempButton2Grey = button3Grey;
			userNumber2 = int.Parse( button3.GetComponentInChildren<Text>().text);
		}
	}
	public void OnNumberButton4Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button4.GetComponent<Button> ().enabled = false;
			addToTextArea(button4.GetComponentInChildren<Text> ().text);
			button4Disabled.SetActive(true);
			number1selected = true;
			tempButton1 = button4;
			tempButton1Disabled = button4Disabled;
			userNumber1 = int.Parse( button4.GetComponentInChildren<Text>().text);
		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button4.GetComponent<Button> ().enabled = false;
			addToTextArea(button4.GetComponentInChildren<Text> ().text);
			button4Disabled.SetActive(true);
			number2selected = true;
			tempButton2 = button4;
			tempButton2Disabled = button4Disabled;
			tempButton2Grey = button4Grey;
			userNumber2 = int.Parse( button4.GetComponentInChildren<Text>().text);
		}
	}
	public void OnNumberButton5Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button5.GetComponent<Button> ().enabled = false;
			addToTextArea(button5.GetComponentInChildren<Text> ().text);
			button5Disabled.SetActive(true);
			number1selected = true;
			tempButton1 = button5;
			tempButton1Disabled = button5Disabled;
			userNumber1 = int.Parse(button5.GetComponentInChildren<Text>().text);
		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button5.GetComponent<Button> ().enabled = false;
			addToTextArea(button5.GetComponentInChildren<Text> ().text);
			button5Disabled.SetActive(true);
			number2selected = true;
			tempButton2 = button5;
			tempButton2Disabled = button5Disabled;
			tempButton2Grey = button5Grey;
			userNumber2 = int.Parse( button5.GetComponentInChildren<Text>().text);
		}
	}
	public void OnNumberButton6Click(){

		if (number1selected == false && operationselected == false && number2selected == false) {
			button6.GetComponent<Button> ().enabled = false;
			addToTextArea(button6.GetComponentInChildren<Text> ().text);
			button6Disabled.SetActive(true);
			number1selected = true;
			tempButton1 = button6;
			tempButton1Disabled = button6Disabled;
			userNumber1 = int.Parse( button6.GetComponentInChildren<Text>().text);
		} else if (number1selected == true && operationselected == true && number2selected == false) {
			button6.GetComponent<Button> ().enabled = false;
			addToTextArea(button6.GetComponentInChildren<Text> ().text);
			button6Disabled.SetActive(true);
			number2selected = true;
			tempButton2 = button6;
			tempButton2Disabled = button6Disabled;
			tempButton2Grey = button6Grey;
			userNumber2 = int.Parse( button6.GetComponentInChildren<Text>().text);
		}
	}

	public void OnNumberButton1DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button1.GetComponent<Button> ().enabled = true;
			takeAwayText(button1.GetComponentInChildren<Text>().text);
			button1Disabled.SetActive (false);
			number1selected = false;
		}
	}
	public void OnNumberButton2DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button2.GetComponent<Button> ().enabled = true;
			takeAwayText(button2.GetComponentInChildren<Text>().text);
			button2Disabled.SetActive (false);
			number1selected = false;
		}
	}
	public void OnNumberButton3DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button3.GetComponent<Button> ().enabled = true;
			takeAwayText(button3.GetComponentInChildren<Text>().text);
			button3Disabled.SetActive (false);
			number1selected = false;
		}
	}
	public void OnNumberButton4DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button4.GetComponent<Button> ().enabled = true;
			takeAwayText(button4.GetComponentInChildren<Text>().text);
			button4Disabled.SetActive (false);
			number1selected = false;
		}
	}
	public void OnNumberButton5DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button5.GetComponent<Button> ().enabled = true;
			takeAwayText(button5.GetComponentInChildren<Text>().text);
			button5Disabled.SetActive (false);
			number1selected = false;
		}
	}
	public void OnNumberButton6DisabledClick(){
		if (number1selected == true && operationselected == false && number2selected == false) {
			button6.GetComponent<Button> ().enabled = true;
			takeAwayText(button6.GetComponentInChildren<Text>().text);
			button6Disabled.SetActive (false);
			number1selected = false;
		}
	}

	public void OnAddButtonClick(){
		if (number1selected && !operationselected && !number2selected) {	
			addButton.GetComponent<Button> ().enabled = false;
			addToTextArea(addButton.GetComponentInChildren<Text> ().text);
			addButtonDisabled.SetActive (true);
			operationselected = true;
			tempOperationButton = addButton;
			tempOperationButtonDisabled = addButtonDisabled;
			userOperation = "add";
		} else if (operationselected) {
			addButton.GetComponent<Button>().enabled = true;
			addButtonDisabled.SetActive(false);
			operationselected = false;

		}

	}
	public void OnSubtractButtonClick(){
		if (number1selected && !operationselected && !number2selected) {
			subtractButton.GetComponent<Button> ().enabled = false;
			addToTextArea(subtractButton.GetComponentInChildren<Text> ().text);
			subtractButtonDisabled.SetActive(true);
			operationselected = true;
			tempOperationButton = subtractButton;
			tempOperationButtonDisabled = subtractButtonDisabled;
			userOperation = "subtract";
		}else if (operationselected) {
			subtractButton.GetComponent<Button>().enabled = true;
			subtractButtonDisabled.SetActive(false);
			operationselected = false;
			
		}
	}
	public void OnMultiplyButtonClick(){
		if (number1selected && !operationselected && !number2selected) {
			multiplyButton.GetComponent<Button> ().enabled = false;
			addToTextArea(multiplyButton.GetComponentInChildren<Text> ().text);
			multiplyButtonDisabled.SetActive(true);
			operationselected = true;
			tempOperationButton = multiplyButton;
			tempOperationButtonDisabled = multiplyButtonDisabled;
			userOperation = "multiply";
		}else if (operationselected) {
			multiplyButton.GetComponent<Button>().enabled = true;
			multiplyButtonDisabled.SetActive(false);
			operationselected = false;
			
		}
	}
	public void OnDivideButtonClick(){
		if (number1selected && !operationselected && !number2selected) {
			divideButton.GetComponent<Button> ().enabled = false;
			addToTextArea(divideButton.GetComponentInChildren<Text> ().text);
			divideButtonDisabled.SetActive(true);
			operationselected = true;
			tempOperationButton = divideButton;
			tempOperationButtonDisabled = divideButtonDisabled;
			userOperation = "divide";
		}else if (operationselected) {
			divideButton.GetComponent<Button>().enabled = true;
			divideButtonDisabled.SetActive(false);
			operationselected = false;
			
		}
	}

	public void OnAddButtonDisabledClick(){
		if (number1selected == true && operationselected == true && number2selected == false) {
			addButton.GetComponent<Button> ().enabled = true;
			takeAwayText(addButton.GetComponentInChildren<Text>().text);
			addButtonDisabled.SetActive (false);
			operationselected = false;
		}
	}
	public void OnSubtractButtonDisabledClick(){
		if (number1selected == true && operationselected == true && number2selected == false) {
			subtractButton.GetComponent<Button> ().enabled = true;
			takeAwayText(subtractButton.GetComponentInChildren<Text>().text);
			subtractButtonDisabled.SetActive (false);
			operationselected = false;
		}
	}
	public void OnMultiplyButtonDisabledClick(){
		if (number1selected == true && operationselected == true && number2selected == false) {
			multiplyButton.GetComponent<Button> ().enabled = true;
			takeAwayText(multiplyButton.GetComponentInChildren<Text>().text);
			multiplyButtonDisabled.SetActive (false);
			operationselected = false;
		}
	}
	public void OnDivideButtonDisabledClick(){
		if (number1selected == true && operationselected == true && number2selected == false) {
			divideButton.GetComponent<Button> ().enabled = true;
			takeAwayText(divideButton.GetComponentInChildren<Text>().text);
			divideButtonDisabled.SetActive (false);
			operationselected = false;
		}
	}


	public void OnSubtractAlertOkButtonClick(){
		subtractAlert.SetActive (false);
	}
	public void OnDivisionAlertOkButtonClick(){
		divisionAlert.SetActive (false);
	}

	public void OnHomeAlertYesButtonClick(){
		ClearArray ();
		DisplayNumbers ();
		Reset ();
		homeAlert.SetActive (false);
		MainMenuCanvas.SetActive (true);
		GameCanvas.SetActive (false);
	}
	public void OnHomeAlertNoButtonClick(){
		homeAlert.SetActive (false);
	}

	public void OnResetButtonClick(){
		//Start ();
		PlayerPrefs.SetInt (References.NUMBER_OF_RESETS, PlayerPrefs.GetInt (References.NUMBER_OF_RESETS) + 1);
		Reset ();
	}




}
