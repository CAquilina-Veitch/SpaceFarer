using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Building build;
    public GameObject resourceImagePrefab;
    Inventory inv;
    int id;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI GUIName;
    [SerializeField] TextMeshProUGUI GUIDescription;
    [SerializeField] Image image;
    [SerializeField] GameObject GUIWindow;
    [SerializeField] GameObject CantUseOverlay;


    public void initiate(Building b, int _id)
    {
        id = _id;
        build = b;

        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

        CanBeClicked(build.canBeClickedFromStart);
        Name.text = build.name;
        GUIName.text = build.name;
        image.sprite = build.icon;
        GUIDescription.text = build.description;

        for (int i = 0; i < build.constructionItems.Length; i++)
        {
            GameObject resourceObj = Instantiate(resourceImagePrefab, GUIWindow.transform);
            resourceObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(GlobalFunctions.EvenlyCenteredValueAround0(build.constructionItems.Length, i), 16.7f);
            resourceObj.GetComponent<Image>().sprite = inv.GetItemFromID(build.constructionItems[i].type).icon;

            ResourceAvailability _rA = resourceObj.GetComponent<ResourceAvailability>();
            _rA.item = new Item();
            _rA.invAmount = new int();
            _rA.item = build.constructionItems[i];
            _rA.Size(20);
        }
        SetHover(false);
    }

    public void Activate()
    {
        GetComponentInParent<BuildingConstructionMenu>().OptionClicked(id);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetHover(false);
    }
    void SetHover(bool val)
    {
        GUIWindow.SetActive(val);
    }
    public void CanBeClicked(bool to)
    {
        CantUseOverlay.SetActive(!to);
        GetComponent<Button>().interactable = to;
    }

}
