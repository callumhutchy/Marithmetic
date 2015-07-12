using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SocialPlatforms;

public class CanvasSwitching : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject CustomGameSetup;
	public GameObject AboutCanvas;
	public GameObject StatsCanvas;


	

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


	void Start(){

	}

	void Update(){

	}



}
