using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class movieScript : MonoBehaviour {

	public float changeAfter = 16;

	// Use this for initialization
	void Start () {
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
		//		Debug.Log (curTime.ToString ());
		if (curTime <= 0) {
			SceneManager.LoadScene ("classroomStoryScene");
		}
	}
}
