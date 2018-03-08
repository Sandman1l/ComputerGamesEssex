using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
		List<Vector2> atkAux;
		public int limitX = 20;
		public int limitY = 20;



		public List<Vector2> actualAttackPos( Vector2 pos, int range)
		{
			if (atkAux.Count != 0) {
				atkAux.Clear ();
			}
			addAtkVtwo((pos.x+range),pos.y);
			addAtkVtwo((pos.x-range),pos.y);
			addAtkVtwo(pos.x,(pos.y+range));
			addAtkVtwo(pos.x,(pos.y-range));
			if(range == 2){
				addAtkVtwo((pos.x-1),(pos.y+1));
				addAtkVtwo((pos.x+1),(pos.y+1));
				addAtkVtwo((pos.x-1),(pos.y-1));
				addAtkVtwo((pos.x+1),(pos.y-1));
			}

			return null
		}






// MARK: Grid update functions
	public List<Vector2> UpdateGrid (List<Vector2> vtPosMov, int range)
	{
		if (atkAux.Count != 0) {
			atkAux.Clear ();
		}
		atkAux = new List<Vector2>(vtPosMov);

		foreach(Vector2 vt in vtPosMov){
			addAtkVtwo((vt.x+range),vt.y);
			addAtkVtwo((vt.x-range),vt.y);
			addAtkVtwo(vt.x,(vt.y+range));
			addAtkVtwo(vt.x,(vt.y-range));
			if(range == 2){
				addAtkVtwo((pos.x-1),(pos.y+1));
				addAtkVtwo((pos.x+1),(pos.y+1));
				addAtkVtwo((pos.x-1),(pos.y-1));
				addAtkVtwo((pos.x+1),(pos.y-1));
			}
		}

		return atkAux;
	}

	void addAtkVtwo (float x, float y)
	{
		//the first check are the limits for the map!
		if ((limitX >= x && x >= 0) && (limitY >= y && y >= 0)) {
			Vector2 nVtwo = new Vector2 (x, y);
					if (!atkAux.Contains (nVtwo)) {
						atkAux.Add (nVtwo);
					}
		}
	}

}
