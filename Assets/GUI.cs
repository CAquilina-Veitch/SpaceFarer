using System;
using System.Collections.Generic;
using UnityEngine;

public enum GUIType {blank, buildingConstruction, megaStructures, planetInfo, constructionMicro}

[Serializable]
public struct GUIWindow
{
    public GUIType type;
    public GameObject Obj;
    public bool active;
    public bool hoverable;
    public bool clickable;
    public GUIType hoverGUI;
}



public class GUI : MonoBehaviour
{
    
    public List<GUIWindow> windows;



    public void SetGUI(GUIWindow window, bool to)
    {
        if(window.active = to)
        {
            return;
        }
        window.active = to;
        window.Obj.SetActive(to);
    }
    public void ToggleGUI(GUIWindow window)
    {
        window.active = !window.active;
        window.Obj.SetActive(window.active);
    }

    public GUIWindow findGUIWindow(GUIType type)
    {
        return windows.Find(x => x.type == type);
    }

    private void FixedUpdate()
    {
        
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
