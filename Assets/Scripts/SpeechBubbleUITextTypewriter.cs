using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubbleUITextTypewriter : MonoBehaviour {

	Text txt;
	public string currentResponse;
	public static bool responsePlayed;
	private static bool startSpeech;
	public GameObject camera;
	public GameObject bubble;
	public GameLogicTree logicTree; 
	bool needToMove;
	int roundNumber;
	int questionNumber;
	int counter = 0; 

	public GameObject scriptsObj; 

	void getResponse() {

		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;

		string response = "NO RESPONSE SELECTED";

		Debug.Log ("Question num: " + questionNumber); 




	


		if (questionNumber == 1) {
			if (!logicTree.rounds [logicTree.getRoundNumber ()].Q1.getCharacter ("Greg").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Greg").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Greg").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Bruno").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Bruno").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Bruno").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Alix").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Alix").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Alix").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Olivia").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Olivia").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Olivia").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Duke").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Duke").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q1.getCharacter ("Duke").isResponsePlayed = true; 
			}
		} else if (questionNumber == 2) {
			if (!logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Greg").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Greg").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Greg").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Bruno").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Bruno").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Bruno").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Alix").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Alix").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Alix").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Olivia").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Olivia").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Olivia").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Duke").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Duke").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q2.getCharacter ("Duke").isResponsePlayed = true; 
			}
		} else {
			if (!logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Greg").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Greg").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Greg").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Bruno").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Bruno").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber ()].Q3.getCharacter ("Bruno").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Alix").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Alix").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber ()].Q3.getCharacter ("Alix").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Olivia").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Olivia").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber ()].Q3.getCharacter ("Olivia").isResponsePlayed = true; 
			} else if (!logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Duke").isResponsePlayed) {
				response = logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Duke").getRandomResponse ();
				logicTree.rounds [logicTree.getRoundNumber () ].Q3.getCharacter ("Duke").isResponsePlayed = true; 
			}
		}






		currentResponse = response;


	}

	void Start() {
		camera = GameObject.Find ("Main Camera");
		bubble = GameObject.Find ("Speech bubble");
		scriptsObj = GameObject.Find ("Scripts"); 

		needToMove = false;
		txt = GetComponent<Text>(); // temporary! should be selected from another script that deals with the logic 
		currentResponse = txt.text;
		// txt.text = "";

		// TODO: need a way to find out when the story is complete (use global bool?) and then start Coroutine
		StartCoroutine("PlayText");
	}
	
	void Awake()
	{
		//Debug.Log (startSpeech.GetType());
	}

	IEnumerator PlayText()
	{
		yield return new WaitUntil(() => startSpeech);
		yield return new WaitForSeconds(0.5f);
		getResponse (); 
		foreach (char c in currentResponse)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.03f);
		}
		responsePlayed = true;
		needToMove = true;
		StoryTextUITextTypewriter.storyIntroPlayed = false;
	}

	IEnumerator nextCharacter() {
		yield return new WaitForSeconds(1f);
		camera.transform.position += Vector3.left * 10;
		bubble.transform.position += Vector3.left * 10;
		StartCoroutine ("PlayText"); 
	}

	void Update ()
	{
		startSpeech = StoryTextUITextTypewriter.storyIntroPlayed;
		if (responsePlayed && needToMove) {
			StartCoroutine("nextCharacter");
			needToMove = false;
		}
	}
}
