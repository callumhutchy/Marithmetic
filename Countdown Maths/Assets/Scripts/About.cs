using UnityEngine;
using System.Collections;

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
		Application.OpenURL("www.callumhutchy.co.uk/mental-maths/");

	}
	public void OnTwitterClick(){
		Application.OpenURL("https://twitter.com/thecallumhutchy");
		
	}
	public void OnMyAppsClick(){
		Application.OpenURL("https://play.google.com/store/search?q=callumhutchy&c=apps");
		
	}
	public void OnBackButtonClick(){
		MainMenuCanvas.SetActive (true);
		AboutCanvas.SetActive (false);
	}

}
