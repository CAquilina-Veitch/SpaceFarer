using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum resource { empty, iron, gold, redstone }
public class ItemStorage : MonoBehaviour
{
    
    public Dictionary<resource, float> inventory = new Dictionary<resource, float>();
    public int maxItemStackCapacity;

    [SerializeField] bool requiresPower = false;
    public bool activity;

    public void setActivity(bool to)
    {
        if (requiresPower)
        {
            activity = to;
        }
        
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
