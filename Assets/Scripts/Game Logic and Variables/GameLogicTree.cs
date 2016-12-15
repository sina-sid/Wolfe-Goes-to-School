using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;



// Streamer struct for later functionality 

public struct Streamer {
	public bool isAlive;
	public string whoIsStreamer;
	public string streamerResponseType;
}

public struct AudienceSelection {
	public int questionNumber;
	public string whoToKill;
}







//Round contains 3 questions to choose between 

public class Round 
{	
	public Question Q1; 
	public Question Q2; 
	public Question Q3; 
}
	



//Each Question contains the title of the question as well as a list of characters 

public class Question
{
	public string title; 
	public List<Character> Characters; 

	public Character getCharacter(string name) {
		for (int i = 0; i < Characters.Count; i++) {
			if (Characters [i].name.ToLower().Equals (name.ToLower())) {
				return Characters[i]; 
			}
		}
		return default(Character); 
	}
}




//Character contains various booleans required for the project
public class Character
{
	//check if this character is alive, if they are the wolf, or if the response has been played 
	public bool isAlive;
	public bool isWolf; 
	public bool isResponsePlayed; 
	public string name; 
	public bool hasSpoken;

	//various responses that can be played by the user 
	public string neutralResponse; 
	public string selfResponse; 
	public string primaryResponse; 
	public string secondaryResponse; 
	public string tertiaryResponse; 

	//pointer back to the parent to get the question
	public Question parent; 


	//get a random response from all of the possible choices of responses for the character
	public string getRandomResponse() {
		int choices = 4; 

		//null checks 
		choices = string.IsNullOrEmpty (secondaryResponse) == true ? choices -= 1 : choices; 
		choices = string.IsNullOrEmpty (tertiaryResponse) == true ? choices -= 1 : choices; 

		//get the character that could be blamed from the responses 
		string character1 = getBlameCharacter (primaryResponse); 
		string character2 = getBlameCharacter (primaryResponse); 
		string character3 = getBlameCharacter (primaryResponse); 



		//randomly pick a response to blame

		int randomNum = Random.Range(0,choices);

		switch (randomNum)
		{
		case 0:
			return neutralResponse; 
		case 1:
			return selfResponse; 
		case 2:
			if (parent.getCharacter (character1).isAlive) {
				return primaryResponse; 
			}

			if (parent.getCharacter (character2).isAlive) {
				return secondaryResponse; 
			}

			if (parent.getCharacter (character3).isAlive) {
				return tertiaryResponse; 
			}
			return ""; 
		case 3:
			if (parent.getCharacter (character2).isAlive) {
				return secondaryResponse; 
			}

			if (parent.getCharacter (character3).isAlive) {
				return tertiaryResponse; 
			}
			return ""; 
		case 4:
			if (parent.getCharacter (character3).isAlive) {
				return tertiaryResponse; 
			}
			return "";
		default: 
			return ""; 
		}
	}


	//parses string to see character to blame
	public string getBlameCharacter(string str) {
		if (str != null) {
			if (str.ToLower ().Contains ("greg")) {
				return "greg"; 
			}

			if (str.ToLower ().Contains ("bruno")) {
				return "bruno"; 
			}


			if (str.ToLower ().Contains ("alix")) {
				return "alix"; 
			}


			if (str.ToLower ().Contains ("olivia")) {
				return "olivia"; 
			}


			if (str.ToLower ().Contains ("duke")) {
				return "duke"; 
			}
		}


		return ""; 

	}
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


	//boolean to check if the wolf has been killed 
	public bool isWolfKilled = false; 


	//global list of the entire script
	public List<Round> rounds; 

	// Use this for initialization
	//THIS FUNCTION IS CALLED ONCE PER GAME TO SET STARTING VALUES AND CREATE ALL INSTANCES OF POSSIBLE DIALOGUE
	public void Start () {

		parseRounds (); 


	}




	// called by anyone
	public void Init()
	{
		// some stuff...
		parseRounds (); 
	}

	// Call function before starting every new round
	void resetValuesForNextRound() {
		characters.resetCharactersForNextRound ();
		roundNumber++;
		questionNumber = 0; //way to tell if question has been selected: if questionNumber != 0
	}



	//save persistance 
	void awake() {
		DontDestroyOnLoad (transform.gameObject); 
	}





	//keep track of round number 
	public int getRoundNumber() {
		return roundNumber; 
	}

	public void setRoundNumber(int n) {
		roundNumber = n; 
	}



	//get the number of players who still remain alive
	public int getNumAlive() {


		int count = 0; 
		foreach (Character C in rounds[0].Q1.Characters) {
			if (C.isAlive) {
				count++;  
			}
		}

		return count; 



	}



