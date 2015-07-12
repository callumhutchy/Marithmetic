using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Statistics : MonoBehaviour {


	public GameObject HighestNumberSolved, LowestNumberSolved, TotalNumbersSolved, NumberOfResets, BigNumbersUsed, SmallNumbersUsed, HowManyBigAllTime;



	// Use this for initialization
	void Start () {
	
	}

	void Awake(){

	}
	
	// Update is called once per frame
	void Update () {
		HighestNumberSolved.GetComponent<Text>().text = PlayerPrefs.GetInt(References.HIGHEST_NUMBER_SOLVED).ToString();
		LowestNumberSolved.GetComponent<Text>().text = PlayerPrefs.GetInt(References.LOWEST_NUMBER_SOLVED).ToString();
		TotalNumbersSolved.GetComponent<Text>().text = PlayerPrefs.GetInt(References.TOTAL_NUMBERS_SOLVED).ToString();
		NumberOfResets.GetComponent<Text>().text = PlayerPrefs.GetInt(References.NUMBER_OF_RESETS).ToString();
		BigNumbersUsed.GetComponent<Text>().text = PlayerPrefs.GetInt(References.BIG_NUMBERS_USED).ToString();
		SmallNumbersUsed.GetComponent<Text>().text = PlayerPrefs.GetInt(References.SMALL_NUMBERS_USED).ToString();
		HowManyBigAllTime.GetComponent<Text>().text = PlayerPrefs.GetInt(References.HOW_MANY_BIG).ToString();




	}
}
