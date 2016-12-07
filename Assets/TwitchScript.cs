using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TwitchScript : MonoBehaviour {

	TwitchIRC IRC; 
	bool connected; 
	public string delimiter;

	// Use this for initialization
	void Start () {
		IRC = this.GetComponent<TwitchIRC>();

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


	public void Vote(string username, string choice)
	{
		Debug.Log (username + " voted: " + choice); 
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


		if (cmd.Contains ("Vote")) {
			Vote (username, str); 
		}


	}


}
