using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movieScript : MonoBehaviour {

	public float changeAfter = 16;
	public MovieTexture movie;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
		//		Debug.Log (curTime.ToString ());
		if (curTime <= 0) {
			SceneManager.LoadScene ("round1Start");
		}
	}
}
