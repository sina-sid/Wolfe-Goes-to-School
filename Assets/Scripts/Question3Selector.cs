using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Question3Selector : MonoBehaviour {
	public Dictionary<int, string> question2_list = new Dictionary<int, string>();
	Text question2;
	int round_number = 0; //this needs to be declared somewhere else as a global variable

	// Use this for initialization
	void Start () {
		question2 = GetComponent<Text>();


		question2_list.Add(0, "What's your favorite game to play?");
		question2_list.Add(1, "What's your favorite subject in school?");
		question2_list.Add(2, "QUESTION NOT AVAILABLE");

	}

	// Update is called once per frame
	void Update () {
		question2.text = question2_list[round_number];

	}
}
