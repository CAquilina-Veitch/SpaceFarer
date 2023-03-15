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

[Serializable]
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
    [SerializeField] Inventory inv;

    

    [Header("Camera Selection")]

    [SerializeField] LayerMask CameraGroundLayer;

    [SerializeField] Transform Camera;

    [Header("Tile Selection")]

    public mouseAction currentAction;

    public Tile currentlySelectedTile;


    [Header("Draft Tile")]

    [SerializeField] BuildingDraft draft;

    [Header("Tiles Information")]

    //public Dictionary<Vector2, Building> BuildingPositions = new Dictionary<Vector2, Building>();
    public List<Building> activeBuildings;
    public Dictionary<Building, int> BuildingPopulations = new Dictionary<Building, int>();

    public Dictionary<Vector2, Tile> tilePositions = new Dictionary<Vector2, Tile>();

    
    Vector2[] CoordinatePositionToVectorArray(Vector2 coord, BuildingShape shape)
    {
        return GlobalFunctions.V2ArrayToCoord(coord, shape.Layout);
    }
    bool checkShapeEmpty(Vector2 coord, BuildingShape shape)
    {
        Vector2[] temp = GlobalFunctions.V2ArrayToCoord(coord, shape.Layout);
        foreach (Vector2 pos in temp)
        {
            if (tilePositions.ContainsKey(pos))
            {
                return false;
            }

        }
        return true;
    }
    bool checkTileEmpty(Vector2 coordinate)
    {
        return tilePositions.ContainsKey(coordinate) ? false : true;
    }

    void RemoveTile(Vector2 coord)
    {
        Tile deletingTile = TileAtCoord(coord);
        foreach (Vector2 Coordinate in GlobalFunctions.V2ArrayToCoord(coord, buildings.GetBuildingShapeFromID(deletingTile.name).Layout))
        {
            tilePositions.Remove(Coordinate);
        }
        BuildingPopulations[deletingTile.building] -= 1;
        Destroy(deletingTile);
        
    }

    Tile TileAtCoord(Vector2 coord)
    {
        return tilePositions[coord];
    }



    void FixedUpdate()
    {
            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, CameraGroundLayer))
            {

                Vector3 temp = GlobalFunctions.posToCoord(hit.point);
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
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            draft.building = buildings.buildings[0];
            UpdateDraftActivity();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            draft.building = buildings.buildings[1];
            UpdateDraftActivity();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.LogWarning(buildings.GetBuildingShapeFromID(draft.building.name).name);
        }




/*


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
        }*/
    }







    void replaceTile(Vector2 coord)
    {

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
        UpdateDraftActivity();
    }
    Vector2 CurrentMouseCoord()
    {
        Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, CameraGroundLayer))
        {

            Vector3 temp = GlobalFunctions.posToCoord(hit.point);

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
        Debug.LogError($"Clicked on {Coordinate}");
        Debug.LogWarning("when you click a tile");

        if (checkTileEmpty(Coordinate))
        {
            Debug.LogWarning("if the tile has nothing in it");
            UpdateDraftActivity();
            if (draft.active)
            {
                Debug.LogWarning("draft is currently active");
                if (checkDraftAtCoord(Coordinate))
                {
                    Debug.LogWarning("clicked on draft");

                    bool canMake = true;
                    for (int i = 0; i < draft.building.constructionResourcesID.Length; i++)
                    {
                        if (inv.inventory[draft.building.constructionResourcesID[i]] < draft.building.constructionRatio[i])
                        {
                            canMake = false;
                        }
                    }
                    if (canMake)
                    {
                        Debug.LogWarning("you have the materials to build");

                        ConfirmPlaceTile();


                    }
                    else
                    {
                        Debug.LogWarning("no materials");

                    }





                }
                else
                {
                    Debug.LogWarning("empty, switched position");
                    draft.coordinate = Coordinate;
                }
            }
            else
            {
                Debug.LogWarning("not drafting yet, doing now");
                draft.active = true;
                draft.coordinate = Coordinate;
            }
            
        }
        else
        {
            Debug.LogWarning($"if the tile has something in it that is of course a: {TileAtCoord(Coordinate)}");

        }
    }

    public void SetDraft(int num)
    {
        draft.building = buildings.buildings[num];
    }

    public void ConfirmPlaceTile()
    {
        if (draft.building.powerRequirement > gF.PowerLevel)
        {
            Debug.LogError("Not enough power to place building");
            return;
        }
        else if (draft.active)
        {
            TryPlaceBuilding(draft.coordinate, draft.building);
            ClearDraft();
        }
    }
    void TryPlaceBuilding(Vector2 coord, Building build)
    {
        if (checkShapeEmpty(coord, buildings.GetBuildingShapeFromID(build.tileShapeID)))
        {
            PlaceTile(build, coord);
        }
    }
    void PlaceTile(Building build, Vector2 coord)
    {
        Tile tile = MakeTile(coord, build);
        SetTile(coord, tile, build);
    }

    Tile MakeTile(Vector2 coord, Building build)
    {
        Tile tempTile = Instantiate(build.prefab, GlobalFunctions.coordToPoint(coord), Quaternion.identity, transform).GetComponent<Tile>();
        tempTile.coordinate = coord;
        tempTile.building = build;
        return tempTile;
    }
    void SetTile(Vector2 coord, Tile tile, Building building)
    {

        Vector2[] temp = GlobalFunctions.V2ArrayToCoord(coord, buildings.GetBuildingShapeFromID(building.tileShapeID).Layout);
        foreach (Vector2 pos in temp)
        {
            tilePositions[pos] = tile;
        }
        buildings.activeBuilds = GlobalFunctions.activeBuildingsOperator(buildings.activeBuilds, GlobalFunctions.BuildingToActiveBuilds(building), 1);
    }
    bool checkDraftAtCoord(Vector2 coord)
    {
        UpdateDraftActivity();
        if (!draft.active) { return false; }
        bool temp = false;
        Debug.LogError($"DRAFTCHECK AT {coord}");
        foreach(Vector2 position in GlobalFunctions.V2ArrayToCoord(coord, buildings.GetBuildingShapeFromID(draft.building.tileShapeID).Layout))
        {
            if(position == draft.coordinate)
            {
                temp = true;
            }
            Debug.LogError($"temp {temp}, position {position}, draft coordinate {draft.coordinate}");
        }
        return temp;
        
    }

    void UpdateDraftActivity()
    {
        if (draft.active && draft.building.name == "Empty")
        {
            draft.active = false;
        }
    }





    











}
