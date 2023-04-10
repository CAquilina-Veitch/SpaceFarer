using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Objectives : MonoBehaviour
{
    [SerializeField] UnityEvent ObjectiveComplete;
    void Awake()
    {
        ObjectiveComplete.Invoke();
    }
}
