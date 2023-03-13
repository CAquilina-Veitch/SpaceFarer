using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Requirement { none, sun, water }


public class PowerGenerator : MonoBehaviour
{
    public float powerOutput;

    public Requirement requirement;
    public bool requirementMet;

    [SerializeField] bool requiresPower = false;
    public bool activity;

    public void Built()
    {
        GameObject.FindGameObjectWithTag("TileManager").GetComponent<GlobalFunctionality>().PowerLevel += powerOutput;
    }

    public void gFCall()
    {

    }


}
