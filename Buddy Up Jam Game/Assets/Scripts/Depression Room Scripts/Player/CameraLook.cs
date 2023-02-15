using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float lookSensitivity = 15f;
    private GameObject cam;
    private Vector2 rotation = Vector2.zero;
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
        //TODO: Clamp rotation
        //TODO: Figure out why it's jittery when rotating
        rotation += new Vector2(-Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime);
        cam.transform.localEulerAngles = new Vector3(rotation.x, 0, 0);
        transform.eulerAngles = new Vector3(0, rotation.y, 0);
    }
}
