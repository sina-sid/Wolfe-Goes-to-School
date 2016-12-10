using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;


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






public struct Round 
{


	public struct Character
	{
		public bool isAlive;
		public string name; 
		public bool hasSpoken;
		public string neutralResponse; 
		public string selfResponse; 
		public string primaryResponse; 

	}

	public struct Question
	{
		public string title; 
		public List<Character> Characters; 
	}
		
	public Question Q1; 
	public Question Q2; 
	public Question Q3; 
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

		parseRounds (); 



		characters = GetComponent<Characters>();
		characters.Start (); //does not run. Why?
		Debug.Log (characters.Greg.response); //why doesn't this print?
		//Debug.Log (characters.Greg.isAlive.ToString ());
		characters.resetCharactersForNextRound();
		resetValuesForNextRound ();



		//Streamer.isAlive = true;

		// Initialize responseTypes






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



	void parseRounds() {
		TextAsset r1 = Resources.Load("round1") as TextAsset; 
		var N = JSON.Parse(r1.text);
		Round round1 = new Round(); 

		Round.Question q1 = new Round.Question(); 
		q1.title = N ["Question1"] ["title"].Value; 
		JSONArray arr = N["Question1"]["characters"].AsArray;
		q1.Characters = new List<Round.Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Round.Character character = new Round.Character ();  
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			q1.Characters.Add (character); 
		}
		round1.Q1 = q1; 


		Round.Question q2 = new Round.Question(); 
		q2.title = N ["Question2"] ["title"].Value; 
		arr = N["Question2"]["characters"].AsArray;
		q2.Characters = new List<Round.Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Round.Character character = new Round.Character ();  
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			q2.Characters.Add (character); 
		}
		round1.Q2 = q2; 


		Round.Question q3 = new Round.Question(); 
		q3.title = N ["Question3"] ["title"].Value; 
		arr = N["Question3"]["characters"].AsArray;
		q3.Characters = new List<Round.Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Round.Character character = new Round.Character ();  
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			q3.Characters.Add (character); 
		}
		round1.Q3 = q3; 

	}

	
	// Update is called once per frame
	void Update () {
	
	}
}

