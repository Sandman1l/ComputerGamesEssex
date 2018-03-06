using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovController : MonoBehaviour
{

	private GameObject GM;
	public List<Vector2> movements;
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


		//				 (1,4)
		//			(0,3)(1,3)(2,3)
		//			(0,2)(1,2)(2,2)(3,2)
		//			(0,1)(1,1)(2,1)
		//				 (1,0)
		if (movements.Count != 0) {
			movements.Clear();
		}
		for (int j = 1; j <= mov; j++) 
		{
			movements.Add(new Vector2 (target.x, (target.y + j)));

			for (int k = j; k < mov; k++)
			{
				//add restrictions
				movements.Add(new Vector2 (target.x-k, (target.y + j)));
				movements.Add(new Vector2 (target.x+k, (target.y + j)));
			}
		}

		return movements;
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

