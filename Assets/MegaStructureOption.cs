using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MegaStructureOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item[] cost;
    UnityEvent buildEvent;
    bool hasFixed;
    public GameObject windowPrefab;
    GameObject GUIWindow;
    public GameObject resourceImagePrefab;
    Inventory inv;

    public string Description;

    private void Awake()
    {
        inv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        if (!hasFixed)
        {
            hasFixed = true;
            buildEvent = GetComponent<Button>().onClick;
            GetComponent<Button>().onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            GetComponent<Button>().onClick.AddListener(delegate { Clicked(); });
            GUIWindow = Instantiate(windowPrefab,transform.position+Vector3.up,Quaternion.identity,transform);


            GUIWindow.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            GUIWindow.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Description;

            for (int i = 0; i < cost.Length; i++)
            {
                GameObject resourceObj = Instantiate(resourceImagePrefab, GUIWindow.transform);
                resourceObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(GlobalFunctions.EvenlyCenteredValueAround0(cost.Length, i), 16.7f);
                resourceObj.GetComponent<Image>().sprite = inv.GetItemFromID(cost[i].type).icon;

                ResourceAvailability _rA = resourceObj.GetComponent<ResourceAvailability>();
                _rA.item = new Item();
                _rA.invAmount = new int();
                _rA.item = cost[i];
                _rA.Size(20);
            }
            SetHover(false);



        }
        
    }

    public void Clicked()
    {
        bool hasEnough = true ;
        foreach(Item it in cost)
        {
            if (hasEnough)
            {
                hasEnough = inv.hasEnoughOfResource(it.type, it.num);
            }
            else
            {
                break;
            }
        }
        if (hasEnough)
        {
            buildEvent.Invoke();
        }
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





}
