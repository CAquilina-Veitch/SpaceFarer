using System;
using System.Collections.Generic;
using UnityEngine;

public enum MainGUIType {noGUI, GUIMenu, construction, megaStructures, inventory, planetInfo}
public enum AdditionalGUIType { temperature, objectives, times}




public class GUI : MonoBehaviour
{
    public MainGUIType currentMainGUIType;
    public GameObject[] MainGUIObjects;
    public GameObject[] AdditionalGUIObjects;

    public void SwitchMainGUIToString(string to)
    {
        SwitchMainGUITo((MainGUIType)Enum.Parse(typeof(MainGUIType), to));
    }
    public void SwitchMainGUITo(MainGUIType to)
    {
        if (to != currentMainGUIType)
        {
            for (int i = 0; i < Enum.GetNames(typeof(MainGUIType)).Length; i++)
            {
                if (MainGUIObjects.Length > i)
                {
                    if (MainGUIObjects[i] != null)
                    {
                        MainGUIObjects[i].SetActive(i == (int)to);
                    }
                }
                else
                {
                    Debug.LogError($"trying to access mainGUIobject {i}, but only {MainGUIObjects.Length} exist"); /////////////////// DELETE THIS ELSE WHEN WE fill all fields
                }
            }
        }
    }
    public void SetAdditionalGUIactivity(bool to, AdditionalGUIType type)
    {
        AdditionalGUIObjects[(int)type].SetActive(to);
    }
    public void HideAllAdditionalGUI()
    {
        foreach(GameObject obj in AdditionalGUIObjects)
        {
            obj.SetActive(false);
        }
    }
   
        



}
