using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum resourceState { locked, notEnough, enough }

public class ResourceAvailability : MonoBehaviour
{
    [SerializeField]Inventory inv;
    int currentInventoryVersion;

    public Item item;
    public int invAmount;
    resourceState state = new resourceState();
    public Color[] colours;
    public Image overlay;
    public GameObject lockIcon;
    public TextMeshProUGUI textBox;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void FixedUpdate()
    {
        if (inv.currentInventoryVersion != currentInventoryVersion)
        {
            invAmount = inv.inventory[item.type];
            currentInventoryVersion = inv.currentInventoryVersion;
            if (invAmount >= item.num)
            {
                SwitchState(resourceState.enough);
            }
            else if (invAmount > 0)
            {
                SwitchState(resourceState.notEnough);
            }
            else
            {
                SwitchState(resourceState.locked);
            }
            textBox.text = $"{invAmount}/{item.num}";
            
        }
        
    }
    void SwitchState(resourceState to)
    {
        if(state != to)
        {
            state = to;
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
