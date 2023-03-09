using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crafter : MonoBehaviour
{
    [SerializeField] bool requiresPower = true;
    public bool activity;
    bool currentlyCooking; // if u want it to brighten up>?? ????? ? 

    GlobalFunctionality gF;

    private void OnEnable()
    {
        gF = GameObject.FindGameObjectWithTag("TileManager").GetComponent<GlobalFunctionality>();
    }

    public void setActivity(bool to)
    {
        activity = to;
        if (activity)
        {
            gF.activeCrafters.Add(this);
        }
        else
        {
            gF.activeCrafters.Remove(this);
        }

    }

    [SerializeField] int outputItemMax;

    [Header("Requirements")]
    [SerializeField] int numInputs;
    [SerializeField] resource resource1 = resource.empty;
    [SerializeField] resource resource2 = resource.empty;
    [SerializeField] resource resource3 = resource.empty;
    [SerializeField] resource output = resource.empty;
    [SerializeField] int[] resourceSlotRequirements = new int[4];
    [SerializeField] int[] currentSlotValues = new int[4];




    void TryCraftItem()
    {

        bool canCraft = true;
        if (currentSlotValues[3] >= outputItemMax)
        {
            Debug.LogError("Output Full");
            canCraft = false;
        }
        for (int i = 0; i<currentSlotValues.Length; i++)
        {
            if(currentSlotValues[i]< resourceSlotRequirements[i])
            {
                canCraft = false;
            }

        }
        if (canCraft == true)
        {
            for (int i = 0; i < currentSlotValues.Length-1; i++)
            {
                currentSlotValues[i] -= resourceSlotRequirements[i];

            }
            currentSlotValues[3] += resourceSlotRequirements[3];
        }
        currentlyCooking = canCraft;
    }

    public void gFCall()
    {
        TryCraftItem();
    }

}
