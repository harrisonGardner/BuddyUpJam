using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 20f;

    private Rigidbody rb;


    private float horizontalInput;
    private float verticalInput;

    [Header("Ground Check")]
    private float playerHeight;
    public LayerMask whatIsGround;
    private bool grounded;

    public float coyoteTime = 0.1f;
    private float coyoteTimer = 0f;

    private bool jumpBuffer = false;
    public float jumpCooldown = 0.25f;
    private float jumpCooldownTimer = 0f;
    private bool jumpApexReached = false;

    public float jumpHeight = 5f;

    public bool doubleJumpEnabled = true;
    private bool doubleJumped = false;
    
    [Tooltip("How powerful the double jump will be relative to the normal jump power")]
    public float doubleJumpMultiplier = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHeight = gameObject.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GetComponent<PlayerHealth>().Damage(100);
        }

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight + 0.05f, whatIsGround);

        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else if (!grounded)
        {
            if(coyoteTimer > 0)
                coyoteTimer -= Time.deltaTime;
        }

        if ((!jumpBuffer && coyoteTimer > 0 && Input.GetKey(KeyCode.Space)))
        {
            Jump(jumpHeight);
        }
        else if (doubleJumpEnabled && !doubleJumped && coyoteTimer <= 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(jumpHeight * doubleJumpMultiplier);
            doubleJumped = true;
        }

        if (grounded)
            doubleJumped = false;

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

        if (coyoteTimer > 0)
            jumpApexReached = false;

        if (!jumpApexReached && coyoteTimer <= 0 && !grounded && rb.velocity.y < 0)
        {
            jumpApexReached = true;
        }
        else if (!jumpApexReached && !grounded && Input.GetKeyUp(KeyCode.Space))
        {
            jumpApexReached = true;
            rb.AddForce(Vector3.down * Mathf.Abs(rb.velocity.y * 2), ForceMode.Impulse);
        }

        SpeedControl();
    }

    public void Jump(float power)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * power, ForceMode.Impulse);
        jumpBuffer = true;
        coyoteTimer = 0;
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * (acceleration * horizontalInput), ForceMode.Force);
    }

    private void SpeedControl()
    {
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, rb.velocity.z);
    }

    public void FreezePlayer()
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
