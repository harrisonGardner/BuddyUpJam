using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Vector2 moveInputs;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInputs = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {

        Vector3 moveVector = ((transform.forward * moveInputs.y) + (transform.right * moveInputs.x)).normalized;

        rb.AddForce(moveVector * moveSpeed * Time.deltaTime);
    }
}
