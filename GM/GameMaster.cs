using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{


	public List<Vector2> enemies;
	public List<Vector2> playerChar;
	// Use this for initialization
	void Start ()
	{
		updateEnemyPositions ();
		updatePlayerPositions ();
	}

	// Update is called once per frame
	void Update ()
	{
		
	}



	//Doesn't take much to do this functions  but the Update Positions are escential for the movement Controller.
	void updateEnemyPositions()
	{
		List<GameObject> enem = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Enemy"));
		foreach (GameObject pieze in enem) 
		{
			enemies.Add(pieze.GetComponent<Transform>().position);
		}
	}

	void updatePlayerPositions()
	{
		List<GameObject> pl = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Character"));
		foreach (GameObject pieze in pl) 
		{
			playerChar.Add(pieze.GetComponent<Transform>().position);
		}
	}


}
