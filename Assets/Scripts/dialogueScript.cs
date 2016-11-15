using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class dialogueScript : MonoBehaviour {





	Text doctor_answer;

	// Use this for initialization
	void Start () {
		Dictionary<string, string> round1_question1_doctor_answers = new Dictionary<string, string>();
		doctor_answer = GetComponent<Text>();

		round1_question1_doctor_answers.Add("Snarky", "If you must know, I was performing an operation - which was probably more important than anything you were doing.");
		round1_question1_doctor_answers.Add("Dramatic", "Only saving lives!! Not that it matters now that this wolf is on the prowl.");
		round1_question1_doctor_answers.Add("Humorous", "Well, I was wheeling a patient into the operation room, but then he had a change of heart.");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
