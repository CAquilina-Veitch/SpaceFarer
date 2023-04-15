using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeManager : MonoBehaviour
{
    public void MissionChosen(int id)
    {
        Debug.Log("GOING TO PLANET " + id);
    }
}
