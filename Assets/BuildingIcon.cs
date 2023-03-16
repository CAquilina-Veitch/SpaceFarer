using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingIcon : MonoBehaviour
{
    Building build;
    public GameObject resourceImagePrefab;
    Items items;
    int id;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI GUIName;
    [SerializeField] TextMeshProUGUI GUIDescription;
    [SerializeField] Image image;


    public void initiate(Building b, int _id)
    {
        id = _id;
        build = b;


        items = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Items>();


        Name.text = build.name;
        GUIName.text = build.name;
        image.sprite = build.icon;
        GUIDescription.text = build.description;

        for (int i = 0; i < build.constructionResourcesID.Length; i++)
        {
            GameObject resourceObj = Instantiate(resourceImagePrefab, transform);
            resourceObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(GlobalFunctions.EvenlyCenteredValueAround0(build.constructionResourcesID.Length, i), 63.84f);
            resourceObj.GetComponent<Image>().sprite = items.GetItemFromID(build.constructionResourcesID[i]).icon;
        }
    }

    public void Activate()
    {
        GetComponentInParent<BuildingConstructionMenu>().OptionClicked(id);
    }



}
