using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Item
{
    public string name;
    public Texture2D icon;
}





public class Items : MonoBehaviour
{
    public List<Item> items;
}
