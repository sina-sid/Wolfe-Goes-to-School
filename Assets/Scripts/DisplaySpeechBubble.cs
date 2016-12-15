using UnityEngine;
using System.Collections;

//Increments round number (only called in RoundEnd scene)

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
