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
	public TwitchScript twitch; 

	void Start()
	{
		Scene scene = SceneManager.GetActiveScene ();
		sceneName = scene.name;
		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;
		twitch = FindObjectOfType(typeof(TwitchScript)) as TwitchScript;
		storyIntroPlayed = false;
		txt = GetComponent<Text>();
		getStory ();

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

	void getStory () {
		if (sceneName == "classroomStoryScene" && logicTree.getRoundNumber () == 0) {
			story = "You know one of your students is actually a wolf in disguise and today you plan to ask them questions and decide who you think is the wolf. If you choose wrong, the wolf lives another day and you get another chance to investigate. If you choose right, you can sleep better tonight knowing that your students and your town are safe once again. Good luck!";
		} 
		else if (sceneName == "studentVoteReveal") {
			string voteWinner = twitch.votesWinner (); 
			logicTree.kill (voteWinner); 
			story = "You chose " + voteWinner + ". Your other students drag them to the tetherball court to teach them a lesson.";
		} 
		else {
			story = txt.text;
		}
	}

	public void clearText ()
	{
		StopAllCoroutines();
		txt.text = "";
	}

}