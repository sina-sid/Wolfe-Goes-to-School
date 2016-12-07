using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VotingTransitionScript : MonoBehaviour {

	public float changeAfter = 70;
	private string sceneName;

	// Use this for initialization
	void Start () {
		Scene scene = SceneManager.GetActiveScene();
		sceneName = scene.name;
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
//		Debug.Log (curTime.ToString ());
		if (curTime <= 0) {
			
			changeScene ();
		}
	}

	public void changeScene () {
		if (sceneName == "votingQuestionScene") {
			SceneManager.LoadScene ("questionAnswerScene");
		} else if (sceneName == "votingStudentScene") {
			SceneManager.LoadScene ("classroomStoryScene");
		}
	}
}
