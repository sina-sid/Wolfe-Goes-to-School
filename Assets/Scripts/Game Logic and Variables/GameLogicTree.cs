using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public Characters characters;

	//Round variables
	public int roundNumber;

	//Response Types (pre-defined)
	public List<string> responseTypes = new List<string>();

	// Question variables
	public int questionNumber;
	public Dictionary<int, string> question1;
	public Dictionary<int, string> question2;
	public Dictionary<int, string> question3;

	// Use this for initialization
	//THIS FUNCTION IS CALLED ONCE PER GAME TO SET STARTING VALUES AND CREATE ALL INSTANCES OF POSSIBLE DIALOGUE
	void Start () {
		characters = GetComponent<Characters>();
		characters.Start (); //does not run. Why?
		Debug.Log (characters.Greg.response); //why doesn't this print?
		//Debug.Log (characters.Greg.isAlive.ToString ());
		characters.resetCharactersForNextRound();
		resetValuesForNextRound ();

		//Streamer.isAlive = true;

		// Initialize responseTypes
		responseTypes.Add("Blame Someone else");
		responseTypes.Add("Give an alibi");
		responseTypes.Add("Take the blame");

		// Initialize Questions
		question1 = new Dictionary<int, string>();
		question1.Add(1, "Where were you last night?");
		question1.Add(2, " Who do you trust?");
		question1.Add(3, "How’s your environment at home?");

		question2 = new Dictionary<int, string>();
		question2.Add(1, "Where were you?");
		question2.Add(2, " Who do?");
		question2.Add(3, "How’s your?");

		question3 = new Dictionary<int, string>();
		question3.Add(1, "Where were you last?");
		question3.Add(2, " Who do you?");
		question3.Add(3, "How’s your environment?");
	}

	// Call function before starting every new round
	void resetValuesForNextRound() {
		characters.resetCharactersForNextRound ();
		roundNumber++;
		questionNumber = 0; //way to tell if question has been selected: if questionNumber != 0
	}


	//void checkIfStreamerAlive() {
	//
	//}

	
	// Update is called once per frame
	void Update () {
	
	}
}

