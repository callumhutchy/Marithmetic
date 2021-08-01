using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Statistics : MonoBehaviour {


	public GameObject HighestNumberSolved, LowestNumberSolved, TotalNumbersSolved, NumberOfResets, BigNumbersUsed, SmallNumbersUsed, HowManyBigAllTime, FastestTime, RecentTime;

	string textTime;
	string recentTime;

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
		int minutes = (int) PlayerPrefs.GetInt (References.FASTEST_TIME) / 60;
		int seconds = (int) PlayerPrefs.GetInt (References.FASTEST_TIME) % 60;
		textTime = string.Format ("{0:00}:{1:00}", minutes, seconds);
		FastestTime.GetComponent<Text> ().text = textTime;
		int minutes1 = (int) PlayerPrefs.GetInt (References.RECENT_TIME) / 60;
		int seconds1 = (int) PlayerPrefs.GetInt (References.RECENT_TIME) % 60;
		recentTime = string.Format ("{0:00}:{1:00}", minutes1, seconds1);
		RecentTime.GetComponent<Text> ().text = recentTime;




	}
}
