using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fisrtPersoneCamera : MonoBehaviour
{
    public Transform player; // Drag the Player GameObject into this field in the Inspector
    public float sensitivity = 2.0f;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        player.Rotate(Vector3.up * mouseX); // Rotate the player horizontally
        transform.Rotate(Vector3.left * mouseY);

        //
    }
}
