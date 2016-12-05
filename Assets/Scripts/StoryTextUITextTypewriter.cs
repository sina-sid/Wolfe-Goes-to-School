using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class StoryTextUITextTypewriter : MonoBehaviour
{

	Text txt;
	public string story;
	public static bool storyIntroPlayed;

	void Awake()
	{
		storyIntroPlayed = false;
		txt = GetComponent<Text>();

		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine("PlayText");
	}

	public void startAnimation ()
	{
		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.03f);
		}
		storyIntroPlayed = true;
	}

	void Update () {
		Debug.Log (storyIntroPlayed.ToString ());
	}

	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}

}