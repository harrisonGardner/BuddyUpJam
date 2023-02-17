using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 20f;
    public float drag = 20f;

    private Rigidbody rb;
    

    private float horizontalInput;
    private float verticalInput;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    private bool jumpBuffer = false;
    public float jumpCooldown = 0.25f;
    private float jumpCooldownTimer = 0f;

    public float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHeight = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.05f, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, playerHeight + 0.05f);

        if (!jumpBuffer && grounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumpBuffer = true;
        }
        if (jumpBuffer && !grounded)
        {
            jumpBuffer = false;
            jumpCooldownTimer = 0;
        }
        else if (jumpBuffer && grounded)
        {
            jumpCooldownTimer += Time.deltaTime;
            if (jumpCooldownTimer >= jumpCooldown)
            {
                jumpCooldownTimer = 0;
                jumpBuffer = false;
            }
        }


        SpeedControl();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * (acceleration * horizontalInput), ForceMode.Force);
    }

    private void SpeedControl()
    {
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, rb.velocity.z);
    }

}
