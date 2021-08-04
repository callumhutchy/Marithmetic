using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public static int[] numberArray = new int[6];
    ArrayList totalArrayList = new ArrayList();

    public GameObject GameCanvas;
    public GameObject MainMenuCanvas;
    public GameObject GameCompleteCanvas;

    public static bool reset = false;

    public static int numberOfBigs = 0;
    public static int numberOfSmalls = 0;

    public Text mathsTextArea;
    string mathsTextString = "";

    int mathsTextLine = 1;
    int currentTextNumber = 0;

    #region Number button game objects
    public GameObject button1, button2, button3, button4, button5, button6;

    public GameObject button1Disabled, button2Disabled, button3Disabled, button4Disabled, button5Disabled, button6Disabled;

    public GameObject button1Grey, button2Grey, button3Grey, button4Grey, button5Grey, button6Grey;
    #endregion
    public List<NumberButton> numberButtons;

    #region Operation Button game objects
    public GameObject addButton, subtractButton, multiplyButton, divideButton;

    public GameObject addButtonDisabled, subtractButtonDisabled, multiplyButtonDisabled, divideButtonDisabled;
    #endregion
    public List<OperationButton> operationButtons;

    public GameObject alertPanel;
    public GameObject okButton;
    public GameObject yesNoButton;
    public TMP_Text alertText;
    public string subtractAlertString;
    public string divisionAlertString;
    public string homeAlertString;

    public GameObject timeTotal;

    NumberButton tempButton1;
    NumberButton tempButton2;
    OperationButton tempOperationButton;

    bool number1selected = false;
    bool operationselected = false;
    bool number2selected = false;

    int userNumber1 = 0;
    int userNumber2 = 0;
    Operation userOperation;
    int usersTotal = 0;

    public static float startTime;
    float updatedTime;
    float finalTime;
    string textTime;

    int totalNumber;
    public TMP_Text totalNumberText;

    public void OnGameBackButtonClick()
    {
        alertText.text = homeAlertString;
        yesNoButton.SetActive(true);
        alertPanel.SetActive(true);
    }

    Difficulty difficulty;

    // Use this for initialization
    void Start()
    {
        switch (PlayerPrefs.GetString(References.DIFFICULTY_LEVEL))
        {
            case "beginner":
                difficulty = Difficulty.BEGINNER;
                break;
            case "experienced":
                difficulty = Difficulty.EXPERIENCED;
                break;
        }

        numberButtons = new List<NumberButton>() {
            new NumberButton (button1, button1.GetComponentInChildren<TMP_Text> (), button1.GetComponent<Button>(),button1Disabled, button1Disabled.GetComponentInChildren<TMP_Text> (), button1Grey),
                new NumberButton (button2, button2.GetComponentInChildren<TMP_Text> (), button2.GetComponent<Button>(), button2Disabled, button2Disabled.GetComponentInChildren<TMP_Text> (), button2Grey),
                new NumberButton (button3, button3.GetComponentInChildren<TMP_Text> (), button3.GetComponent<Button>(), button3Disabled, button3Disabled.GetComponentInChildren<TMP_Text> (), button3Grey),
                new NumberButton (button4, button4.GetComponentInChildren<TMP_Text> (), button4.GetComponent<Button>(), button4Disabled, button4Disabled.GetComponentInChildren<TMP_Text> (), button4Grey),
                new NumberButton (button5, button5.GetComponentInChildren<TMP_Text> (), button5.GetComponent<Button>(), button5Disabled, button5Disabled.GetComponentInChildren<TMP_Text> (), button5Grey),
                new NumberButton (button6, button6.GetComponentInChildren<TMP_Text> (), button6.GetComponent<Button>(), button6Disabled, button6Disabled.GetComponentInChildren<TMP_Text> (), button6Grey),
        };
        foreach (NumberButton nb in numberButtons)
        {
            nb.button.onClick.AddListener(delegate { OnNumberButtonClick(nb); });
            nb.disabledGameObject.GetComponent<Button>().onClick.AddListener(delegate { OnNumberButtonDisabledClick(nb); });
        }
        operationButtons = new List<OperationButton>() {
            new OperationButton (addButton, addButton.GetComponent<Button> (), addButtonDisabled, Operation.ADDITION),
                new OperationButton (subtractButton, subtractButton.GetComponent<Button> (), subtractButtonDisabled, Operation.SUBTRACT),
                new OperationButton (multiplyButton, multiplyButton.GetComponent<Button> (), multiplyButtonDisabled,  Operation.MULTIPLY),
                new OperationButton (divideButton, divideButton.GetComponent<Button> (), divideButtonDisabled, Operation.DIVISION)
        };
        foreach (OperationButton ob in operationButtons)
        {
            ob.button.onClick.AddListener(delegate { OnOperationButtonClick(ob); });
            //ob.disabledButton.onClick.AddListener (delegate { OnOperationButtonDisabledClick (ob); });
        }
        Reset();
        totalNumber = GenerateSuitableNumber();
        totalNumberText.text = totalNumber.ToString();
        reset = false;
    }

    void Reset()
    {
        totalArrayList.Clear();
        numberArray = CustomGame.numberArray;
        number1selected = operationselected = number2selected = false;
        ResetButtons();
        ResetTextArea();

        for (int i = 0; i < numberArray.Length; i++)
        {
            totalArrayList.Add(numberArray[i]);
        }

        DisplayNumbers();
        //	totalArrayList = ShuffleList (totalArrayList);
        totalArrayList.CopyTo(numberArray);
    }

    ArrayList ShuffleList(ArrayList numbers)
    {
        ArrayList randomList = new ArrayList();

        int randomIndex = 0;
        while (numbers.Count > 0)
        {
            randomIndex = Random.Range(0, numbers.Count);
            randomList.Add(numbers[randomIndex]);
            numbers.RemoveAt(randomIndex);
        }
        return randomList;
    }

    void ClearArray()
    {
        numberArray = new int[6];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (reset)
        {
            ClearArray();
            Start();
            reset = false;
            startTime = Time.time;
        }

        updatedTime = Time.time - startTime;

        //Debug.Log (updatedTime.ToString ());
        int minutes = (int)updatedTime / 60;
        int seconds = (int)updatedTime % 60;

        textTime = string.Format("{0:00}:{1:00}", minutes, seconds);

        timeTotal.GetComponentInChildren<TMP_Text>().text = textTime;

        if (number1selected && operationselected && number2selected)
        {

            usersTotal = applyOperation();
            Debug.Log("Users total = " + usersTotal);
            if (usersTotal == totalNumber)
            {
                ClearArray();
                DisplayNumbers();

                if (PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) < 100 && totalNumber >= 100)
                {
                    PlayerPrefs.SetInt(References.LOWEST_NUMBER_SOLVED, totalNumber);
                }
                else if (PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) >= 100 && PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED) > totalNumber)
                {
                    PlayerPrefs.SetInt(References.LOWEST_NUMBER_SOLVED, totalNumber);
                }
                if (PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) < 100 && totalNumber >= 100)
                {
                    PlayerPrefs.SetInt(References.HIGHEST_NUMBER_SOLVED, totalNumber);
                }
                else if (PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) >= 100 && PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED) < totalNumber)
                {
                    PlayerPrefs.SetInt(References.HIGHEST_NUMBER_SOLVED, totalNumber);
                }

                PlayerPrefs.SetInt(References.SMALL_NUMBERS_USED, PlayerPrefs.GetInt(References.SMALL_NUMBERS_USED) + numberOfSmalls);

                PlayerPrefs.SetInt(References.BIG_NUMBERS_USED, PlayerPrefs.GetInt(References.BIG_NUMBERS_USED) + numberOfBigs);

                finalTime = updatedTime;

                if (PlayerPrefs.GetInt(References.FASTEST_TIME) > (int)finalTime)
                {
                    PlayerPrefs.SetInt(References.FASTEST_TIME, (int)finalTime);
                }
                else if (PlayerPrefs.GetInt(References.FASTEST_TIME) < 1)
                {
                    PlayerPrefs.SetInt(References.FASTEST_TIME, (int)finalTime);

                }

                PlayerPrefs.SetInt(References.RECENT_TIME, (int)finalTime);

                if (numberOfBigs == 4)
                {
                    PlayerPrefs.SetInt(References.HOW_MANY_BIG, PlayerPrefs.GetInt(References.HOW_MANY_BIG) + 1);
                }

                PlayerPrefs.SetInt(References.TOTAL_NUMBERS_SOLVED, PlayerPrefs.GetInt(References.TOTAL_NUMBERS_SOLVED) + 1);
                Reset();
                ClearArray();
                DisplayNumbers();
                GameCompleteCanvas.SetActive(true);
                GameCanvas.SetActive(false);

            }

            if (usersTotal == -1)
            {
                alertText.text = subtractAlertString;
                okButton.SetActive(true);
                alertPanel.SetActive(true);
                ClearLastLine();
                tempButton1.disabledGameObject.SetActive(false);
                tempButton2.disabledGameObject.SetActive(false);
                tempOperationButton.disabledGameObject.SetActive(false);
                tempOperationButton.button.enabled = true;
                tempButton1.button.enabled = true;
                tempButton2.button.enabled = true;
                number1selected = operationselected = number2selected = false;
            }
            else if (usersTotal == -2)
            {
                alertText.text = divisionAlertString;
                okButton.SetActive(true);
                alertPanel.SetActive(true);
                ClearLastLine();
                tempButton1.disabledGameObject.SetActive(false);
                tempButton2.disabledGameObject.SetActive(false);
                tempOperationButton.disabledGameObject.SetActive(false);
                tempOperationButton.button.enabled = true;
                tempButton1.button.enabled = true;
                tempButton2.button.enabled = true;
                number1selected = operationselected = number2selected = false;

            }
            else
            {

                tempButton1.numberText.text = usersTotal.ToString();
                tempButton1.numberDisabledText.text = usersTotal.ToString();
                tempButton1.disabledGameObject.SetActive(false);
                tempButton1.button.enabled = true;
                tempButton2.disabledGameObject.SetActive(false);
                tempButton2.numberText.text = "";
                tempButton2.greyGameObject.SetActive(true);
                tempOperationButton.disabledGameObject.SetActive(false);
                tempOperationButton.button.enabled = true;
                addToTextArea("= " + usersTotal.ToString());
                number1selected = operationselected = number2selected = false;

            }
        }
    }
    void DisplayNumbers()
    {
        for (int i = 0; i < 6; i++)
        {
            if (numberArray[i] != 0)
            {
                Debug.Log(numberArray[i]);
                numberButtons[i].numberText.text = numberArray[i].ToString();
                numberButtons[i].numberDisabledText.text = numberArray[i].ToString();
            }
            else
            {
                numberButtons[i].numberText.text = "";
            }
        }
    }

    int addOperation(int number1, int number2)
    {
        return number1 + number2;
    }

    int multiplyOperation(int number1, int number2)
    {
        return number1 * number2;
    }

    int subtractOperation(int number1, int number2)
    {
        if (number1 - number2 < 0 && number2 - number1 > 0)
        {
            return number2 - number1;
        }
        else
        {
            return number1 - number2;
        }
    }

    int divideOperation(int number1, int number2)
    {
        if (number1 % number2 == 0 && number1 / number2 >= 1)
        {
            return number1 / number2;
        }
        else if (number2 % number1 == 0 && number2 / number1 >= 1)
        {
            return number2 / number1;
        }
        else
        {
            return number1 / number2;
        }

    }

    void ResetTextArea()
    {
        mathsTextString = "";
        mathsTextArea.text = mathsTextString;
        currentTextNumber = 0;
        mathsTextLine = 1;
    }

    void ClearLastLine()
    {
        mathsTextLine--;
        currentTextNumber = 0;
        mathsTextString = mathsTextString.Substring(0, mathsTextString.LastIndexOf(tempButton1.numberText.text.ToString() + " " + tempOperationButton.gameObject.GetComponentInChildren<TMP_Text>().text.ToString() + " " + tempButton2.numberText.text.ToString() + " "));
        Debug.Log(mathsTextString);
        mathsTextArea.text = mathsTextString;
    }

    void addToTextArea(string text)
    {
        mathsTextString += text.ToString() + " ";
        currentTextNumber++;
        if (currentTextNumber == 4)
        {
            mathsTextLine++;
            mathsTextString = mathsTextString + "\n";
            currentTextNumber = 0;
        }
        Debug.Log(mathsTextString);
        mathsTextArea.text = mathsTextString;
    }

    void takeAwayText(string removeChar)
    {
        mathsTextString = mathsTextString.Substring(0, mathsTextString.LastIndexOf(removeChar + " "));
        currentTextNumber--;
        Debug.Log(mathsTextString);
        mathsTextArea.text = mathsTextString;
    }

    public void ResetButtons()
    {
        for (int i = 0; i < numberButtons.Count; i++)
        {
            NumberButton nb = numberButtons[i];
            nb.gameObject.SetActive(true);
            nb.button.enabled = true;
            nb.disabledGameObject.SetActive(false);
            nb.greyGameObject.SetActive(false);
        }
        foreach (OperationButton ob in operationButtons)
        {
            ob.gameObject.SetActive(true);
            ob.button.enabled = true;
            ob.disabledGameObject.SetActive(false);
        }
    }
    int GenerateSuitableNumber()
    {
        bool notSuitable = true;
        while (notSuitable)
        {
            int tempNumber = GenerateTotalNumber();
            if (tempNumber < 1000)
            {
                Debug.Log("Its less than 1000");
                if (tempNumber > 99)
                {
                    Debug.Log("It was suitable");
                    return tempNumber;
                }
                Debug.Log(tempNumber + " lol");
                Debug.Log("Recreate");
            }

        }
        return 1;
    }

    int GenerateTotalNumber()
    {
        bool listContains1 = false;
        int returnValue = 0;
        ArrayList tempArray = new ArrayList();
        tempArray.Clear();
        tempArray.AddRange(totalArrayList);
        while (!listContains1)
        {
            Debug.Log("1 ArrayList count = " + tempArray.Count);
            int index1 = Random.Range(0, tempArray.Count);
            int temp1 = (int)tempArray[index1];
            tempArray.RemoveAt(index1);
            Debug.Log("2 ArrayList count = " + tempArray.Count);
            int index2 = Random.Range(0, tempArray.Count);
            int temp2 = (int)tempArray[index2];
            tempArray.RemoveAt(index2);
            Debug.Log("3 ArrayList count = " + tempArray.Count);

            int newNumber = applyRandomOperation(temp1, temp2);
            tempArray.Add(newNumber);
            Debug.Log("4 ArrayList count = " + tempArray.Count);

            if (tempArray.Count < 2)
            {
                listContains1 = true;
                returnValue = (int)tempArray[tempArray.Count - 1];
            }

        }
        Debug.Log(returnValue);
        return returnValue;
    }

    int applyRandomOperation(int number1, int number2)
    {
        if (difficulty == Difficulty.BEGINNER)
        {
            int randomOperation = Random.Range(0, 2);
            switch (randomOperation)
            {
                case 0:
                    return addOperation(number1, number2);
                case 1:
                    if (number1 - number2 > 0)
                    {
                        return subtractOperation(number1, number2);
                    }
                    else if (number2 - number1 > 0)
                    {
                        return subtractOperation(number2, number1);
                    }
                    else
                    {
                        return addOperation(number1, number2);
                    }
            }
        }
        else if (difficulty == Difficulty.EXPERIENCED)
        {
            int randomOperation = Random.Range(0, 3);
            switch (randomOperation)
            {
                case 0:
                    if (number1 - number2 > 0)
                    {
                        return subtractOperation(number1, number2);
                    }
                    else if (number2 - number1 > 0)
                    {
                        return subtractOperation(number2, number1);
                    }
                    else
                    {
                        return multiplyOperation(number1, number2);
                    }
                case 1:
                    return multiplyOperation(number1, number2);
                case 2:
                    if (number1 / number2 > 1 && number2 / number1 > 1)
                    {
                        return divideOperation(number1, number2);
                    }
                    else if (number1 - number2 > 0 || number2 - number1 > 0)
                    {
                        int randomOperation2 = Random.Range(1, 3);
                        switch (randomOperation2)
                        {
                            case 1:
                                return multiplyOperation(number1, number2);
                            case 2:
                                return subtractOperation(number1, number2);
                        }
                    }
                    return 1;
            }
        }
        return 1;
    }
    int applyOperation()
    {
        switch (userOperation)
        {
            case Operation.ADDITION:
                return userNumber1 + userNumber2;
            case Operation.SUBTRACT:
                if (userNumber1 - userNumber2 > 0)
                {
                    return userNumber1 - userNumber2;
                }
                else
                {
                    return -1;
                }
            case Operation.MULTIPLY:
                return userNumber1 * userNumber2;
            case Operation.DIVISION:
                if (userNumber1 % userNumber2 == 0)
                {
                    return userNumber1 / userNumber2;
                }
                else
                {
                    return -2;
                }
            default:
                return 1;
        }
    }
    public void OnNumberButtonClick(NumberButton nb)
    {
        if (!number1selected && !operationselected && !number2selected)
        {
            nb.button.enabled = false;
            addToTextArea(nb.numberText.text);
            nb.disabledGameObject.SetActive(true);
            number1selected = true;
            tempButton1 = nb;
            userNumber1 = int.Parse(nb.numberText.text);
        }
        else if (number1selected && operationselected && !number2selected)
        {
            nb.button.enabled = false;
            addToTextArea(nb.numberText.text);
            nb.disabledGameObject.SetActive(true);
            number2selected = true;
            tempButton2 = nb;
            userNumber2 = int.Parse(nb.numberText.text);
        }
    }

    public void OnNumberButtonDisabledClick(NumberButton nb)
    {
        if (number1selected && !operationselected && !number2selected)
        {
            nb.button.enabled = true;
            takeAwayText(nb.numberText.text);
            nb.disabledGameObject.SetActive(false);
            number1selected = false;
        }
    }

    public void OnOperationButtonClick(OperationButton ob)
    {
        if (number1selected && !operationselected && !number2selected)
        {
            addToTextArea(ob.button.GetComponentInChildren<TMP_Text>().text);
            ob.disabledGameObject.SetActive(true);
            operationselected = true;
            tempOperationButton = ob;
            userOperation = ob.operation;
        }
        else if (operationselected)
        {
            tempOperationButton.button.enabled = true;
            takeAwayText(tempOperationButton.gameObject.GetComponentInChildren<TMP_Text>().text);
            tempOperationButton.disabledGameObject.SetActive(false);
			
            if (tempOperationButton.Equals(ob))
            {
                operationselected = false;
            }
            else
            {
                addToTextArea(ob.button.GetComponentInChildren<TMP_Text>().text);
                ob.disabledGameObject.SetActive(true);
                operationselected = true;
                tempOperationButton = ob;
                userOperation = ob.operation;
            }

        }
    }

    public void OnAlertOkButtonClick()
    {
        alertPanel.SetActive(false);
        okButton.SetActive(false);
    }
    public void OnAlertYesButtonClick()
    {
        ClearArray();
        DisplayNumbers();
        Reset();
        alertPanel.SetActive(false);
        yesNoButton.SetActive(false);
        MainMenuCanvas.SetActive(true);
        GameCanvas.SetActive(false);
    }
    public void OnAlertNoButtonClick()
    {
        alertPanel.SetActive(false);
        yesNoButton.SetActive(false);
    }

    public void OnResetButtonClick()
    {
        //Start ();
        PlayerPrefs.SetInt(References.NUMBER_OF_RESETS, PlayerPrefs.GetInt(References.NUMBER_OF_RESETS) + 1);
        Reset();
    }
}

public enum Operation
{
    ADDITION,
    SUBTRACT,
    MULTIPLY,
    DIVISION
}
public class OperationButton
{
    public GameObject gameObject;
    public Button button;
    public GameObject disabledGameObject; public Operation operation;

    public OperationButton(GameObject gameObject, Button button, GameObject disabledGameObject, Operation operation)
    {
        this.gameObject = gameObject;
        this.button = button;
        this.disabledGameObject = disabledGameObject;
        this.operation = operation;
    }
}
public class NumberButton
{
    public GameObject gameObject;
    public Button button;
    public TMP_Text numberText;
    public GameObject disabledGameObject;
    public TMP_Text numberDisabledText;
    public GameObject greyGameObject;

    public NumberButton(GameObject gameObject, TMP_Text numberText, Button button, GameObject disabledGameObject, TMP_Text numberDisabledText, GameObject greyGameObject)
    {
        this.gameObject = gameObject;
        this.numberText = numberText;
        this.button = button;
        this.disabledGameObject = disabledGameObject;
        this.numberDisabledText = numberDisabledText;
        this.greyGameObject = greyGameObject;

    }

}
public enum Difficulty
{
    BEGINNER,
    EXPERIENCED
}