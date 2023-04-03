using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class UICollision : MonoBehaviour
{
    [SerializeField] UnityEvent Interact;
    [SerializeField] UnityEvent Leave;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider Player)
    {
        Interact.Invoke();
    }
        private void OnTriggerExit(Collider Player)
    {
        Leave.Invoke();
    }
}
