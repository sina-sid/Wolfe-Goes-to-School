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

	public character Greg;
	public character Bruno;
	public character Alix;
	public character Olivia;
	public character Duke;

	// Use this for initialization
	public void Start () {
		Greg = new character ();
		Bruno = new character ();
		Alix = new character ();
		Olivia = new character ();
		Duke = new character ();

		Debug.Log ("Ran.");
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
