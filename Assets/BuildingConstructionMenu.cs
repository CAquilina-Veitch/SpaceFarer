using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstructionMenu : MonoBehaviour
{
    [SerializeField] Buildings bM;
    public List<Building> constructables;


    public GameObject bgObj;
    public GameObject iconPrefab;

    bool currentlyActive;

    public List<GameObject> Icons;
    float scrollValue;
    float seperation;

    private void OnEnable()
    { 
        initiate();
    }

    void initiate()
    {
        constructables = new List<Building>((Building[])bM.buildings.ToArray().Clone());
        foreach(Building b in constructables)
        {
            GameObject icon = Instantiate(iconPrefab, bgObj.transform.position, Quaternion.identity, bgObj.transform);
        }
    }

    public void setActivity(bool to)
    {
        currentlyActive = to;
        bgObj.SetActive(currentlyActive);
    }


}
