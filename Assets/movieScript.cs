using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Plays the video for both cutscenes and handles scene transitions for those scenes

public class movieScript : MonoBehaviour {

	public float changeAfter = 16;
	public MovieTexture movie;
	public string sceneName;
	public GameLogicTree logicTree;

	// Use this for initialization
	void Start () {
		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		Scene scene = SceneManager.GetActiveScene();
		sceneName = scene.name;
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
		//Debug.Log (Time.timeSinceLevelLoad.ToString ());
		//		Debug.Log (curTime.ToString ());
		if (curTime <= 0) {
			if (sceneName == "Opening") {
				SceneManager.LoadScene ("round1Start");
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 1 && !movie.isPlaying) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("round2Start"); 
				}
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 2 && !movie.isPlaying) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("round3Start"); 
				}
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 3 && !movie.isPlaying) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("GameLose"); 
				}
			}
		}
	}
}
