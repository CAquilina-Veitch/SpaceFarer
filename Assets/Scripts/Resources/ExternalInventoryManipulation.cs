using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalInventoryManipulation : MonoBehaviour
{
    [SerializeField] Inventory inv;

    [SerializeField] List<Item> AsteroidGens;
    Item currentGen = new Item { type = itemType.None, num = 0 };

    private void Awake()
    {
        currentGen.type = itemType.None;
    }
    public void changeAsteroidItemGeneration(int opt)
    {
        Debug.Log(1);
        if (currentGen.type != AsteroidGens[opt].type)
        {
            Debug.Log(2);
            if (currentGen.type != itemType.None)
            {
                inv.ChangeItemGeneration(new Item { type = currentGen.type, num = -currentGen.num });
                Debug.Log("sdjao" + -currentGen.num);
            }
            inv.ChangeItemGeneration(AsteroidGens[opt]);
            Debug.Log(AsteroidGens[opt].type);
            currentGen = AsteroidGens[opt];
        }
    }


}
