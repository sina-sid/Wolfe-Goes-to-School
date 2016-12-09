using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwitchScript : MonoBehaviour {

	TwitchIRC IRC; 
	bool connected; 
	public string delimiter;

	public Hashtable votes; 
	public Hashtable questionChoice; 

	// Use this for initialization
	void Start () {
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
				max = val; 
			}
		}

		return max; 
	}



	public int votesWinner() {
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
				max = val; 
			}
		}
		return max; 
	}


	public void displayMessage(string str) {
		IRC.SendMsg (str); 
	}
		
	public void Vote(string username, int choice)
	{
		votes.Add (username, choice); 
	}

	public void Question(string username, int choice) {
		questionChoice.Add (username, choice); 
	}



	public void convertMsg(string str) {



		//Remove non-command parts of message (like username)
		int msgIndex = str.IndexOf("PRIVMSG #");
		string msgSubStr = str.Substring(msgIndex + 9);
		string username = msgSubStr.Substring (0, msgSubStr.IndexOf (" "));



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
