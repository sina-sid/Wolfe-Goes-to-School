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
		Scene scene = SceneManager.GetActiveScene();
		sceneName = scene.name;
		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
		//		Debug.Log (curTime.ToString ());
		if (curTime <= 0) {
			if (sceneName == "Opening") {
				SceneManager.LoadScene ("round1Start");
			} else if (sceneName == "RoundEnd") {
				if (logicTree.getRoundNumber () == 1) {
					SceneManager.LoadScene ("round2Start");
				} else if (logicTree.getRoundNumber () == 2) {
					SceneManager.LoadScene ("round3Start");
				} else {
				}
			}
		}
	}
}
