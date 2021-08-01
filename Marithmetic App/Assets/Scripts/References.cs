using UnityEngine;
using System.Collections;

public class References : MonoBehaviour {


	public static string HIGHEST_NUMBER_SOLVED, LOWEST_NUMBER_SOLVED, TOTAL_NUMBERS_SOLVED, NUMBER_OF_RESETS, BIG_NUMBERS_USED, SMALL_NUMBERS_USED, HOW_MANY_BIG, FASTEST_TIME, RECENT_TIME; 

	public static string NEWS_LAST_MODIFIED, CONFIG_LAST_MODIFIED;

	public static string REVIEW_APP;

	public static string FETCHED_STRING;
	
	public static string FONT_SIZE, NEWS_TEXT, WEBSITE_LINK, TWITTER_LINK, MYAPPS_LINK, APPSTORE_LINK;

	public static string DIFFICULTY_LEVEL;


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


		NEWS_LAST_MODIFIED = "news_last_modified";
		CONFIG_LAST_MODIFIED = "config_last_modified";

		REVIEW_APP = "review_app";

		FETCHED_STRING = "fetched_string";

		FONT_SIZE = "font_size";
		NEWS_TEXT = "news_text";
		WEBSITE_LINK = "website_link";
		TWITTER_LINK = "twitter_link";
		MYAPPS_LINK = "myapps_link";
		APPSTORE_LINK = "appstore_link";

		DIFFICULTY_LEVEL = "difficulty";

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
