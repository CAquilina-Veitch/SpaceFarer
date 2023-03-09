using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;


public enum tileShape { empty, single, nine, test}
public enum tileType { empty, ore, furnace, test1}
public enum mouseAction { blank, selecting, drafting, building }

public class TileManager : MonoBehaviour
{
    [Header("Camera Selection")]

    [SerializeField] LayerMask CameraGroundLayer;

    [SerializeField] Transform Camera;

    [Header("Tile Selection")]

    public mouseAction currentAction;

    public Tile currentlySelectedTile;
    public tileType selectedTileType;

    [Header("Draft Tile")]

    public bool drafted;
    public tileType draftType;
    public Vector2 draftCoord;
    [Header("Tiles Information")]

    //[SerializeField] Tile TilePrefab;
    [SerializeField] Tile[] tilePrefabs;

    public Dictionary<Vector2, Tile> tileDictionary = new Dictionary<Vector2, Tile>();
    public Dictionary<tileType, tileShape> tileTypeShapes = new Dictionary<tileType, tileShape>
    {
        {tileType.ore, tileShape.nine},
        {tileType.furnace, tileShape.single},
        {tileType.test1, tileShape.test},
        {tileType.empty, tileShape.empty}
    };
    public Vector2[][] ExternalTilePositions =
    {
        new[] {new Vector2(0,0)},
        new[] {new Vector2(0,0)},
        new[] {new Vector2(-1,-1),new Vector2(-1,0),new Vector2(-1,1),new Vector2(0,-1),new Vector2(0,0),new Vector2(0,1),new Vector2(1,-1),new Vector2(1,0),new Vector2(1,1)},
        new[] {new Vector2(-1,0),new Vector2(0,0),new Vector2(1,0)} 
    };

    [Header("Instantiate Information")]

    public Tile[] tilesOnStart;

    [Header("Ingame Information")]
    [SerializeField] GlobalFunctionality gF;





    void initiate()
    {
        foreach (Tile tile in tilesOnStart)
        {
            SetTile(tile.coordinate, tileTypeShapes[tile.type], tile);
            tileDictionary[tile.coordinate] = tile;
        }
    }


    Vector2[] CoordinatePositionToVectorArray(Vector2 basePos, tileShape shape)
    {
        Vector2[] temp = new Vector2[ExternalTilePositions[(int)shape].Length];
        for (int i = 0; i < ExternalTilePositions[(int)shape].Length; i++)
        {
            temp[i] = basePos + ExternalTilePositions[(int)shape][i];
        }

        return temp;
    }

    bool checkShapeEmpty(Vector2 basePos, tileShape shape)
    {
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
        foreach (Vector2 pos in temp)
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

    Tile TileAtCoord(Vector2 coord)
    {
        return tileDictionary[coord];
    }


    // Start is called before the first frame update
    void Start()
    {
        Vector2[] a = CoordinatePositionToVectorArray(new Vector2(5, 2), tileShape.nine);
        Debug.Log("5, 2 becomes  " + string.Join("",
             new List<Vector2>(a)
             .ConvertAll(i => i.ToString())
             .ToArray()));

        initiate();
        //Tile t = Instantiate(getTilePrefab(type));
    }

    void FixedUpdate()
    {
        /*if (currentAction = )
        {*/
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, CameraGroundLayer))
            {

                Vector3 temp = posToCoord(hit.point);
                Debug.Log(temp);
            }
        //}
    }

    private void Update()
    {
        if (currentAction == mouseAction.building)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DraftCurrent();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                ConfirmPlaceTile();
            }
        }
        if (currentAction == mouseAction.selecting)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                InteractCurrent();
            }
        }
    }


    void TryPlaceBuilding(Vector2 coord, tileType type)
    {
        


        if (checkShapeEmpty(coord, tileTypeShapes[type]))
        {
            Tile tile = MakeTile(coord, type);
            tile.type = type;
            tile.coordinate = coord;
            SetTile(coord, tileTypeShapes[type], tile); ;
        }
    }



    Vector2 posToCoord(Vector3 pos)
    {
        Vector2 temp = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.z));

        return temp;
    }

    Vector3 coordToPoint(Vector2 coord)
    {
        return new Vector3(coord.x, 0, coord.y);
    }



    Tile MakeTile(Vector2 coord, tileType type)
    {
        Tile tempTile = Instantiate(getTilePrefab(type), coordToPoint(coord), Quaternion.identity, transform);
        return tempTile;
    }
    void replaceTile(Vector2 coord)
    {

    }

    public void ConfirmPlaceTile()
    {
        Tile tileInfo = MakeTile(draftCoord, draftType);
        if (tileInfo.powerRequirement > gF.PowerLevel)
        {
            Debug.LogError("Not enough power to place building");
            Destroy(tileInfo);
            return;
        }
        Destroy(tileInfo);

        if (drafted)
        {
            TryPlaceBuilding(draftCoord, draftType);
            ClearDraft();
        }
    }
    void ClearDraft()
    {
        drafted = false;
        draftType = tileType.empty;
        draftCoord = Vector2.zero;
    }

    void DraftCurrent()
    {
        draftCoord = CurrentMouseCoord();
        draftType = selectedTileType;
        drafted = true;
    }
    Vector2 CurrentMouseCoord()
    {
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, CameraGroundLayer))
        {

            Vector3 temp = posToCoord(hit.point);

            return temp;
        }
        else { Debug.Log("NOTHING ON MOUSE"); return Vector2.zero; }
    }

    Tile getTilePrefab(tileType type)
    {
        return tilePrefabs[(int)type];
    }

    void InteractCurrent()
    {
        currentlySelectedTile.InteractionWindow(false);
        currentlySelectedTile = TileAtCoord(CurrentMouseCoord());
        currentlySelectedTile.InteractionWindow(true);
    }

}
