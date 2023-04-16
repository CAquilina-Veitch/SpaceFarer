using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGenerator : MonoBehaviour
{
    public float powerOutput;

    [SerializeField] bool requiresPower = false;

    public void Built()
    {
        GameObject.FindGameObjectWithTag("TileManager").GetComponent<GlobalFunctionality>().PowerLevel += powerOutput;
    }

}
