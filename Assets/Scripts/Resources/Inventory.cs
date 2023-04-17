using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }

public enum itemType { None, Iron, Silicon, Glass }



[Serializable]
public struct Item
{
    public itemType type;
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


    public Dictionary<itemType, int> inventory = new Dictionary<itemType, int>();
    public Dictionary<string, float> crafting = new Dictionary<string, float>();
    public int maxItemStackCapacity;


    public int currentInventoryVersion;


    float itemDelay = 1;
    bool itemGenerating;

    Dictionary<itemType,int> Gens = new Dictionary<itemType, int>();


    List<CraftingRecipe> UnsustainableCrafts;







    public List<Item> items;

    public Item GetItemFromID(itemType type)
    {

        return items.Find(x => x.type == type);
    }







    // Start is called before the first frame update
    void Start()
    {
        foreach(Item item in items)
        {
            inventory.Add(item.type, 0);
            Debug.LogError($"Added item {item.type}");
        }

        foreach(Item item in items)
        {
            TryChangeItems(item.type,maxItemStackCapacity);
        }
    }

    public bool hasEnoughOfResource(itemType type, int num)
    {
        return inventory[type] >= num;
    }

    public bool TryChangeItems(itemType type, int val)
    {
        if (inventory[type] >= -val)
        {
            inventory[type] += val;
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
        
        foreach(itemType t in Gens.Keys)
        {
            inventory[t] += Gens[t];
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
    public void StopGeneration(itemType type)
    {
        if (Gens.ContainsKey(type))
        {
            Gens.Remove(type);
        }
    }

    public void ChangeItemGeneration(Item item)
    {
        if (!Gens.ContainsKey(item.type))
        {
            Gens.Add(item.type, 0);
        }
        Gens[item.type] += item.num;
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
            ChangeItemGeneration(new Item() { type = it.type, num = -it.num });
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
            if (!Gens.ContainsKey(it.type))
            {
                return false;
            }
            else
            {
                if(Gens[it.type] < it.num)
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
            if (inventory[ID.type] < ID.num)
            {
                return;
            }
        }
        foreach(Item ID in rec.inputItems)
        {
            inventory[ID.type] -= ID.num;
        }
        foreach(Item ID in rec.outputItems)
        {
            inventory[ID.type] += ID.num;
        }
    }
}
