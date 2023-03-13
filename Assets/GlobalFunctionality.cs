using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunctionality : MonoBehaviour
{
    //public List<Crafter> activeCrafters;
    public List<PowerGenerator> activePowerGenerators;
    public List<ItemStorage> activeItemStorage;
    public List<Launcher> activeLaunchers;
    public List<Launchpad> activeLaunchpads;

    float frequency = 0.5f;
    float progress;


    public float PowerLevel;

    private void FixedUpdate()
    {
        progress += Time.deltaTime;
        if (progress >= frequency)
        {
            NextInstance();
            progress -= frequency;
        }
    }
    void NextInstance()
    {
        /*foreach(Crafter c in activeCrafters)
        {
            c.gFCall();
        }*/
/*        foreach(PowerGenerator pG in activePowerGenerators)
        {
            pG.gFCall();
        }
        foreach(ItemStorage iS in activeItemStorage)
        {
            iS.gFCall();
        }
        foreach(Launcher lr in activeLaunchers)
        {
           lr.gFCall();
        }
        foreach(Launchpad lp in activeLaunchpads)
        {
            lp.gFCall();
        }*/
    }




}
