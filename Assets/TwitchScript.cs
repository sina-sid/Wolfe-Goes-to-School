﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwitchScript : MonoBehaviour {

	TwitchIRC IRC; 
	bool connected; 
	public string delimiter;

	public Hashtable votes; 
	public Hashtable questionChoice; 



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

		if (IRC == null) {
			Debug.Log ("THIS IS NULL"); 
		} else {
			IRC.Connected += Connected;
			delimiter = ":"; 
			IRC.messageRecievedEvent.AddListener(getMessage);

		}
	}


	void Connected()
	{
		connected = true;
		Debug.Log("Connected to Chat");
	}

	// Update is called once per frame
	void Update () {
	
	}

	void getMessage(string msg){
		//Handle Message
		//Form: “PRIVMSG #nickName: message”
		convertMsg(msg); 

	}


	//return 
	public int questionWinner() {
		

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


		foreach (int key in choiceHash.Keys) {
			int val = (int)choiceHash[key];
			if (val > max) {
				max = key; 
			}
		}
		questionChoice.Clear (); 
		return max; 
	}



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


		Debug.Log ("This is max: " + max); 


		switch (max) {
		case 0: 
			return "Greg"; 
			break; 
		case 1: 
			return "Bruno"; 
			break; 
		case 2: 
			return "Alix"; 
			break; 
		case 3: 
			return "Olivia"; 
			break; 
		case 4: 
			return "Duke"; 
			break; 
		}

		return ""; 
	}


	public void displayMessage(string str) {
		IRC.SendMsg (str); 
	}
		
	public void Vote(string username, int choice)
	{


		if (votes.ContainsKey (username)) {
			votes [username] = choice; 
		} else {
			votes.Add (username, choice); 
		}


		Debug.Log (username + " voted " + choice); 
		float scale = 0.0f; 

		updateVotes (); 
	}



	void updateVotes() {

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


	public void Question(string username, int choice) {
		if (questionChoice.ContainsKey (username)) {
			questionChoice [username] = choice; 
		} else {
			questionChoice.Add (username, choice); 
		}

	
		updateQuestionVotes (); 



	}



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

	public int getVotesTotal() {
		return votes.Keys.Count; 
	}



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


	public int getQuestionCountTotal() {
		return questionChoice.Keys.Count; 
	}


	public void convertMsg(string str) {


		Debug.Log (str); 


		//Remove non-command parts of message (like username)
		int msgIndex = str.IndexOf("PRIVMSG #");
		string msgSubStr = str.Substring(msgIndex + 9);
		string username = str.Substring (1, (str.IndexOf ("!") - 1) ); 



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
