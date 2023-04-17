using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;



public enum tileShape { empty, single, nine, test}

[Serializable]
public struct BuildingDraft
{
    public bool active;
    public Building building;
};

public class TileManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] GlobalFunctionality gF;
    [SerializeField] Buildings buildings;
    [SerializeField] Inventory inv;
    [SerializeField] GameManager gM;

    

    [Header("Camera Selection")]

    [SerializeField] LayerMask CameraGroundLayer;

    [SerializeField] Transform Camera;

    [Header("Tile Selection")]
    Vector2 recentTileChecked;


    [Header("Draft Tile")]

    [SerializeField] BuildingDraft draft;
    [SerializeField] GameObject draftVisual;
    [SerializeField] MeshFilter draftMesh;
    [SerializeField] MeshRenderer draftMeshRenderer;
    [SerializeField] Material[] draftMats;


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
        draftVisual.SetActive(draft.active);
        if (draft.active)
        {
            draftMesh.mesh = draft.building.prefab.GetComponent<MeshFilter>().sharedMesh;
            draftVisual.transform.position = GlobalFunctions.coordToPoint(CurrentMouseCoord());
            draftVisual.transform.localScale = draft.building.prefab.transform.localScale;
            if (recentTileChecked != CurrentMouseCoord())
            {
                draftMeshRenderer.material = checkShapeEmpty(CurrentMouseCoord(), buildings.GetBuildingShapeFromID(draft.building.tileShapeID)) ? draftMats[0] : draftMats[1];
                recentTileChecked = CurrentMouseCoord();
            }
            
        }
            
    }

    private void Update()
    {
        if (gM.state != gameState.surface)
        {
            return;
        }





        if (Input.GetKeyDown(KeyCode.Mouse0)&&!IsMouseOverUIWithIgnores())
        {
            ClickedOnCoord(CurrentMouseCoord());
        }








    }
    private bool IsMouseOverUIWithIgnores()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for(int i = 0; i < raycastResultList.Count; i++)
        {
            if(raycastResultList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }

    void ClearDraft()
    {
        draft.active = false;
        draft.building = new Building();
        //draft.building = buildings.GetBuildingFromID("Empty");
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
        Tile currentlySelectedTile = TileAtCoord(CurrentMouseCoord());
        currentlySelectedTile.InteractionWindow();
    }
    void ClickedOnCoord(Vector2 Coordinate)
    {
        Debug.LogError($"Clicked on {Coordinate}");
        Debug.LogWarning("when you click a tile");

        UpdateDraftActivity();
        if (draft.active)//if building type is selected.
        {
            Debug.Log("draft is active");
            if (checkShapeEmpty(Coordinate, buildings.GetBuildingShapeFromID(draft.building.tileShapeID)))//if there is no building overlapping the current place.
            {
                if (HasDraftResources()) // there are materials to build
                {
                    if (HasDraftPower())//if there is enough power
                    {
                        TryPlaceBuilding(Coordinate, draft.building);
                        ClearDraft();
                    }
                    else
                    {
                        //not enough power
                    }



                }
                else
                {
                    //not enough resources
                }


            }
            else
            {
                //there is a building in here
            }


        }
        else
        {
            //Hasnt selected a building yet
            if (!checkTileEmpty(Coordinate))//there is something to look at 
            {
                Debug.LogError($"YOU HAVE NOW CLICKED UPPON THE TILE {TileAtCoord(Coordinate)}");
                InteractCurrent();
            }



        }
    }

    bool HasDraftPower()
    {
        return draft.building.powerRequirement <= gF.PowerLevel;
    }

    bool HasDraftResources()
    {
        bool canMake = true;
        for (int i = 0; i < draft.building.constructionResourcesID.Length; i++)
        {
            if (inv.inventory[draft.building.constructionResourcesID[i]] < draft.building.constructionRatio[i])
            {
                canMake = false;
            }
        }
        return canMake;
    }



    public void SetDraft(int num)
    {
        draft.building = buildings.buildings[num];
        draft.active = true;
    }

    void TryPlaceBuilding(Vector2 coord, Building build)
    {
        if (checkShapeEmpty(coord, buildings.GetBuildingShapeFromID(build.tileShapeID)))
        {
            PlaceTile(build, coord);
            build.instantiationAction.Invoke();
            Debug.Log(" PLACED");
        }
        else
        {
            Debug.Log("FAILED PLACE");
        }
    }
    void PlaceTile(Building build, Vector2 coord)
    {
        Tile tile = MakeTile(coord, build);
        SetTile(coord, tile, build);
    }

    Tile MakeTile(Vector2 coord, Building build)
    {
        Debug.Log($"Making tile {build}, {build.name}, at {coord}");
        GameObject tempObj = Instantiate(build.prefab, GlobalFunctions.coordToPoint(coord), Quaternion.identity, transform);
        Tile tempTile = tempObj.GetComponent<Tile>();
        Debug.Log($"{tempObj.name}");
        tempTile.coordinate = coord;
        tempTile.building = build;
        ClearDraft();
        return tempTile;
    }
    void SetTile(Vector2 coord, Tile tile, Building building)
    {

        Vector2[] temp = GlobalFunctions.V2ArrayToCoord(coord, buildings.GetBuildingShapeFromID(building.tileShapeID).Layout);
        foreach (Vector2 pos in temp)
        {
            tilePositions[pos] = tile;
        }
    }

    void UpdateDraftActivity()
    {
        draft.active = false;
        if(draft.building.name!="Empty"&&draft.building.name!= null)
        {
            Debug.Log($"setting to true, is called {draft.building.name}");
            draft.active = true;
        }
        
    }

    private void OnEnable()
    {
        ClearDraft();
    }















}
