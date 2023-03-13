using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
    public Vector2 coordinate;
    public Building building;




    [Header("Functionality Dependencies")]
    //public Crafter crafter;
    public PowerGenerator powerGenerator;
    public ItemStorage itemStorage;
    public Launcher launcher;
    public Launchpad launchpad;

    //[Header("Functionality Unique")]

    private void OnEnable()
    {
        //TryGetComponent<Crafter>(out crafter);
        TryGetComponent<PowerGenerator>(out powerGenerator);
        TryGetComponent<ItemStorage>(out itemStorage);
        TryGetComponent<Launcher>(out launcher);
        TryGetComponent<Launchpad>(out launchpad);
    }

    public void InteractionWindow(bool openWindow)
    {
        //open correct gui
        /*if (crafter != null)
        {

        }*/
       /* if (powerGenerator != null)
        {

        }
        else if (itemStorage != null)
        {

        }
        else if (launcher != null)
        {

        }
        else if (launchpad != null)
        {

        }*/
    }
    


}
