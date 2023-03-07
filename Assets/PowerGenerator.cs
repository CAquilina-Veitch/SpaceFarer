using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Requirement { none, sun, water}


public class PowerGenerator : MonoBehaviour
{
    public float powerOutput;
    public Tile[] powerTo;
    public Requirement requirement;
    public bool requirementMet;

    [SerializeField] bool requiresPower = false;
    public bool activity;

    public void setActivity(bool to)
    {
        if (requiresPower)
        {
            activity = to;
        }

    }
    private void FixedUpdate()
    {
        switch (requirement)
        {
            case Requirement.none:
                break;
            case Requirement.sun:
                break;
            case Requirement.water:
                break;
            default:
                Debug.LogError(requirement);
                break;
        }
    }

    void requirementChange(bool toValue)
    {
        if(toValue == requirementMet)
        {
            return;
        }
        //has actually changed
        requirementMet = toValue;
        switch (requirement)
        {
            case Requirement.none:
                foreach (Tile tile in powerTo)
                {
                    tile.UpdatePowerValue(-powerOutput);
                }
                break;
            case Requirement.sun:
                break;
            case Requirement.water:
                break;
            default:
                Debug.LogError(requirement);
                break;
        }
    }






}
