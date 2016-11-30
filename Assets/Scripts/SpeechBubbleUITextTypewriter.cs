using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBubbleUITextTypewriter : MonoBehaviour {

	Text txt;
	public string response;
	public bool responsePlayed;

	void Awake()
	{
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
		//yield return new WaitUntil(bool);
		yield return new WaitForSeconds(7);
		foreach (char c in response)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.03f);
		}
		responsePlayed = true;
	}

	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}
}
