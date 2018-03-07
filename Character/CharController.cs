using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour {


	/*
	 *  The Character Controller holds all the necesary information about a character or an enemy.
	 *  The functions here don't do much as they only handle the existance of the gameobject it is attached to.
	*/

	public string chName;

	public int health;
	//Resistance, it tells us how much damage is taken out when dealing magic damage.
	public int Res;
	//Defence, it tells us how much damage is taken out when dealing with physical attacks.
	public int def;
	//Skill, it measous how good a character will be on the hit probability as a bonus. 
	public int skill;
	//Movement, how much a character is able to move in the game world.
	public int mov;
	//Level, the level of a character, at this stage is just for show.
	public int level;

	//Vector2 Posible Movements are in accordance to the mov variable and the MovementController deals with this. 
	public List<Vector2> vtPosMov;
	//This is escenetially the gameobject that holds our movController
	private GameObject movManager;
	// Use this for initialization
	void Start () {
		
		movManager = GameObject.FindGameObjectWithTag ("MoveController");
		StartCoroutine (LateStart ());

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator LateStart(){
		yield return new WaitForSeconds (1);
		vtPosMov = new List<Vector2>(movManager.GetComponent<MovController>().UpdateGrid(transform.position,mov));
	}

}
