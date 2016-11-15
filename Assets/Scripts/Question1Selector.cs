using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Question1Selector : MonoBehaviour {
	public Dictionary<int, string> question1_list = new Dictionary<int, string>();
	Text question1;
	int round_number = 0; //this needs to be declared somewhere else as a global variable

	// Use this for initialization
	void Start () {
		question1 = GetComponent<Text>();


		question1_list.Add(0, "Where were you last night?");
		question1_list.Add(1, "Who do you trust?");
		question1_list.Add(2, "QUESTION NOT AVAILABLE");
	
	}
	
	// Update is called once per frame
	void Update () {
		question1.text = question1_list[round_number];
	
	}
}
