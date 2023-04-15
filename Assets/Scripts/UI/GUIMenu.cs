using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMenu : MonoBehaviour
{
    public float size = 80;
    public RectTransform building;
    public RectTransform resources;
    [SerializeField] GUI gui;

    private void OnEnable()
    {
        Size();
    }


    public void Clicked(int id)
    {
        if(id == 0)
        {
            gui.SwitchMainGUITo(MainGUIType.construction);
        }
        else if (id == 1)
        {
            gui.SwitchMainGUITo(MainGUIType.inventory);
        }
    }

    public void Size()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size*2.5f);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size*1.3f);
        building.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        building.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        building.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        building.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        building.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        resources.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        resources.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        resources.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        resources.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }

}
