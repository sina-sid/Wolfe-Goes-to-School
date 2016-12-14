using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// attach to UI Text component (with the full text already there)

public class StoryTextUITextTypewriter : MonoBehaviour
{

	Text txt;
	public string story;
	public static bool storyIntroPlayed;
	public string sceneName;
	public GameLogicTree logicTree; 

	void Start()
	{
		Scene scene = SceneManager.GetActiveScene ();
		sceneName = scene.name;
		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;
		storyIntroPlayed = false;
		txt = GetComponent<Text>();
		getStory ();

		txt.text = "";
		Debug.Log (story);
		Debug.Log (logicTree.getRoundNumber().ToString());

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

	void getStory () {
		if (sceneName == "classroomStoryScene" && logicTree.getRoundNumber() == 0 ) {
			story = "You know one of your students is actually a wolf in disguise and today you plan to ask them questions and decide who you think is the wolf. If you choose wrong, the wolf lives another day and you get another chance to investigate. If you choose right, you can sleep better tonight knowing that your students and your town are safe once again. Good luck!";
		}
	}

	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}

}