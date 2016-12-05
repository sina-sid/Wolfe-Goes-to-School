using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	Text timer;
	public float maxTime = 60;

	// Use this for initialization
	void Start () {
		timer = GetComponent<Text>();
	
	}
	
	// Update is called once per frame
	void Update () {
		int curTime = (int)(maxTime - Time.timeSinceLevelLoad);
		//string secMillsec = string.Format("{0}:{1:00}", (int)curTime, (int)curTime % 60);
		//Debug.Log(curTime.ToString());

		if (curTime <= 10) {
			timer.color = Color.red;
		}

		if (curTime <= 0) {
			timer.text = "00:00";
		} else {
			int minutes = Mathf.FloorToInt(curTime / 60F);
			int seconds = Mathf.FloorToInt(curTime - minutes * 60);
			string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);	
			timer.text = niceTime;
			// when answer chosen: highlight most voted answer, change scene
		}
	}
}
