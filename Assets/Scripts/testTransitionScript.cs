using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class testTransitionScript : MonoBehaviour {

	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.08f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
//	void Update () { //does not work
//		Scene currentScene = SceneManager.GetActiveScene ();
//		if (currentScene.name == "classroomStoryScene" && Time.fixedTime > 5) {
//			float fadeTime = BeginFade (1);
//			yield return new WaitForSeconds (fadeTime);
//			SceneManager.LoadScene ("questionAnswerScene");
//		}
//	}
		

	void onGUI() {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect( 0, 0, Screen.width, Screen.height), fadeOutTexture );

	}

	public float BeginFade(int direction) {
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelLoaded() {
		BeginFade (-1);
	}

	public void changeScene () {
		SceneManager.LoadScene ("testScene");

	}
}
