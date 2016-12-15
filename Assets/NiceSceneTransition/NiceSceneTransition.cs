using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Handles the fade-to-black transition for all scenes except the cutscenes

public class NiceSceneTransition : MonoBehaviour {

    public static NiceSceneTransition instance;
	private string sceneName;
	public float changeAfter = 30;
	public GameLogicTree logicTree;

    public float transitionTime = 1.0f;

    public bool fadeIn;
    public bool fadeOut;

    public Image fadeImg;

    float time = 0f;

    // Use this for initialization
    void Awake()
    {
		logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;
        if (instance == null) {
            DontDestroyOnLoad(transform.gameObject);
            instance = this;
            if (fadeIn) {
                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, 1.0f);
            }
        } else {
            Destroy(transform.gameObject);
        }
    }

	void Start() {
		
	}

    void OnEnable()
    {
        if (fadeIn)
        {
            StartCoroutine(StartScene());
        }
    }

    public void LoadScene(string level)
    {
        StartCoroutine(EndScene());
    }

    public IEnumerator StartScene()
    {
        time = 1.0f;
        yield return null;
        while (time >= 0.0f)
        {
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, time);
            time -= Time.deltaTime * (1.0f / transitionTime);
            yield return null;
        }
        fadeImg.gameObject.SetActive(false);
    }

    public IEnumerator EndScene()
    {
        fadeImg.gameObject.SetActive(true);
        time = 0.0f;
        yield return null;
        while (time <= 1.0f)
        {
            fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, time);
            time += Time.deltaTime * (1.0f/transitionTime);
            yield return null;
        }


//		Debug.Log ("THIS RUNS " + logicTree.getRoundNumber ()); 
		Debug.Log("HELLO ROUND " + sceneName); 
//
//
//		if (logicTree.isWolfKilled) {
//			Debug.Log ("HELLO KILL"); 
//		} else {
//			Debug.Log ("HELLO NEXT"); 
//		}


		if (sceneName == "classroomStoryScene") {
			SceneManager.LoadScene ("votingQuestionScene");
		} else if (sceneName == "round1Start") {
			SceneManager.LoadScene ("round1StartPart2");
		} else if (sceneName == "round1StartPart2") {
			SceneManager.LoadScene ("classroomStoryScene");
		} else if (sceneName == "votingQuestionScene") {
			SceneManager.LoadScene ("questionAnswerScene");
		} else if (sceneName == "votingStudentScene") {
			// WARNING! AREA BELOW CONTAINS BUGS
			SceneManager.LoadScene ("studentVoteReveal");
		} else if (sceneName == "studentVoteReveal") {
			SceneManager.LoadScene ("RoundEnd");
		} else if (sceneName == "round2Start") {
			SceneManager.LoadScene ("classroomStoryScene");
		}
        StartCoroutine(StartScene());
    }

	void Update () {
		Scene scene = SceneManager.GetActiveScene ();
		sceneName = scene.name;
		int curTime = (int)(changeAfter - Time.timeSinceLevelLoad);
		if (sceneName == "classroomStoryScene" && StoryTextUITextTypewriter.storyIntroPlayed) {
			StoryTextUITextTypewriter.storyIntroPlayed = false;
			StartCoroutine (EndScene ());
			//			StartCoroutine ("changeScene");
		} else if (sceneName == "round1Start" && StoryTextUITextTypewriter.storyIntroPlayed) {
			StoryTextUITextTypewriter.storyIntroPlayed = false;
			StartCoroutine (EndScene ());
			//			StartCoroutine ("changeScene");
		} else if (sceneName == "round1StartPart2" && StoryTextUITextTypewriter.storyIntroPlayed) {
			StoryTextUITextTypewriter.storyIntroPlayed = false;
			StartCoroutine (EndScene ());
			//			StartCoroutine ("changeScene");
		} else if (sceneName == "votingQuestionScene" || sceneName == "votingStudentScene") {
			if (curTime <= 0) {
				StartCoroutine (EndScene ());
			}
		} else if (sceneName == "studentVoteReveal") {
			StoryTextUITextTypewriter.storyIntroPlayed = false;
			StartCoroutine (EndScene ());
		}
	}

}
