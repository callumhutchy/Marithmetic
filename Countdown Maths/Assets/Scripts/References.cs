using UnityEngine;
using System.Collections;

public class References : MonoBehaviour {


	public static string HIGHEST_NUMBER_SOLVED, LOWEST_NUMBER_SOLVED, TOTAL_NUMBERS_SOLVED, NUMBER_OF_RESETS, BIG_NUMBERS_USED, SMALL_NUMBERS_USED, HOW_MANY_BIG, FASTEST_TIME, RECENT_TIME; 

	// Use this for initialization
	void Start () {
		HIGHEST_NUMBER_SOLVED = "highest_number_solved";
		LOWEST_NUMBER_SOLVED = "lowest_number_solved";
		TOTAL_NUMBERS_SOLVED = "total_numbers_solved";
		NUMBER_OF_RESETS = "number_of_resets";
		BIG_NUMBERS_USED = "big_numbers_used";
		SMALL_NUMBERS_USED = "small_numbers_used";
		HOW_MANY_BIG = "how_many_times_max_big_numbers";
		FASTEST_TIME = "fastest_time";
		RECENT_TIME = "recent_time";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
