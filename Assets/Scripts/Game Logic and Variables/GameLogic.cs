using UnityEngine;


public class GameLogic : MonoBehaviour
{

	public static GameLogic instance = null;            
	public GameLogicTree tree;                        


	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)	

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);



		//Call the InitGame function to initialize the first level 
		InitGame();
	}

	//Initializes the game for each level.
	void InitGame()
	{
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		tree.Init(); 
		tree.setRoundNumber (0); 

	}



	//Update is called every frame.
	void Update()
	{

	}
}
