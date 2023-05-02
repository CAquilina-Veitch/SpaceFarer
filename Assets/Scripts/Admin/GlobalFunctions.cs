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
        return ((numOfItems - 1) * -15) + (30 * i);
    }
    public static Vector3 asPoint(this Vector2 c)
    {
        return coordToPoint(c);
    }
    public static Vector2 asCoord(this Vector3 p)
    {
        return posToCoord(p);
    }
    public static Vector2[] ArrayMinMax(this Vector2[] arr)
    {
        Vector2[] b = {new Vector2(100,100),new Vector2(-100,-100) };
        foreach(Vector2 v in arr)
        {
            b[0].x = v.x < b[0].x ? v.x : b[0].x;
            b[1].x = v.x > b[1].x ? v.x : b[1].x;
            b[0].y = v.y < b[0].y ? v.y : b[0].y;
            b[1].y = v.y > b[1].y ? v.y : b[1].y;
        }
        return b;
    }

}
