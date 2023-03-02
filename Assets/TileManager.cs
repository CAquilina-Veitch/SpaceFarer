using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum tileType { empty, single, nine, test}


public class TileManager : MonoBehaviour
{
    public Dictionary<Vector2, Tile> tileDictionary = new Dictionary<Vector2, Tile>();

    public Tile[] tilesOnStart;


    public Vector2[][] ExternalTilePositions =
    {
        new[] {new Vector2(0,0)},
        new[] {new Vector2(0,0)},
        new[] {new Vector2(-1,-1),new Vector2(-1,0),new Vector2(-1,1),new Vector2(0,-1),new Vector2(0,0),new Vector2(0,1),new Vector2(1,-1),new Vector2(1,0),new Vector2(1,1)},
        new[] {new Vector2(-1,0),new Vector2(0,0),new Vector2(1,0)}
    };


    void initiate()
    {
        foreach(Tile tile in tilesOnStart)
        {
            tileDictionary[tile.coordinate] = tile;
        }
    }


    Vector2[] CoordinatePositionToVectorArray(Vector2 basePos, tileType type)
    {
        Vector2[] temp = new Vector2[ExternalTilePositions[(int)type].Length];
        for(int i = 0; i < ExternalTilePositions[(int)type].Length; i++)
        {
            temp[i] = basePos + ExternalTilePositions[(int)type][i];
        }
        
        return temp;
    }

    bool checkEmpty(Vector2 basePos, tileType type)
    {
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, type);
        foreach(Vector2 pos in temp)
        {
            tileDictionary.TryGetValue(pos, out Tile v);
        }
    }

    void SetTile(Vector2 basePos,tileType type)
    {

    }



    // Start is called before the first frame update
    void Start()
    {
        Vector2[] a =CoordinatePositionToVectorArray(new Vector2(5, 2), tileType.nine);
        Debug.Log("5, 2 becomes  " + string.Join("",
             new List<Vector2>(a)
             .ConvertAll(i => i.ToString())
             .ToArray()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
