using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwitchScript : MonoBehaviour {

	TwitchIRC IRC; 
	bool connected; 
	public string delimiter;

	//hash tables to keep track of choices made by the user 
	public Hashtable votes; 
	public Hashtable questionChoice; 



	//bars in the graph screens 
	public GameObject row1;
	public GameObject row2;
	public GameObject row3;
	public GameObject row4;
	public GameObject row5;



	public GameObject q1; 
	public GameObject q2; 
	public GameObject q3; 









	// Use this for initialization
	void Start () {

		// iniialize all graph views 
		row1 = GameObject.Find("greg_block");
		row2 = GameObject.Find("bruno_block");
		row3 = GameObject.Find("alix_block");
		row4 = GameObject.Find("olivia_block");
		row5 = GameObject.Find("duke_block");


		q1 = GameObject.Find ("q0_block"); 
		q2 = GameObject.Find ("q1_block"); 
		q3 = GameObject.Find ("q2_block"); 
	

		IRC = this.GetComponent<TwitchIRC>();
		votes = new Hashtable();
		questionChoice = new Hashtable (); 


		//start listening to twitch stuff 
		if (IRC == null) {
			Debug.Log ("THIS IS NULL"); 
		} else {
			IRC.Connected += Connected;
			delimiter = ":"; 
			IRC.messageRecievedEvent.AddListener(getMessage);

		}
	}


	//call back listener once you're connected to twitch chat 
	void Connected()
	{
		connected = true;
		Debug.Log("Connected to Chat");
	}

	// Update is called once per frame
	void Update () {
	
	}


	//gets called when someone sends a message on twitch 
	void getMessage(string msg){
		//Handle Message
		//Form: “PRIVMSG #nickName: message”
		convertMsg(msg); 

	}


	//can be called to see who won the question 
	public int questionWinner() {
		

		//hashtable to keep track of the choices 
		Hashtable choiceHash = new Hashtable ();  
		int max = -1; 
		foreach (string key in questionChoice.Keys) {
			int val = (int)questionChoice[key];

			if (choiceHash.ContainsKey (val)) {
				choiceHash [val] = ((int)choiceHash [val]) + 1; 
			} else {
				choiceHash.Add (val, 1); 
			}
		}

		//iterate through hashtable to get winner 
		foreach (int key in choiceHash.Keys) {
			int val = (int)choiceHash[key];
			if (val > max) {
				max = key; 
			}
		}
		questionChoice.Clear (); 
		return max; 
	}


	//can be called to see who won the character vote 
	public string votesWinner() {



		Hashtable choiceHash = new Hashtable ();  
		int max = -1; 
		foreach (string key in votes.Keys) {
			int val = (int)votes[key];

			if (choiceHash.ContainsKey (val)) {
				choiceHash [val] = ((int)choiceHash [val]) + 1; 
			} else {
				choiceHash.Add (val, 1); 
			}
		}


		foreach (int key in choiceHash.Keys) {
			int val = (int)choiceHash[key];
			if (val > max) {
				max = key; 
			}
		}
		votes.Clear (); 

	


		//return name of chracter rather than index

		switch (max) {
		case 0: 
			return "Greg"; 
		case 1: 
			return "Bruno";
		case 2: 
			return "Alix"; 
		case 3: 
			return "Olivia"; 
		case 4: 
			return "Duke"; 
		}

		return ""; 
	}


	//send chat message back to the user 
	public void displayMessage(string str) {
		IRC.SendMsg (str); 
	}


	//vote for a character
	public void Vote(string username, int choice)
	{


		if (votes.ContainsKey (username)) {
			votes [username] = choice; 
		} else {
			votes.Add (username, choice); 
		}


		//reload graph view when a vote has been cast 
		updateVotes (); 
	}



	void updateVotes() {

		//change size of graph rows 
		row1 = GameObject.Find("greg_block");
		row2 = GameObject.Find("bruno_block");
		row3 = GameObject.Find("alix_block");
		row4 = GameObject.Find("olivia_block");
		row5 = GameObject.Find("duke_block");

		float scale = 0.0f; 

		scale = ((float)getVoteOf (0)) / ((float)getVotesTotal ());
		if (row1 != null) {
			row1.transform.localScale = new Vector3(scale, 0.67f, 1.0f);
		}

		scale = ((float)getVoteOf (1)) / ((float)getVotesTotal ());
		if (row2 != null) {
			row2.transform.localScale = new Vector3(scale, 0.67f, 1.0f);
		}


		scale = ((float)getVoteOf (2)) / ((float)getVotesTotal ());
		if (row3 != null) {
			row3.transform.localScale = new Vector3(scale, 0.67f, 1.0f);
		}

		scale = ((float)getVoteOf (3)) / ((float)getVotesTotal ());
		if (row4 != null) {
			row4.transform.localScale = new Vector3(scale, 0.67f, 1.0f);
		}


		scale = ((float)getVoteOf (4)) / ((float)getVotesTotal ());
		if (row5 != null) {
			row5.transform.localScale = new Vector3(scale, 0.67f, 1.0f);
		}

	}


	void updateQuestionVotes() {
		float scale = 0.0f; 

		//change size of graph rows 
		q1 = GameObject.Find ("q0_block"); 
		q2 = GameObject.Find ("q1_block"); 
		q3 = GameObject.Find ("q2_block"); 

		scale = ((float)getQuestionVoteOf (2)) / ((float)getQuestionCountTotal ());
		if (q3 != null) {
			q3.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
		}

		scale = ((float)getQuestionVoteOf (1)) / ((float)getQuestionCountTotal ());

		if (q2 != null) {

			q2.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
		}


		scale = ((float)getQuestionVoteOf (0)) / ((float)getQuestionCountTotal ());
		if (q1 != null) {
			q1.transform.localScale = new Vector3 (scale, 1.0f, 1.0f);
		}
	}


	//gets called when a vote for a question has been cast 
	public void Question(string username, int choice) {
		if (questionChoice.ContainsKey (username)) {
			questionChoice [username] = choice; 
		} else {
			questionChoice.Add (username, choice); 
		}

	
		updateQuestionVotes (); 



	}



	//get num of votes for X choice 
	public int getVoteOf(int choice) {
		int count = 0; 
		foreach (string key in votes.Keys) {
			int val = (int)votes[key];
			if (val == choice) {
				count++; 
			}
		}
		return count; 
	}

	//get total votes cast
	public int getVotesTotal() {
		return votes.Keys.Count; 
	}


	//get num of votes for X choice 
	public int getQuestionVoteOf(int choice) {
		int count = 0; 
		foreach (string key in questionChoice.Keys) {
			int val = (int)questionChoice[key];
			if (val == choice) {
				count++; 
			}
		}
		return count; 
	}

	//get total votes cast
	public int getQuestionCountTotal() {
		return questionChoice.Keys.Count; 
	}



	//listener that gets called when vote has been cast and parses the message from the twitch screen 
	public void convertMsg(string str) {


		Debug.Log (str); 


		//Remove non-command parts of message (like username)
		int msgIndex = str.IndexOf("PRIVMSG #");
		string msgSubStr = str.Substring(msgIndex + 9);
		string username = str.Substring (1, (str.IndexOf ("!") - 1) ); 



		//parse and get the command and parameter cast 
		str = str.Substring(msgIndex + IRC.nickName.Length + 11);
		//Allow non delimited commands using the entire string (ie 'A' for A-button instead of 'button: A')
		string cmd = str;
		if(delimiter.Length > 0 && str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries).Length > 1)
		{
			string[] blocks = str.Split(delimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
			cmd = blocks[0];
			str = "";
			for(int i=1; i<blocks.Length; i++)
			{
				str += blocks[i];
			}
				
		}

		int x = -1; 

		//cast votes for appropropriate command 
		if (cmd.Contains ("Question") && !(str.Equals("Question") || str.Equals("Question:") )) {

		
			if (int.TryParse(str, out x))
			{
				// you know that the parsing attempt
				// was successful
				Question (username, x); 
			}




		}
		if (cmd.Contains ("Vote") && !(str.Equals("Vote") || str.Equals("Vote:") )) {


			if (int.TryParse(str, out x))
			{
				// you know that the parsing attempt
				// was successful
				Vote (username, x); 
			}
				
		}

		if (str.Equals("History")) {
			displayMessage ("Foo"); 
		}


	}


}
