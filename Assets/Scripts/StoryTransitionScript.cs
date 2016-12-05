using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryTransitionScript : MonoBehaviour {

	private string sceneName;
	private float waitTime;

	// Use this for initialization
	void Start () {
		waitTime = 3f;
		Scene scene = SceneManager.GetActiveScene();
		sceneName = scene.name;
		//Debug.Log ("Hi");
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneName == "classroomStoryScene" && StoryTextUITextTypewriter.storyIntroPlayed) {
			StoryTextUITextTypewriter.storyIntroPlayed = false;
			StartCoroutine ("changeScene");
		}
	}
		
	IEnumerator changeScene()
	{
		yield return new WaitForSeconds (waitTime);
		if (sceneName == "classroomStoryScene") {
			SceneManager.LoadScene ("votingQuestionScene");
		}
	}
}
