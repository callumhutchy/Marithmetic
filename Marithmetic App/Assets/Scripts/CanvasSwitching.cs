using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Net;
using System;
using UnityEngine.SocialPlatforms;
public class CanvasSwitching : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject CustomGameSetup;
	public GameObject AboutCanvas;
	public GameObject StatsCanvas;

	public Text newsMessage;

	static string urlConfig = "http://213.171.209.132//marithmetic.config";
	public string fetchedString;

	public void OnMainMenuCustomGameSetupClick(){

			CustomGameSetup.SetActive (true);
			MainMenu.SetActive (false);
			CustomGame.reset = true;

	}


	public void OnAboutButtonClick(){
		AboutCanvas.SetActive (true);
		MainMenu.SetActive (false);

	}
	public void OnStatsButtonClick(){
		StatsCanvas.SetActive (true);
		MainMenu.SetActive (false);
	}
	public void OnReportBugButtonClick(){
		SendEmail ();
	}

	void SendEmail ()
	{
		string email = "callumhutchie@hotmail.co.uk";
		string subject = MyEscapeURL("Marithmetic Bug Report");
		string body = MyEscapeURL("The problem:\r\n\n\nHas this happened before?");
		
		Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
	}
	
	string MyEscapeURL (string url)
	{
		return WWW.EscapeURL(url).Replace("+","%20");
	}


	void Start(){

		StartCoroutine ("wwwLoad");







		//Debug.Log (fetchedString);
	}
	
	IEnumerator wwwLoad(){
		WWW myWWW = new WWW(urlConfig);
		yield return myWWW;
		fetchedString = myWWW.text;
		//string filepath = "C:/Users/Callum/Desktop/";
		WebClient client = new WebClient();

		HttpWebRequest req =(HttpWebRequest)WebRequest.Create(urlConfig);
		HttpWebResponse res =(HttpWebResponse) req.GetResponse();

		Debug.Log (fetchedString);
		newsMessage.text = fetchedString;


		PlayerPrefs.SetString (References.FETCHED_STRING, fetchedString);


		foreach (string line in fetchedString.Split('\n'))
		{
			if(line.Contains("newsFontSize=")){
				PlayerPrefs.SetInt (References.FONT_SIZE, int.Parse(line.Replace("newsFontSize="," ")));
			}else if(line.Contains("newsText=")){
				PlayerPrefs.SetString (References.NEWS_TEXT,line.Replace("newsText=",""));
			}else if(line.Contains ("websiteLink=")){
				PlayerPrefs.SetString (References.WEBSITE_LINK,line.Replace("websiteLink=",""));
			}else if(line.Contains ("myAppsLink=")){
				PlayerPrefs.SetString (References.MYAPPS_LINK, line.Replace("myAppsLink=",""));
			}else if(line.Contains ("appStorePage")){
				PlayerPrefs.SetString (References.APPSTORE_LINK, line.Replace("appStorePage=",""));
			}
		}

		newsMessage.text = PlayerPrefs.GetString (References.NEWS_TEXT);
		newsMessage.fontSize = PlayerPrefs.GetInt (References.FONT_SIZE);

	}


}


