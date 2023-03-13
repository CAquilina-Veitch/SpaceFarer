using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;


public enum tileShape { empty, single, nine, test}/*
public enum tileType { empty, ore, furnace, test1}*/
public enum mouseAction { blank, selecting, drafting, building }

public struct BuildingDraft
{
    public bool active;
    public Vector2 coordinate;
    public Building building;
};

public class TileManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GlobalFunctionality gF;
    [SerializeField] Buildings buildings;


    

    [Header("Camera Selection")]

    [SerializeField] LayerMask CameraGroundLayer;

    [SerializeField] Transform Camera;

    [Header("Tile Selection")]

    public mouseAction currentAction;

    public Tile currentlySelectedTile;


    [Header("Draft Tile")]

    BuildingDraft draft;

    [Header("Tiles Information")]

    public Dictionary<Vector2, Building> BuildingPositions = new Dictionary<Vector2, Building>();
    public Dictionary<Building, int> BuildingPopulations = new Dictionary<Building, int>();

    public Dictionary<Vector2, Tile> tileDictionary = new Dictionary<Vector2, Tile>();

    
    Vector2[] CoordinatePositionToVectorArray(Vector2 basePos, BuildingShape shape)
    {
        Vector2[] temp = shape.Layout;
        for (int i = 0; i < shape.Layout.Length; i++)
        {
            temp[i] = basePos + shape.Layout[i];
        }

        return temp;
    }
    bool checkShapeEmpty(Vector2 basePos, BuildingShape shape)
    {
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
        foreach (Vector2 pos in temp)
        {
            if (BuildingPositions.ContainsKey(pos))
            {
                return false;
            }

        }
        return true;
    }
    bool checkTileEmpty(Vector2 coordinate)
    {
        return BuildingPositions.ContainsKey(coordinate) ? false : true;
    }

    void SetTile(Vector2 basePos, BuildingShape shape, Building building)
    {

        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
        foreach (Vector2 pos in temp)
        {
            BuildingPositions[pos] = building;
        }
        BuildingPopulations[building] += 1;
    }
    void RemoveTile(Vector2 coord)
    {
        Tile deletingTile = TileAtCoord(coord);
        foreach (Vector2 Coordinate in CoordinatePositionToVectorArray(coord, buildings.GetBuildingShapeFromID(deletingTile.name)))
        {
            BuildingPositions.Remove(Coordinate);
        }
        BuildingPopulations[deletingTile.building] -= 1;
        Destroy(deletingTile);
        
    }

    Building BuildingAtCoord(Vector2 coord)
    {
        return BuildingPositions[coord];
    }
    Tile TileAtCoord(Vector2 coord)
    {
        return tileDictionary[coord];
    }



    void FixedUpdate()
    {
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, CameraGroundLayer))
            {

                Vector3 temp = posToCoord(hit.point);
                Debug.Log(temp);
            }
    }

    private void Update()
    {
        /// ALL TEMPORARY TESTING STUFF :DD:D::D:D::D::D::D:D::D::D:D:D::D::D::D:D:D:D:D
        /// 

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClickedOnCoord(CurrentMouseCoord());
        }



        /*if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            selectedTileType = (tileType)0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedTileType = (tileType)1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedTileType = (tileType)2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedTileType = (tileType)3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedTileType = (tileType)4;
        }*/









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


    void TryPlaceBuilding(Vector2 coord, Building build)
    {
        if (checkShapeEmpty(coord, buildings.GetBuildingShapeFromID(build.tileShapeID)))
        {
            Tile tile = MakeTile(coord, build);
            SetTile(coord, buildings.GetBuildingShapeFromID(build.tileShapeID), build); ;
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



    Tile MakeTile(Vector2 coord, Building build)
    {
        Tile tempTile = Instantiate(build.prefab, coordToPoint(coord), Quaternion.identity, transform).GetComponent<Tile>();
        tempTile.coordinate = coord;
        tempTile.building = build;
        return tempTile;
    }
    void replaceTile(Vector2 coord)
    {

    }

    public void ConfirmPlaceTile()
    {
        if (draft.building.powerRequirement > gF.PowerLevel)
        {
            Debug.LogError("Not enough power to place building");
            return;
        }
        else if(draft.active)
        {
            TryPlaceBuilding(draft.coordinate, draft.building);
            ClearDraft();
        }
    }
    void ClearDraft()
    {
        draft.active = false;
        draft.coordinate = Vector2.zero;
        draft.building = buildings.GetBuildingFromID("Empty");
    }

    void DraftCurrent()
    {
        Debug.Log("Drafted");
        draft.coordinate = CurrentMouseCoord();
        draft.active = true;
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


    void InteractCurrent()
    {
        currentlySelectedTile.InteractionWindow(false);
        currentlySelectedTile = TileAtCoord(CurrentMouseCoord());
        currentlySelectedTile.InteractionWindow(true);
    }
    void ClickedOnCoord(Vector2 Coordinate)
    {
        // when you click a tile
        if (checkTileEmpty(Coordinate))
        {
            //if the tile has nothing in it
            if (checkDraftAtCoord(Coordinate))
            {
                // if there is a draft
            }
            else
            {

            }
        }
        else
        {
            //if the tile has something in it
        }
    }

    bool checkDraftAtCoord(Vector2 coord)
    {
        bool temp = false;

        foreach(Vector2 position in CoordinatePositionToVectorArray(coord, buildings.GetBuildingShapeFromID(draft.building.name)))
        {
            temp = position == draft.coordinate ? true : false;
        }
        return temp;
        
    }
}
