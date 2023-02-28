using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private GameObject player;

    public bool locked = false;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player");

        xRotation = player.transform.GetChild(2).transform.eulerAngles.x;
        yRotation = player.transform.GetChild(2).transform.eulerAngles.y;
    }

    private void Update()
    {
        if (!locked)
        {
            //get mouse input
            float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //rotate cam and orientation
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    private void LateUpdate()
    {
        if(!locked)
            transform.position = player.transform.GetChild(2).transform.position;
    }
}
