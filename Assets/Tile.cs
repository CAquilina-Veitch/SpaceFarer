using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tile : MonoBehaviour
{
    public Vector2 coordinate;
    public tileType type;
    public Vector2[] externalEffectors;


    [Header("Power")]
    [SerializeField] float powerInput;
    [SerializeField] float powerRequirement;
    bool currentlyActive;
    //public PowerGenerator[] powerGenerators;



    [Header("Functionality Dependencies")]
    public Crafter crafter;
    public PowerGenerator powerGenerator;
    public ItemStorage itemStorage;
    public Launcher launcher;
    public Launchpad launchpad;

    //[Header("Functionality Unique")]

    private void OnEnable()
    {
        TryGetComponent<Crafter>(out crafter);
        TryGetComponent<PowerGenerator>(out powerGenerator);
        TryGetComponent<ItemStorage>(out itemStorage);
        TryGetComponent<Launcher>(out launcher);
        TryGetComponent<Launchpad>(out launchpad);
    }

    public void InteractionWindow(bool openWindow)
    {
        //open correct gui
        if (crafter != null)
        {

        }
        else if (powerGenerator != null)
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

        }
    }
    public void UpdatePowerValue(float input)
    {
        powerInput += input;
        powerInput = powerInput < 0 ? 0 : powerInput;
        currentlyActive = powerInput > powerRequirement ? true : false;
        SetFunctionalityActivity();
    }
    void SetFunctionalityActivity()
    {
        if (crafter != null)
        {
            crafter.setActivity(currentlyActive);
        }
        else if (powerGenerator != null)
        {
            powerGenerator.setActivity(currentlyActive);
        }
        else if (itemStorage != null)
        {
            itemStorage.setActivity(currentlyActive);
        }
        else if (launcher != null)
        {
            launcher.setActivity(currentlyActive);
        }
        else if (launchpad != null)
        {
            launchpad.setActivity(currentlyActive);
        }
    }
    /*private void FixedUpdate()
    {
        SetFunctionalityActivity();
    }*/

}
