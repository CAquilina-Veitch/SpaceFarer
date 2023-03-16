using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }
public class Inventory: MonoBehaviour
{
    [SerializeField] Items items;


    public Dictionary<string, float> inventory = new Dictionary<string, float>();
    public int maxItemStackCapacity;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Item item in items.items)
        {
            inventory.Add(item.name, 0);
            Debug.LogError($"Added item {item.name}");
        }

        foreach(Item item in items.items)
        {
            inventory[item.name] = maxItemStackCapacity;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void gFCall()
    {

    }
}
