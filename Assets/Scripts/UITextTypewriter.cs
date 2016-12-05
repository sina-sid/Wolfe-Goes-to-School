using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypewriter : MonoBehaviour
{

	Text txt;
	public string story;
	private float speed;

	void Awake()
	{
		speed = 0.03f;
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
			yield return new WaitForSeconds(speed);
		}
	}

	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}

}