using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public GameObject MainMenuCanvas;
	public GameObject StatsCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick(){
		MainMenuCanvas.SetActive (true);
		StatsCanvas.SetActive (false);
	}

}
