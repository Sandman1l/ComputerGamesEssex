using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map  {

	public  Vector3 WorldToMap(Vector3 worldPos, Vector3 offset)
    {
        return worldPos - offset;
    }
    public Vector3 MapToWorld(Vector3 mapPos, Vector3 offset)
    {
        return mapPos + offset;
    }
}
