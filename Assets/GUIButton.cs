using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GUIButton : MonoBehaviour
{
    public int id;
    public void Activate()
    {
        GetComponentInParent<BuildingConstructionMenu>().OptionClicked(id);
    }
}
