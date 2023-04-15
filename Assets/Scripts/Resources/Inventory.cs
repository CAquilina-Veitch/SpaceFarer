using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }


public class Inventory: MonoBehaviour
{
    [SerializeField] Items items;


    public Dictionary<string, float> inventory = new Dictionary<string, float>();
    public int maxItemStackCapacity;


    public int currentInventoryVersion;


    float itemDelay = 1;
    bool itemGenerating;

    Dictionary<string,int> Gens = new Dictionary<string, int>();


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
        
        foreach(string i in Gens.Keys)
        {;
            inventory[i] += Gens[i];
        }

        currentInventoryVersion++;
        yield return new WaitForSeconds(itemDelay);
        if (itemGenerating)
        {
            StartCoroutine(GenerateItem());
        }
    }
    public void StopGeneration(string item)
    {
        if (Gens.ContainsKey(item))
        {
            Gens.Remove(item);
        }
    }

    public void ChangeItemGeneration(Item item, int rate)
    {
        if (!Gens.ContainsKey(item.name))
        {
            Gens.Add(item.name, 0);
        }
        Gens[item.name] += rate;
    }
    //buildingShapes.First(shape => shape.name == id);




}
