using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompleteGame : MonoBehaviour {

	public GameObject gameCompleteCanvas;
	public GameObject homeCanvas;
	public GameObject customGameCanvas;
	public GameObject FastestTime, RecentTime;

	string textTime;
	string recentTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		int minutes = (int) PlayerPrefs.GetInt (References.FASTEST_TIME) / 60;
		int seconds = (int) PlayerPrefs.GetInt (References.FASTEST_TIME) % 60;
		textTime = string.Format ("{0:00}:{1:00}", minutes, seconds);
		FastestTime.GetComponent<Text> ().text = textTime;
		int minutes1 = (int) PlayerPrefs.GetInt (References.RECENT_TIME) / 60;
		int seconds1 = (int) PlayerPrefs.GetInt (References.RECENT_TIME) % 60;
		recentTime = string.Format ("{0:00}:{1:00}", minutes1, seconds1);
		RecentTime.GetComponent<Text> ().text = recentTime;


	}

	public void OnPlayAgainButtonClick(){
		customGameCanvas.SetActive (true);
		gameCompleteCanvas.SetActive (false);
		CustomGame.reset = true;
	}
	public void OnHomeButtonClick(){
		homeCanvas.SetActive (true);
		gameCompleteCanvas.SetActive (false);

	}

}
