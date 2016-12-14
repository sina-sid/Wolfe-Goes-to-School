using UnityEngine;
using System.Collections;

public class changeRound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		GameLogicTree logicTree = FindObjectOfType(typeof(GameLogicTree)) as GameLogicTree;

		logicTree.setRoundNumber (logicTree.getRoundNumber++); 
	

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
