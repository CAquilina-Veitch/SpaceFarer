using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct Building
{
    public string name;
    public string tileShapeID;
    public float powerRequirement;

    public string[] constructionResourcesID;
    public int[] constructionRatio;

    public bool hasCrafter;
    public bool hasPowerGenerator;
    public bool hasItemStorage;
    public bool hasLauncher;
    public bool hasLaunchpad;

    public GameObject prefab;
}
[Serializable]
public struct ActiveBuildings
{
    public int activeCrafters;
    public int activePowerGenerators;
    public int activeItemStorage;
    public int activeLaunchers;
    public int activeLaunchpads;
}

[Serializable]
public struct BuildingShape
{
    public string name;
    public Vector2[] Layout;
}

[Serializable]
public struct CrafterType
{
    public Item[] input;
    public Item[] output;
    public int[] ratio;
    public int[] max;
}
public struct PowerGeneratorType
{
    public Requirement requirement;
    public bool requiresItems;
    public string requiredItemID;
    public int quantity;
}




public class Buildings : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    public List<BuildingShape> buildingShapes = new List<BuildingShape>();
    public ActiveBuildings activeBuilds;

    private void OnEnable()
    {
        foreach (BuildingShape bS in buildingShapes)
        {

        }
    }


    public BuildingShape GetBuildingShapeFromID(string id)
    {
        Debug.LogError($"id {id} being searched, through {buildingShapes[0].name}, {buildingShapes[1].name}, {buildingShapes[2].name}, {buildingShapes[3].name}");
        int index = buildingShapes.FindIndex(x => x.name == id);
        return buildingShapes.First(shape => shape.name == id);
        if (index >= 0)
        {
            // found!
            Debug.LogError("FOUND");
            return(buildingShapes[index]);
        }
        else
        {
            Debug.LogError("NOT FOUND");
            return buildingShapes[1];
        }

        return buildingShapes.Find(x => x.name == id);
    }
    public Building GetBuildingFromID(string id)
    {
        
        return buildings.Find(x => x.name == id);
    }



    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
