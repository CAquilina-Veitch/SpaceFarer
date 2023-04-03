using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Interact : MonoBehaviour
{
    [SerializeField] UnityEvent InteractWith;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            InteractWith.Invoke();
        }
    }
}