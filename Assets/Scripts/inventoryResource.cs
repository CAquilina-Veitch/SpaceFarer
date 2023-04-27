using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class inventoryResource : MonoBehaviour
{
    [SerializeField] Inventory inv;
    int currentInventoryVersion;

    public Item item;   


    public UnityEngine.UI.Image img;
    public TextMeshProUGUI name ,quantity;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        name.text = $"{item.type}";
    }
    private void OnEnable()
    {
        img.sprite = inv.GetItemFromID(item.type).icon;
    }
    private void FixedUpdate()
    {
        if (inv.currentInventoryVersion != currentInventoryVersion)
        {
            item.num = inv.inventory[item.type];
            currentInventoryVersion = inv.currentInventoryVersion;
            quantity.text = $"{item.num}";
            
        }
        
    }

}
