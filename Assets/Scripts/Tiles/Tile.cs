using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
    public Vector2 coordinate;
    public Building building;


    Inventory inv;



    private void OnEnable()
    {

        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        if (building.f_crafter.exists)
        {
            InitiateCrafting();
        }
        if (building.f_itemGen.exists)
        {
            InitiateGeneration();
        }



    }
    void InitiateCrafting()
    {
        inv.AddCraftingOperation(building.f_crafter.recipe);
    }

    void InitiateGeneration()
    {
        inv.ChangeItemGeneration(building.f_itemGen.item);
    }



    public void InteractionWindow()
    {

    }


}
