using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalInventoryManipulation : MonoBehaviour
{
    [SerializeField] Inventory inv;

    [SerializeField] List<Item> AsteroidGens;
    Item currentGen = new Item { type = itemType.None, num = 0 };


    public void changeAsteroidItemGeneration(int opt)
    {
        if (currentGen.type != AsteroidGens[opt].type)
        {
            if (currentGen.type != itemType.None)
            {
                inv.ChangeItemGeneration(new Item { type = currentGen.type, num = -currentGen.num });
            }
            inv.ChangeItemGeneration(AsteroidGens[opt]);
        }
    }


}
