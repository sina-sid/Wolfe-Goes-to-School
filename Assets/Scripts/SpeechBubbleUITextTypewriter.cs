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
	bool needToMove;
	int roundNumber;
	int questionNumber;

	void getResponse() {
		string response = "NO RESPONSE SELECTED";
		if (questionNumber == 1) {
			if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Greg").isResponsePlayed) {
				response = GameLogicTree.Instance.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Greg").getRandomResponse ();
			} else if (!GameLogicTree.Instance.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Bruno").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Bruno").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Alix").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Alix").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Olivia").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Olivia").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Duke").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q1.getCharacter ("Duke").getRandomResponse ();
			}
		} else if (questionNumber == 2) {
			if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Greg").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Greg").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Bruno").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Bruno").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Alix").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Alix").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Olivia").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Olivia").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Duke").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q2.getCharacter ("Duke").getRandomResponse ();
			}
		} else {
			if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Greg").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Greg").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Bruno").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Bruno").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Alix").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Alix").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Olivia").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Olivia").getRandomResponse ();
			} else if (!GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Duke").isResponsePlayed) {
				response = GameLogicTree.rounds [GameLogicTree.getRoundNumber () - 1].Q3.getCharacter ("Duke").getRandomResponse ();
			}
		}
		currentResponse = response;
	}

	void Start() {
		camera = GameObject.Find ("Main Camera");
		bubble = GameObject.Find ("Speech bubble");
//		roundNumber = GameLogicTree.getRoundNumber ();
//		questionNumber = TwitchScript.questionWinner ();
		needToMove = false;
		txt = GetComponent<Text>(); // temporary! should be selected from another script that deals with the logic 
//		response = txt.text;
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
