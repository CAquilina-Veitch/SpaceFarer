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

    
    Vector2[] CoordinatePositionToVectorArray(Vector2 basePos, BuildingShape shape)
    {
        Debug.LogWarning($"{basePos} basepos, shape layout");
        Vector2[] temp = (Vector2[])shape.Layout.Clone();
        Debug.LogWarning($"temp {temp[0]}, {temp[1]}");
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] += basePos;
        }
        Debug.LogWarning($"temp after {temp[0]}, {temp[1]}");
        return temp;
    }
    bool checkShapeEmpty(Vector2 basePos, BuildingShape shape)
    {
        Vector2[] temp = CoordinatePositionToVectorArray(basePos, shape);
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
        foreach (Vector2 Coordinate in CoordinatePositionToVectorArray(coord, buildings.GetBuildingShapeFromID(deletingTile.name)))
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
        }*/
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            draft.building = buildings.buildings[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            draft.building = buildings.buildings[1];
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




    Vector2 posToCoord(Vector3 pos)
    {
        Vector2 temp = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.z));

        return temp;
    }

    Vector3 coordToPoint(Vector2 coord)
    {
        return new Vector3(coord.x, 0, coord.y);
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
        Debug.LogError($"Clicked on {Coordinate}");
        Debug.LogWarning("when you click a tile");

        if (checkTileEmpty(Coordinate))
        {
            Debug.LogWarning("if the tile has nothing in it");
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
        Tile tempTile = Instantiate(build.prefab, coordToPoint(coord), Quaternion.identity, transform).GetComponent<Tile>();
        tempTile.coordinate = coord;
        tempTile.building = build;
        return tempTile;
    }
    void SetTile(Vector2 basePos, Tile tile, Building building)
    {

        Vector2[] temp = CoordinatePositionToVectorArray(basePos, buildings.GetBuildingShapeFromID(building.tileShapeID));
        foreach (Vector2 pos in temp)
        {
            tilePositions[pos] = tile;
        }
        buildings.activeBuilds = activeBuildingsOperator(buildings.activeBuilds, BuildingToActiveBuilds(building), 1);
    }
    ActiveBuildings activeBuildingsOperator(ActiveBuildings a,ActiveBuildings b,int m)
    {
        ActiveBuildings temp = a;

        temp.activeCrafters += m*b.activeCrafters;
        temp.activePowerGenerators += m*b.activePowerGenerators;
        temp.activeItemStorage += m*b.activeItemStorage;
        temp.activeLaunchers += m*b.activeLaunchers;
        temp.activeLaunchpads += m*b.activeLaunchpads;
        return temp;
    }
    ActiveBuildings BuildingToActiveBuilds(Building build)
    {
        ActiveBuildings temp = new ActiveBuildings { };
        temp.activeCrafters = build.hasCrafter ? 1 : 0;
        temp.activePowerGenerators = build.hasPowerGenerator ? 1 : 0;
        temp.activeItemStorage = build.hasItemStorage ? 1 : 0;
        temp.activeLaunchers = build.hasLauncher ? 1 : 0;
        temp.activeLaunchpads = build.hasLaunchpad ? 1 : 0;
        return temp;
}

    bool checkDraftAtCoord(Vector2 coord)
    {
        bool temp = false;
        Debug.LogError($"DRAFTCHECK AT {coord}");
        foreach(Vector2 position in CoordinatePositionToVectorArray(coord, buildings.GetBuildingShapeFromID(draft.building.tileShapeID)))
        {
            if(position == draft.coordinate)
            {
                temp = true;
            }
            Debug.LogError($"temp {temp}, position {position}, draft coordinate {draft.coordinate}");
        }
        return temp;
        
    }





    











}