	//parses JSON of the dialogue and turns it into the class structure as described above 
	void parseRounds() {


		rounds = new List<Round> (); 

		roundNumber = 0; 

		TextAsset r1 = Resources.Load("round1") as TextAsset; 
		var N = JSON.Parse(r1.text);
		Round round1 = new Round(); 






		//create a question and get all possible characters and response...
		//repeats for all of the options
		Question q1 = new Question(); 
		q1.title = N ["Question1"] ["title"].Value; 
		JSONArray arr = N["Question1"]["characters"].AsArray;
		q1.Characters = new List<Character> (); 

		int randNum = Random.Range (0, arr.Count); 
		// int randNum = 1; 

		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.name = arr[i]["name"].Value; 
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = ""; 
			character.parent = q1; 
			q1.Characters.Add (character); 
		}
		round1.Q1 = q1; 


		Question q2 = new Question(); 
		q2.title = N ["Question2"] ["title"].Value; 
		arr = N["Question2"]["characters"].AsArray;
		q2.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = ""; 
			character.parent = q2; 
			q2.Characters.Add (character); 
		}
		round1.Q2 = q2; 


		Question q3 = new Question(); 
		q3.title = N ["Question3"] ["title"].Value; 
		arr = N["Question3"]["characters"].AsArray;
		q3.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = ""; 
			character.parent = q3; 
			q3.Characters.Add (character); 
		}
		round1.Q3 = q3; 


		rounds.Add (round1); 


		Round round2 = new Round (); 
		TextAsset r2Txt = Resources.Load("round2") as TextAsset; 
		var parsedR2 = JSON.Parse(r2Txt.text);

		q1 = new Question(); 
		q1.title = parsedR2 ["Question1"] ["title"].Value; 
		arr = parsedR2["Question1"]["characters"].AsArray;
		q1.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.parent = q1; 
			q1.Characters.Add (character); 
		}
		round2.Q1 = q1; 


		q2 = new Question(); 
		q2.title = parsedR2["Question2"] ["title"].Value; 
		arr = parsedR2["Question2"]["characters"].AsArray;
		q2.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.parent = q2; 
			q2.Characters.Add (character); 
		}
		round2.Q2 = q2; 


		q3 = new Question(); 
		q3.title = N ["Question3"] ["title"].Value; 
		arr = N["Question3"]["characters"].AsArray;
		q3.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character (); 
			character.isResponsePlayed = false;
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.isAlive = true; 
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.parent = q3; 
			q3.Characters.Add (character); 
		}
		round2.Q3 = q3; 
		rounds.Add (round2); 



		Round round3 = new Round (); 
		TextAsset r3Txt = Resources.Load("round3") as TextAsset; 
		var parsedR3 = JSON.Parse(r3Txt.text);


		q1 = new Question(); 
		q1.title = parsedR3 ["Question1"] ["title"].Value; 
		arr = parsedR3["Question1"]["characters"].AsArray;
		q1.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.tertiaryResponse = arr[i]["tertiary"].Value; 
			character.parent = q1; 

			q1.Characters.Add (character); 
		}
		round3.Q1 = q1; 


		q2 = new Question(); 
		q2.title = parsedR3["Question2"] ["title"].Value; 
		arr = parsedR3["Question2"]["characters"].AsArray;
		q2.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			character.isAlive = true; 
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.tertiaryResponse = arr[i]["tertiary"].Value; 
			character.parent = q2; 
			q2.Characters.Add (character); 
		}
		round3.Q2 = q2; 


		q3 = new Question(); 
		q3.title = parsedR3["Question3"] ["title"].Value; 
		arr = parsedR3["Question3"]["characters"].AsArray;
		q3.Characters = new List<Character> (); 
		for (int i = 0; i < arr.Count; i++) {
			Character character = new Character ();  
			character.isResponsePlayed = false;
			if (i == randNum) {
				character.isWolf = true; 
			} else {
				character.isWolf = false;
			}
			character.isAlive = true; 
			character.name = arr[i]["name"].Value; 
			character.neutralResponse = arr[i]["neutral"].Value; 
			character.selfResponse = arr[i]["self"].Value; 
			character.primaryResponse = arr[i]["primary"].Value; 
			character.secondaryResponse = arr[i]["secondary"].Value; 
			character.tertiaryResponse = arr[i]["tertiary"].Value; 
			character.parent = q3; 
			q3.Characters.Add (character); 
		}
		round3.Q3 = q3; 
		rounds.Add (round3); 

	}


	//kills a character at any stage of the game 
	public void kill(string name) {
		foreach (Round r in rounds) {
			for (int i = 0; i < r.Q1.Characters.Count; i++) {
				Character c = r.Q1.Characters [i]; 
				if (c.name.Equals (name)) {
					if (c.isWolf) {
						this.isWolfKilled = true; 
					}
					c.isAlive = false; 
				}

			}
			for (int i = 0; i < r.Q2.Characters.Count; i++) {
				Character c = r.Q2.Characters [i]; 
				if (c.name.Equals (name)) {
					if (c.isWolf) {
						this.isWolfKilled = true; 
					}
					c.isAlive = false; 
				}
			}
			for (int i = 0; i < r.Q3.Characters.Count; i++) {
				Character c = r.Q3.Characters [i]; 
				if (c.name.Equals (name)) {
					if (c.isWolf) {
						this.isWolfKilled = true; 
					}
					c.isAlive = false; 
				}
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}

