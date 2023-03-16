using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuildingConstructionMenu : MonoBehaviour
{
    [SerializeField] Buildings bM;
    [SerializeField] TileManager tM;
    List<Building> constructables;


    public GameObject bgObj;
    public GameObject iconPrefab;
    public Transform scrollParent;

    public bool currentlyActive;

    public List<GameObject> Icons;
    public float scrollValue;
    float maxLength;
    float offset = -100;
    float howManyOnScreen = 6;
    float screenWidth = 800;
    
    float scrollAcceleration = 20;
    float maxScrollSpeed = 20;
    float maxScrollDistance;
    public float scrollingFor;
    public float currentScrollDirection;

    private void OnEnable()
    {
        initiate();
    }

    void initiate()
    {
        constructables = new List<Building>((Building[])bM.buildings.ToArray().Clone());
        for(int i = 0; i < constructables.Count; i++)
        {
            GameObject Icon = Instantiate(iconPrefab, scrollParent);
            Icon.GetComponent<RectTransform>().anchoredPosition = new Vector3(screenWidth * ((i+1 ) / (howManyOnScreen+1)), 15);
            Icon.GetComponent<Image>().sprite = constructables[i].icon;
            Icon.GetComponentInChildren<TextMeshProUGUI>().text = constructables[i].name;
            Icon.GetComponent<GUIButton>().id = i;
            Icons.Add(Icon);
            maxScrollDistance = i == constructables.Count - 1 ? screenWidth * ((i + 1) / (howManyOnScreen + 1)) : 0;
            maxScrollDistance = i == constructables.Count - 1 && i < howManyOnScreen ? 0 : maxScrollDistance;
        }
    }
    public void OptionClicked(int num)
    {
        tM.SetDraft(num);
    }

    public void scrollRight(bool v)
    {
        currentScrollDirection = v ? -1 : 0;
        scrollingFor = v ? scrollingFor : 0;
    }
    public void scrollLeft(bool v)
    {
        currentScrollDirection = v ? 1 : 0;
        scrollingFor = v ? scrollingFor : 0;
    }
    private void FixedUpdate()
    {
        if (currentlyActive)
        {
            if (currentScrollDirection != 0)
            {
                scrollingFor += Time.deltaTime*scrollAcceleration;
                scrollValue += Mathf.Clamp(scrollingFor, 0, maxScrollSpeed) * currentScrollDirection;
                scrollValue = Mathf.Clamp(scrollValue, -maxScrollDistance,0);
                scrollParent.transform.position = new Vector3(scrollValue, 0);


            }
        }
    }

    public void setActivity(bool to)
    {
        currentlyActive = to;
        bgObj.SetActive(currentlyActive);
    }


}
