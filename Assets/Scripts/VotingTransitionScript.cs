using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VotingTransitionScript : MonoBehaviour {

	public float changeAfter = 70;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(changeAfter - Time.fixedTime);
		if (curTime <= 0) {
			changeScene ();
		}
	}

	public void changeScene () {
		SceneManager.LoadScene ("questionAnswerScene");

	}
}
