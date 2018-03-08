using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{


	public List<Vector2> enemies;
	public List<Vector2> playerChar;
	public List<Vector2> obstacles;
	// Use this for initialization

	void Start ()
	{
		ObstaclesInit ();
		PlayerPositionInit ();

	}

	// Update is called once per frame
	void Update ()
	{


	}






	// MARK: INIT functions


	//Doesn't take much to do this functions  but the Update Positions are escential for the movement Controller.
	void PlayerPositionInit()
	{
		List<GameObject> pl = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Character"));
		foreach (GameObject pieze in pl)
		{
			playerChar.Add(pieze.GetComponent<Transform>().position);
		}
	}

	void ObstaclesInit()
	{
		List<GameObject> enem = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
		foreach (GameObject pieze in enem)
		{
			enemies.Add(pieze.GetComponent<Transform>().position);
		}
		//for actual tag == obstacle
		/*List<GameObject> enem = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
		foreach (GameObject pieze in enem)
		{
			obstacles.Add(pieze.GetComponent<Transform>().position);
		}*/

	}
//The initialization of the player and enemy pieces NEED to happen AFTER their coordinates have been taken
// otherwise there will be problems with the movement manager.
	void initAllPlEne()
	{

		List<GameObject> pl = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Character"));
		foreach (GameObject pieze in pl)
		{
			pieze.GetComponent<CharController>().initPlEne();
		}
		List<GameObject> enem = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
		foreach (GameObject pieze in enem)
		{
			pieze.GetComponent<CharController>().initPlEne();
		}
	}


}
