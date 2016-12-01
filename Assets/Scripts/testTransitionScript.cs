using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class testTransitionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Scene currentScene = SceneManager.GetActiveScene ();
		if (currentScene.name == "classroomStoryScene" && Time.fixedTime > 5) {
			SceneManager.LoadScene ("questionAnswerScene");
		}
	
	}

	public void changeScene () {
		SceneManager.LoadScene ("testScene");

	}
}
