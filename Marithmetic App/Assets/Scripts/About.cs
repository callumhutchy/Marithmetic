using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class About : MonoBehaviour {

	public GameObject AboutCanvas;
	public GameObject MainMenuCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnWebsiteLinkClick(){
		Application.OpenURL(PlayerPrefs.GetString(References.WEBSITE_LINK));

	}
	public void OnTwitterClick(){
		Application.OpenURL(PlayerPrefs.GetString(References.TWITTER_LINK));
		
	}
	public void OnMyAppsClick(){
		Application.OpenURL(PlayerPrefs.GetString(References.MYAPPS_LINK));
	}

	public void OnRateButtonClick(){
		Application.OpenURL (PlayerPrefs.GetString(References.APPSTORE_LINK));
	}

	public void OnBackButtonClick(){
		MainMenuCanvas.SetActive (true);
		AboutCanvas.SetActive (false);
	}

}
