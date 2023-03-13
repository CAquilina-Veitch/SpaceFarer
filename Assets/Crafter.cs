using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crafter : MonoBehaviour
{
    [SerializeField] bool requiresPower = true;
    public bool activity;

    GlobalFunctionality gF;

    private void OnEnable()
    {
        gF = GameObject.FindGameObjectWithTag("TileManager").GetComponent<GlobalFunctionality>();
        currentSlotValues = new int[crafter.input.Length+crafter.output.Length];
    }


    [Header("Requirements")]
    public CrafterType crafter;
    [SerializeField] int[] currentSlotValues = new int[4];

    void TryCraftItem()
    {

        bool canCraft = true;
        if (currentSlotValues[crafter.input.Length] >= crafter.max[crafter.input.Length])
        {
            Debug.LogError("Output Full");
            canCraft = false;
        }
        for (int i = 0; i<crafter.input.Length; i++)
        {
            if(currentSlotValues[i]< crafter.ratio[i])
            {
                canCraft = false;
            }

        }
        if (canCraft == true)
        {
            for (int i = 0; i < crafter.input.Length; i++)
            {
                currentSlotValues[i] -= crafter.ratio[i];

            }
            currentSlotValues[crafter.input.Length] += crafter.ratio[crafter.input.Length];
        }
        //currentlyCooking = canCraft;
    }

    public void gFCall()
    {
        TryCraftItem();
    }

}
