using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CompleteGame : MonoBehaviour {

	public GameObject gameCompleteCanvas;
	public GameObject homeCanvas;
	public GameObject customGameCanvas;
	public GameObject FastestTime, RecentTime;
	public GameObject rateAlert;

	bool StartAlert = false;

	string textTime;
	string recentTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int rand;
		rand = Random.Range (1, 8);
		if (gameCompleteCanvas.activeSelf && !StartAlert) {
			Debug.Log (rand + " " + PlayerPrefs.GetString(References.REVIEW_APP));
			StartAlert = true;
			if (rand == 1 && (PlayerPrefs.GetString (References.REVIEW_APP) != "never")) {
				rateAlert.SetActive (true);

			}

		}

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
		StartAlert = false;

	}

	public void OnRateButtonNeverClick(){
		PlayerPrefs.SetString (References.REVIEW_APP, "never");
		rateAlert.SetActive (false);
	}
	public void OnRateButtonRateClick(){
		Application.OpenURL ("https://play.google.com/store/apps/details?id=uk.co.callumhutchy.marithmetic");
		PlayerPrefs.SetString (References.REVIEW_APP, "never");
		rateAlert.SetActive (false);
	}
	public void OnRateButtonNotNowClick(){
		rateAlert.SetActive (false);
	}




}
