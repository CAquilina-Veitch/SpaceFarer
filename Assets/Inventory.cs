using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }

public struct generatingItem
{
    Item item;
    float rate;
}
public class Inventory: MonoBehaviour
{
    [SerializeField] Items items;


    public Dictionary<string, float> inventory = new Dictionary<string, float>();
    public int maxItemStackCapacity;


    public int currentInventoryVersion;


    float itemDelay;
    bool itemGenerating;


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
            TryChangeItems(item.name,maxItemStackCapacity);
        }
    }

    public bool hasEnoughOfResource(string itemID, int num)
    {
        return inventory[itemID] >= num;
    }

    public bool TryChangeItems(string itemID, int val)
    {
        if (inventory[itemID] >= -val)
        {
            inventory[itemID] += val;
            currentInventoryVersion++;
            return true;
        }
        else
        {
            return false;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    public void gFCall()
    {

    }

    IEnumerator GenerateItem()
    {
        


        yield return new WaitForSeconds(itemDelay);
        if (itemGenerating)
        {
            StartCoroutine(GenerateItem());
        }
    }

}
