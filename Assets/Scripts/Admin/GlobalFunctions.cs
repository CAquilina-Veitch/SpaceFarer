using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunctions
{
    public static Vector2 posToCoord(Vector3 pos)
    {
        Vector2 temp = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.z));

        return temp;
    }

    public static Vector3 coordToPoint(Vector2 coord)
    {
        return new Vector3(coord.x, 0, coord.y);
    }
    public static Vector2[] V2ArrayToCoord(Vector2 coord,Vector2[]layout)
    {
        Vector2[] temp = (Vector2[])layout.Clone();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += coord;
        }
        return temp;
    }
    public static float EvenlyCenteredValueAround0(int numOfItems, int i)
    {
        return ((numOfItems - 1) * -20) + (40 * i);
    }

}
