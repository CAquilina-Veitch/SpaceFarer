using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int id;
    public UnityEvent StartHover;
    public UnityEvent EndHover;
    private void OnMouseEnter()
    {
        StartHover.Invoke();
    }
    private void OnMouseExit()
    {
        EndHover.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartHover.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EndHover.Invoke();
    }


}
