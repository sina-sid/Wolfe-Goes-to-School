using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (SpeechBubbleUITextTypewriter.responsePlayed) {
			transform.position += Vector3.left * 10;
			SpeechBubbleUITextTypewriter.responsePlayed = false;
		}
	}
}
