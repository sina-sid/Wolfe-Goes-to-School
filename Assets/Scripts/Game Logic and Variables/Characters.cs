using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct character {
	public bool isAlive;
	public bool hasSpoken;
	public string responseType;
	public string response;
}


public class Characters : MonoBehaviour {

	character Greg = new character ();
	character Bruno = new character ();
	character Alix = new character ();
	character Olivia = new character ();
	character Duke = new character ();

	// Use this for initialization
	public void Start () {

		Greg.isAlive = true;
		Bruno.isAlive = true;
		Alix.isAlive = true;
		Olivia.isAlive = true;
		Duke.isAlive = true;

		Greg.hasSpoken = false;
		Bruno.hasSpoken  = false;
		Alix.hasSpoken = false;
		Olivia.hasSpoken = false;
		Duke.hasSpoken = false;
	
		resetCharactersForNextRound ();
	}

	public void resetCharactersForNextRound () {
		Greg.responseType = "";
		Bruno.responseType = "";
		Alix.responseType = "";
		Olivia.responseType = "";
		Duke.responseType = "";

		Greg.response = "";
		Bruno.response = "";
		Alix.response = "";
		Olivia.response = "";
		Duke.response = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
