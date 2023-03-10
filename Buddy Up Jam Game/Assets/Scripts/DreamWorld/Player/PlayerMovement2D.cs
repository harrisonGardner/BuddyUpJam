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

    public GameObject platformController;

    private PlayerHealth health;

    public Animator anim;

    public AudioClip jumpSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<PlayerHealth>();
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
            anim.SetBool("IsMoving", false);
        }
        else
        {
            anim.SetBool("IsMoving", true);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0f, (horizontalInput == 1 ? 180f : 0f), 0f)), 720f * Time.deltaTime);
        }

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.05f, whatIsGround);

        Debug.DrawRay(transform.position + (Vector3.down * 0.05f), Vector3.down, Color.red, 0.5f, false);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.15f))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                if (rb.velocity.y < 0)
                {
                    if (hit.collider.gameObject.GetComponent<Patrol>().Kill())
                    {
                        Jump(jumpHeight);
                        doubleJumped = false;

                        health.ActivateInvincibility(0.2f);

                        platformController.GetComponent<PlatformController>().TryRevealNextPlatform();
                    }
                }
            }
        }

        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else if (!grounded)
        {
            if(coyoteTimer > 0)
                coyoteTimer -= Time.deltaTime;
        }

        if (!grounded && coyoteTimer <= 0)
        {
            anim.SetBool("Grounded", false);
            if(rb.velocity.y < 0)
                anim.SetBool("Falling", true);
        }
        else
        {
            anim.SetBool("Grounded", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("Falling", false);
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
        anim.SetBool("IsJumping", true);
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(jumpSound);
        //audioSource.pitch = 1;
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
