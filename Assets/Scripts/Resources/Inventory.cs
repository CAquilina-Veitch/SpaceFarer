using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }

[Serializable]
public struct Item
{
    public string name;
    public Sprite icon;
    public int num;
}
[Serializable]
public struct CraftingRecipe
{
    public Item[] inputItems;
    public Item[] outputItems;
}

public class Inventory: MonoBehaviour
{
    [SerializeField] Items items;


    public Dictionary<string, int> inventory = new Dictionary<string, int>();
    public Dictionary<string, float> crafting = new Dictionary<string, float>();
    public int maxItemStackCapacity;


    public int currentInventoryVersion;


    float itemDelay = 1;
    bool itemGenerating;

    Dictionary<string,int> Gens = new Dictionary<string, int>();


    List<CraftingRecipe> UnsustainableCrafts;


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


    IEnumerator GenerateItem()
    {
        
        foreach(string i in Gens.Keys)
        {;
            inventory[i] += Gens[i];
        }
        //unsustainable craft
        foreach(CraftingRecipe rec in UnsustainableCrafts)
        {
            if (CheckSustainability(rec))
            {
                AddSustainableCraft(rec);
            }
            else
            {
                TryCraft(rec);
            }
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

    public void ChangeItemGeneration(Item item)
    {
        if (!Gens.ContainsKey(item.name))
        {
            Gens.Add(item.name, 0);
        }
        Gens[item.name] += item.num;
    }
    //buildingShapes.First(shape => shape.name == id);

    public void AddCraftingOperation(CraftingRecipe recipe)
    {
        if (CheckSustainability(recipe))
        {
            AddSustainableCraft(recipe);
            
        }
        else
        {
            UnsustainableCrafts.Add(recipe);
        }
    }
    public void AddSustainableCraft(CraftingRecipe recipe)
    {
        foreach (Item it in recipe.inputItems)
        {
            ChangeItemGeneration(new Item() { name = it.name, num = -it.num });
        }
        foreach (Item it in recipe.outputItems)
        {
            ChangeItemGeneration(it);
        }
    }

    bool CheckSustainability(CraftingRecipe rec)
    {
        foreach (Item it in rec.inputItems)
        {
            if (!Gens.ContainsKey(it.name))
            {
                return false;
            }
            else
            {
                if(Gens[it.name] < it.num)
                {
                    return false;
                }
            }
        }
        return true;
    }
    void TryCraft(CraftingRecipe rec)
    {
        foreach(Item ID in rec.inputItems)
        {
            if (inventory[ID.name] < ID.num)
            {
                return;
            }
        }
        foreach(Item ID in rec.inputItems)
        {
            inventory[ID.name] -= ID.num;
        }
        foreach(Item ID in rec.outputItems)
        {
            inventory[ID.name] += ID.num;
        }
    }
}
