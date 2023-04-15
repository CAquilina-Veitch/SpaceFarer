using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOption : MonoBehaviour
{
    public int optionId;
    public void chosen()
    {
        GameObject.FindGameObjectWithTag("ProbeManager").GetComponent<ProbeManager>().MissionChosen(optionId);
    }
}
