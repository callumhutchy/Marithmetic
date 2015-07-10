using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour {

	public GameObject MainMenuCanvas;
	public GameObject StoreCanvas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick(){
		MainMenuCanvas.SetActive (true);
		StoreCanvas.SetActive (false);
	}

}
