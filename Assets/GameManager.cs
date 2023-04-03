using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gameState { menu, ship, shipGUI, surface}

public class GameManager : MonoBehaviour
{
    public gameState state;

    public GameObject[] cameras;

    public GameObject pilot;



    public TileManager tM;

    public void ChangeState(gameState to)
    {
        if(state == to) { return; }
        state = to;
        switch (state)
        {
            case gameState.ship:
                pilot.SetActive(true);
                break;
            case gameState.shipGUI:
                pilot.SetActive(true);
                break;
            case gameState.surface:
                pilot.SetActive(false);
                break;
            default:
                break;

        }
    }
    public void ChangeStateTI(int a) {ChangeState((gameState)a);}

    private void Update()
    {

    }



}
