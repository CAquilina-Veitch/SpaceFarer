using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PilotController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] Vector2 mouseInput;
    [SerializeField] float sensitivity = 1;
    float pitch, yaw;

    [SerializeField] Vector2 moveInput;
    [SerializeField] float speed = 3;
    [SerializeField] float acceleration = 10;
    [SerializeField] Rigidbody rb;


    public void Toggle(bool to)
    {
        Cursor.lockState = to ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !to;
    }

    private void OnEnable()
    {
        Toggle(true);
        yaw = transform.eulerAngles.y;
    }
    private void OnDisable()
    {
        Toggle(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }




    void Update()
    {
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y")) * sensitivity;
        pitch += mouseInput.y;
        yaw += mouseInput.x;
        pitch = Mathf.Clamp(pitch, -90, 90);
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        cam.localRotation = Quaternion.Euler(pitch, 0, 0);

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        rb.velocity = Vector3.Lerp( rb.velocity,transform.forward * moveInput.y + transform.right * moveInput.x,Time.deltaTime*acceleration);
        //Debug.Log(Time.deltaTime * acceleration);




    }
}
