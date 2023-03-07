using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafter : MonoBehaviour
{
    [SerializeField] bool requiresPower = true;
    public bool activity;
    public float speed = 0.5f;
    public float process = 0;
    bool currentlyCooking; // if u want it to brighten up>?? ????? ? 

    public void setActivity(bool to)
    {
        if (requiresPower)
        {
            activity = to;
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

    private void FixedUpdate()
    {
        if (activity)
        {
            if (currentSlotValues[3] >= outputItemMax)
            {
                activity = false;
                return;
            }
            else
            {
                process += Time.deltaTime;
                if (process >= speed)
                {
                    TryCraftItem();
                }
            }
        }
    }


    void TryCraftItem()
    {
        bool canCraft = true;
        for(int i = 0; i<currentSlotValues.Length; i++)
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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
