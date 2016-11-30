using UnityEngine;
using System.Collections;


//TODO: ADD STREAMER FUNCTIONALITY

public struct Streamer {
	public bool isAlive;
	public string whoIsStreamer;
	public string streamerResponseType;
}

public struct AudienceSelection {
	public int questionNumber;
	public string whoToKill;
}

public class GameLogicTree : MonoBehaviour {

	public int roundNumber;
	public int questionNumber;

	public bool isGregAlive;
	public bool isBrunoAlive;
	public bool isAlixAlive;
	public bool isOliviaAlive;
	public bool isDukeAlive;

	///After initialization, these values will be changed in other files

	public bool hasGregSpoken;
	public bool hasBrunoSpoken;
	public bool hasAlixSpoken;
	public bool hasOliviaSpoken;
	public bool hasDukeSpoken;

	public string gregResponseType;
	public string brunoResponseType;
	public string alixResponseType;
	public string oliviaResponseType;
	public string dukeResponseType;

	public string gregResponse;
	public string brunoResponse;
	public string alixResponse;
	public string oliviaResponse;
	public string dukeResponse;


	// Use this for initialization
	//THIS FUNCTION IS CALLED ONCE PER GAME TO SET STARTING VALUES AND CREATE ALL INSTANCES OF POSSIBLE DIALOGUE
	void Start () {
		roundNumber = 1;
		resetValuesForNextRound ();

		isGregAlive = true;
		isBrunoAlive = true;
		isAlixAlive = true;
		isOliviaAlive = true;
		isDukeAlive = true;

		hasGregSpoken = false;
		hasBrunoSpoken  = false;
		hasAlixSpoken = false;
		hasOliviaSpoken = false;
		hasDukeSpoken = false;

		Streamer.isAlive = true;

	}

	// Call function before starting every new round
	void resetValuesForNextRound() {
		roundNumber++;
		questionNumber = 0; //should be reset to 0 every round, before question is selected
		//way to tell if question has been selected: if questionNumber != 0

		gregResponseType = "";
		brunoResponseType = "";
		alixResponseType = "";
		oliviaResponseType = "";
		dukeResponseType = "";

		gregResponse = "";
		brunoResponse = "";
		alixResponse = "";
		oliviaResponse = "";
		dukeResponse = "";
	}

	void killGreg() {
		isGregAlive = false;
		//checkIfSreamerAlive ();
	}

	//void checkIfStreamerAlive() {
	//
	//}

	
	// Update is called once per frame
	void Update () {
	
	}
}

