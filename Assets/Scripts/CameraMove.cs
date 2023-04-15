using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float scrollSpeed = 0.05f;
    public float scrollAmount = 0f;
    Vector3 basePosition;
    Vector3 offset;
    Vector2 bounds = new Vector2(10, 10);
    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.UpArrow) == true || Input.GetKey(KeyCode.W) == true) { basePosition += Vector3.forward * moveSpeed * Time.deltaTime; }
         if (Input.GetKey(KeyCode.DownArrow) == true || Input.GetKey(KeyCode.S) == true) { basePosition -= Vector3.forward * moveSpeed * Time.deltaTime; }


         if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) { basePosition += Vector3.right * moveSpeed * Time.deltaTime; }
         if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) { basePosition -= Vector3.right * moveSpeed * Time.deltaTime; }

        transform.Translate(Input.mouseScrollDelta.y * scrollSpeed * transform.up);
        if (Input.mouseScrollDelta.y != 0)
        {
            scrollAmount = Mathf.Clamp(scrollAmount - Input.mouseScrollDelta.y * scrollSpeed, 0, 1);
            
        }
        offset = transform.forward * -Mathf.Lerp(3, 10, scrollAmount);
        basePosition = new Vector3(Mathf.Clamp(basePosition.x, -bounds.x * (1-scrollAmount), bounds.x * (1 - scrollAmount)), 0, Mathf.Clamp(basePosition.z, -bounds.y * (1 - scrollAmount), bounds.y * (1 - scrollAmount)));
        transform.position = basePosition + offset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7, 4), transform.position.y, Mathf.Clamp(transform.position.z, -5, 3));
    }
}
