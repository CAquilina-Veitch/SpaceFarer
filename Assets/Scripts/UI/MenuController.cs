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
/*
        switch (currentMenu)
        {


            case MenuState.none:
                foreach (GameObject obj in menus)
                {
                    obj.SetActive(false);
                }
                break;
            case MenuState.main:
                foreach (GameObject obj in menus)
                {
                    obj.SetActive(false);
                }
                break;
            case MenuState.pause:
                foreach (GameObject obj in menus)
                {
                    obj.SetActive(false);
                }
                break;
            case MenuState.settings:
                foreach (GameObject obj in menus)
                {
                    obj.SetActive(false);
                }
                menus[1].SetActive(true);
                break;
            default:
                break;

        }*/
    }
    public void ChangeStateFS(string toState)
    {
        ChangeState((MenuState)Enum.Parse(typeof(MenuState), toState));
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
