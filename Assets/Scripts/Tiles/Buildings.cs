using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ItemGenerator
{
    public bool exists;
    public Item item;
}

[Serializable]
public struct Building
{
    public string name;
    public string tileShapeID;
    public float powerRequirement;
/*
    public string[] constructionResourcesID;
    public int[] constructionRatio;
*/
    public Item[] constructionItems;

    public Sprite icon;
    public string description;

    public ItemGenerator f_itemGen;
    public Crafting f_crafter;

    public GameObject prefab;
    public bool canBeClickedFromStart;

    public UnityEvent instantiationAction;
}


[Serializable]
public struct BuildingShape
{
    public string name;
    public Vector2[] Layout;
}

[Serializable]
public struct Crafting
{
    public bool exists;
    public CraftingRecipe recipe;
}
public struct PowerGeneratorType
{
    public bool requiresItems;
    public string requiredItemID;
    public int quantity;
}


public class Buildings : MonoBehaviour
{
    public List<Building> buildings = new List<Building>();
    public List<BuildingShape> buildingShapes = new List<BuildingShape>();

    public BuildingShape GetBuildingShapeFromID(string id)
    {
        Debug.LogError($"id {id} being searched, through {buildingShapes[0].name}, {buildingShapes[1].name}, {buildingShapes[2].name}, {buildingShapes[3].name}");
        //int index = buildingShapes.FindIndex(x => x.name == id);
        return buildingShapes.First(shape => shape.name == id);
    }
    public Building GetBuildingFromID(string id)
    {
        return buildings.Find(x => x.name == id);
    }

}
