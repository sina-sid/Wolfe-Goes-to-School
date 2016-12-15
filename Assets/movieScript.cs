using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
		Debug.Log (Time.timeSinceLevelLoad.ToString ());
		//		Debug.Log (curTime.ToString ());
		if (movie.isPlaying == false) {
			if (sceneName == "Opening") {
				SceneManager.LoadScene ("round1Start");
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 1) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("round2Start"); 
				}
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 2) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("round3Start"); 
				}
			} else if (sceneName == "RoundEnd" && logicTree.getRoundNumber() == 3) {
				if (logicTree.isWolfKilled) {
					SceneManager.LoadScene ("GameWin");
				} else {
					SceneManager.LoadScene ("GameLose"); 
				}
			}
		}
	}
}
