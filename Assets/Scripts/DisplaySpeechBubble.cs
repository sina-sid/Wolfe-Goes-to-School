using UnityEngine;
using System.Collections;

public class DisplaySpeechBubble : MonoBehaviour {

	private SpriteRenderer bubble;

	// Use this for initialization
	void Start () {
		bubble = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (StoryTextUITextTypewriter.storyIntroPlayed) {
			bubble.sortingOrder = 2;
		} 
	}
}
