using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


[Serializable]
public struct Building
{
    public string tileTypeID;
    public string tileShapeID;
    public float powerRequirement;

    public Item[] constructionResources;

    public bool hasCrafter;
    public Crafter crafterFunc;
    public bool hasPowerGenerator;
    public PowerGenerator powerGeneratorFunc;
    public bool hasItemStorage;
    public bool hasLauncher;
    public bool hasLaunchpad;


}
[Serializable]
public struct BuildingShape
{
    public string name;
    public Vector2[] Layout;
}

[Serializable]
public struct Crafter
{
    public Item[] input;
    public Item[] output;
}
public struct PowerGenerator
{

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
