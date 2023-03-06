using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum tileShape { empty, single, nine, test}
public enum tileType { empty, ore, test0, test1}


public class TileManager : MonoBehaviour
{
    [SerializeField] Tile TilePrefab;

    public Dictionary<Vector2, Tile> tileDictionary = new Dictionary<Vector2, Tile>();
    public Dictionary<tileType, tileShape> tileTypeShapes = new Dictionary<tileType, tileShape> 
    { 
        {tileType.ore, tileShape.nine},
        {tileType.test0, tileShape.single},
        {tileType.test1, tileShape.test},
        {tileType.empty, tileShape.empty}
    };

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
            SetTile(tile.coordinate, tileTypeShapes[tile.type],tile);
            tileDictionary[tile.coordinate] = tile;
        }
    }


    Vector2[] CoordinatePositionToVectorArray(Vector2 basePos, tileShape shape)
    {
        Vector2[] temp = new Vector2[ExternalTilePositions[(int)shape].Length];
        for(int i = 0; i < ExternalTilePositions[(int)shape].Length; i++)
        {
            temp[i] = basePos + ExternalTilePositions[(int)shape][i];
        }
        
        return temp;
    }

    bool checkShapeEmpty(Vector2 basePos, tileShape shape)
    {
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
        foreach(Vector2 pos in temp)
        {
            tileDictionary.TryGetValue(pos, out Tile v);
            if (v != null)
            {
                return false;
            }
            
        }
        return true;
    }

    void SetTile(Vector2 basePos, tileShape shape, Tile tileObj)
    {
        
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
        foreach (Vector2 pos in temp)
        {
            tileDictionary[pos] = tileObj;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        Vector2[] a =CoordinatePositionToVectorArray(new Vector2(5, 2), tileShape.nine);
        Debug.Log("5, 2 becomes  " + string.Join("",
             new List<Vector2>(a)
             .ConvertAll(i => i.ToString())
             .ToArray()));

        Tile t = Instantiate(TilePrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
