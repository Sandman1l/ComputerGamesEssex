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

	public bool player;
	//Vector2 Posible Movements are in accordance to the mov variable and the MovementController deals with this.
	public List<Vector2> vtPosMov;
	//This is escenetially the gameobject that holds our movController
	private GameObject movManager;
	private GameObject atkManager;
	public List<Vector2> vtPosAtk;
	public List<Vector2> vtActAtk;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void initPlEne(){
		movManager = GameObject.FindGameObjectWithTag ("MoveController");
		atkManager = GameObject.FindGameObjectWithTag ("CombatManager");
		vtPosMov = new List<Vector2>(movManager.GetComponent<MovController>().UpdateGrid(transform.position,mov,player));
		vtPosAtk = new List<Vector2>(atkManager.GetComponent<CombatManager>().UpdateGrid(vtPosMov));
	}


	IEnumerator LateStart(){
		yield return new WaitForSeconds (1);

	}

}
