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

    public Item[] constructionResources;

    public bool hasCrafter;
    public bool hasPowerGenerator;
    public bool hasItemStorage;
    public bool hasLauncher;
    public bool hasLaunchpad;

    public GameObject prefab;
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
    public Item requiredItem;
}




public class Buildings : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    public List<BuildingShape> buildingShapes = new List<BuildingShape>();
    private void OnEnable()
    {
        foreach (BuildingShape bS in buildingShapes)
        {

        }
    }


    public BuildingShape GetBuildingShapeFromID(string id)
    {
        
        return buildingShapes.Find(x => x.name == id);
    }
    public Building GetBuildingFromID(string id)
    {
        
        return buildings.Find(x => x.name == id);
    }



    // Start is called before the first frame update
    void Start()
    {

        buildings.Find(x => x.tileShapeID == "foobar");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
