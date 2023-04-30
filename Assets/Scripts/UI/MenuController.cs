using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum MenuState { none, main, pause, settings, credits}

public class MenuController : MonoBehaviour
{
    public MenuState currentMenu = MenuState.none;
    public MenuState lastMenu = MenuState.none;
    public GameObject[] menus;



    public void PreviousState()
    {
        ChangeState(lastMenu);
    }

    public void ChangeState(MenuState ms)
    {
        //if (currentMenu == ms) { return; }
        lastMenu = currentMenu;
        currentMenu = ms;

        for(int i = 0; i< menus.Length; i++)
        {
            menus[i].SetActive(i +1 == (int)ms);
        }
    }
    public void ChangeStateFS(string toState)
    {
        ChangeState((MenuState)Enum.Parse(typeof(MenuState), toState));
    }
}
