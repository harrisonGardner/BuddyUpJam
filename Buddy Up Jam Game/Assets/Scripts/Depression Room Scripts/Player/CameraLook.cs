using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private GameObject cam;
    public float camHeight = 0.55f;
    public float lookSensitivity = 15f;
    private Vector2 rotation = Vector2.zero;


    private Vector2 lastFrameRotation = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseInputs = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));

        rotation += new Vector2(mouseInputs.x * lookSensitivity * Time.deltaTime, mouseInputs.y * lookSensitivity * Time.deltaTime);

        //clamp up-down rotation
        rotation.x = Mathf.Clamp(rotation.x, -70f, 70f);

        Vector2 rotationDelta = lastFrameRotation - rotation;
        

        //cam.transform.position = new Vector3(transform.position.x, transform.position.y + camHeight, transform.position.z);

        // Apply camera rotation
        // make sure the change in rotation isn't too great from the last frame,
        // there's an issue where Input.GetAxisRaw with the mouse deltas will return a massive number
        if (Mathf.Abs(rotationDelta.x) < 75f && Mathf.Abs(rotationDelta.y) < 75f)
        {
            //cam.transform.eulerAngles = new Vector3(rotation.x, rotation.y, 0);
            lastFrameRotation = rotation;
        }
        else
        {
            rotation = lastFrameRotation;
        }
        
    }

    private void FixedUpdate()
    {
        //Apply player rotation
        
    }
}
