using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubbleUITextTypewriter : MonoBehaviour {

	Text txt;
	public string response;
	public static bool responsePlayed;
	private static bool startSpeech;
	bool test;

	void Awake()
	{
		
		//Debug.Log (startSpeech.GetType());
		responsePlayed = false;
		txt = GetComponent<Text>(); // temporary! should be selected from another script that deals with the logic 

		response = txt.text;
		txt.text = "";

		// TODO: need a way to find out when the story is complete (use global bool?) and then start Coroutine
		StartCoroutine("PlayText");
	}

	public void startAnimation ()
	{
		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{
		yield return new WaitUntil(() => startSpeech);
		yield return new WaitForSeconds(1);
		foreach (char c in response)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.03f);
		}
		responsePlayed = true;
		StoryTextUITextTypewriter.storyIntroPlayed = false;
	}
		
	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}

	void Update ()
	{
		startSpeech = StoryTextUITextTypewriter.storyIntroPlayed;
	}
}
