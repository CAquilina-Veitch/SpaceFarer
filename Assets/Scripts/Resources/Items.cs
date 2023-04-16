using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;







public class Items : MonoBehaviour
{
    public List<Item> items;

    public Item GetItemFromID(string id)
    {

        return items.Find(x => x.name == id);
    }


}
