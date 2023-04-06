using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { menu, ship, shipGUI, surface}

public class GameManager : MonoBehaviour
{
    public gameState state;
    public bool paused = false;
    public bool gameMenuActive = true;

    public GameObject surfacePlayer;
    public GameObject menuCamera;
    public GameObject shipPilot;


    public TileManager tM;
    public MenuController mC;

    gameState prePauseState;
    MenuState prePauseMenu;
    public GameObject playGUI;
    public GameObject gameGUI;
    bool prePauseGameMenuActive;

    public void GameMenu(bool to)
    {
        gameMenuActive = to;
        playGUI.SetActive(!gameMenuActive);
        gameGUI.SetActive(gameMenuActive);
    }

    public void ChangeState(gameState to)
    {
        if(state == to) { return; }
        state = to;
        switch (state)
        {
            case gameState.menu:
                shipPilot.SetActive(false);
                surfacePlayer.SetActive(false);
                menuCamera.SetActive(true);
                GameMenu(true);

                break;
            case gameState.ship:
                shipPilot.SetActive(true);
                surfacePlayer.SetActive(false);
                menuCamera.SetActive(false);


                mC.ChangeState(MenuState.none);
                GameMenu(false);
                break;
            case gameState.shipGUI:
                shipPilot.SetActive(true);
                surfacePlayer.SetActive(false);
                menuCamera.SetActive(false);
                GameMenu(false);
                break;
            case gameState.surface:
                shipPilot.SetActive(false);
                surfacePlayer.SetActive(true);
                menuCamera.SetActive(false);
                mC.ChangeState(MenuState.none);
                GameMenu(false);
                break;
            default:
                break;

        }
    }
    public void ChangeStateTF(string a) 
    {
        ChangeState((gameState)Enum.Parse(typeof(gameState), a));
    }

    private void OnEnable()
    {
        ChangeState(gameState.menu);
        mC.ChangeState(MenuState.main);
        GameMenu(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        
        paused = !paused;
        if (paused)
        {
            prePauseGameMenuActive = gameMenuActive;
            GameMenu(true);
            prePauseState = state;
            ChangeState(gameState.menu);
            prePauseMenu = mC.currentMenu;
            mC.ChangeState(MenuState.pause);

        }
        else
        {
            ChangeState(prePauseState);
            mC.ChangeState(prePauseMenu);
            GameMenu(prePauseGameMenuActive);
        }
        Time.timeScale = paused ? 0.0001f : 1;
    }
}
