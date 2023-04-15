using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum resourceState { locked, notEnough, enough }

public class ResourceAvailability : MonoBehaviour
{
    [SerializeField]Inventory inv;
    int currentInventoryVersion;

    public string[]items;
    public float[] invAmount;
    public int[] ratio;
    resourceState[] states = new resourceState[4];
    public Color[] colours;
    public Image overlay;
    public GameObject lockIcon;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        if (inv.currentInventoryVersion != currentInventoryVersion)
        {
            for(int i =0; i<items.Length; i++)
            {
                invAmount[i] = inv.inventory[items[i]];
                currentInventoryVersion = inv.currentInventoryVersion;
                if (invAmount[i] >= ratio[i])
                {
                    SwitchState(i,resourceState.enough);
                }
                else if (invAmount[i] > 0)
                {
                    SwitchState(i,resourceState.notEnough);
                }
                else
                {
                    SwitchState(i,resourceState.locked);
                }
            }
            
        }
        
    }
    void SwitchState(int i, resourceState to)
    {
        if(states[i] != to)
        {
            states[i] = to;
            lockIcon.SetActive(to == resourceState.locked);
            overlay.color = colours[(int)to];



        }
    }
    public void Size(int size)
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,size);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size);
        lockIcon.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,size);
        lockIcon.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,size);
        overlay.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
        overlay.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }

}
