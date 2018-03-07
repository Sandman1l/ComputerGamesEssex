using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovController : MonoBehaviour
{

	public GameObject GM;
	public List<Vector2> movements;
	public int limitX = 20;
	public int limitY = 20;
	// Use this for initialization
	void Start ()
	{
		GM = GameObject.FindGameObjectWithTag ("GameMaster");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public List<Vector2> UpdateGrid (Vector2 target, int mov)
	{

		if (movements.Count != 0) {
			movements.Clear();
		}
		allFourSides (target, mov);

		return movements;
	}
		

	// MARK : Utility

	void lastCheck(){

		foreach (Vector2 vt in movements) {
			//Debug.Log (GM.GetComponent<GameMaster> ().obstacles[0]);
			if (GM.GetComponent<GameMaster> ().obstacles.Contains (vt)) {
				//Debug.Log (vt);
				movements.Remove (vt);
			}
		}

	}
	void addMovVectorTwo(float x,float y){
		//We check the obstacles again because of the sides.
		//the first check are the limits for the map!
		if ((limitX >= x && x >= 0) && (limitY >= y && y >= 0)) {
			Vector2 nVtwo = new Vector2 (x,  (y));
			if (!(GM.GetComponent<GameMaster> ().playerChar.Contains (nVtwo)))
			{
				if (!(GM.GetComponent<GameMaster> ().obstacles.Contains (nVtwo)))
				{
					if (!movements.Contains (nVtwo))
					{
						movements.Add (nVtwo);
					}

				}
			}
		}
	}

	//				 (1,4)
	//			(0,3)(1,3)(2,3)
	//			(0,2)(1,2)(2,2)(3,2)
	//			(0,1)(1,1)(2,1)
	//				 (1,0)
	void allFourSides(Vector2 target, int mov){
		GM = GameObject.FindGameObjectWithTag ("GameMaster");

			//UP
				for (int j = 1; j <= mov; j++) {
					Vector2 aux = new Vector2 (target.x, (target.y + j));
					//Debug.Log (GM);
					if ((GM.GetComponent<GameMaster> ().obstacles.Contains (aux))) {
						break;
					}
					addMovVectorTwo (target.x, (target.y + j));
					for (int k = j; k < mov; k++) {
						//add restrictions and limitations according to GameMaster
						addMovVectorTwo (target.x - k, (target.y + j));
						addMovVectorTwo (target.x + k, (target.y + j));
					}
				}

			// DOWN
				for (int j = 1; j <= mov; j++) {
					Vector2 aux = new Vector2 (target.x, (target.y - j));
					if ((GM.GetComponent<GameMaster> ().obstacles.Contains (aux))) {
						break;
					}
					addMovVectorTwo (target.x, (target.y - j));
					for (int k = j; k < mov; k++) {
						//add restrictions and limitations according to GameMaster
						addMovVectorTwo (target.x - k, (target.y - j));
						addMovVectorTwo (target.x + k, (target.y - j));
					}
				}


			// left
				for (int j = 1; j <= mov; j++) {
					Vector2 aux = new Vector2 ((target.x - j), target.y);
					if ((GM.GetComponent<GameMaster> ().obstacles.Contains (aux))) {
						break;
					}
					addMovVectorTwo ((target.x - j), target.y);
					for (int k = j; k < mov; k++) {
						//add restrictions and limitations according to GameMaster
						addMovVectorTwo ((target.x - j), target.y - k);
						addMovVectorTwo ((target.x - j), target.y + k);
					}
				}
			//RIGHT
				for (int j = 1; j <= mov; j++) {
					Vector2 aux = new Vector2 ((target.x + j), target.y);
					if ((GM.GetComponent<GameMaster> ().obstacles.Contains (aux))) {
						break;
					}
					addMovVectorTwo ((target.x + j), target.y);
					for (int k = j; k < mov; k++) {
						//add restrictions and limitations according to GameMaster
						addMovVectorTwo ((target.x + j), target.y - k);
						addMovVectorTwo ((target.x + j), target.y + k);
					}
				}

	}
}








/*for (int i = 0; i < 4; i++) 
		{
			switch (i){
			case 0:
				
				for (int j = 1; j <= mov; j++) 
				{
					movements.Add(new Vector2 (target.x, (target.y + j)));

					for (int k = j; k < mov; k++)
					{
						//add restriction
						movements.Add(new Vector2 (target.x-k, (target.y + j)));
					}
					for (int k = j; k < mov; k++)
					{
						//add restriction
						movements.Add(new Vector2 (target.x+k, (target.y + j)));
					}
				}

			break;


			default:
				break;
			}
				
		
		}*/

