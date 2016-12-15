using UnityEngine;
using System.Collections;

public class changeRound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GameLogicTree logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;

		logicTree.setRoundNumber (logicTree.getRoundNumber() + 1); 
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
