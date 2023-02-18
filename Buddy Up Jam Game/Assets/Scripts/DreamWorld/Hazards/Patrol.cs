using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;

    public float viewDistance;
    public float wallDetectDistance = 0.25f;

    public bool rightStartDirection = false;
    private bool chasing = false;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.right *= (rightStartDirection? -1 : 1); 
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();

        Ray playerRay = new Ray(transform.position, transform.right);
        RaycastHit hit;
        if (Physics.Raycast(playerRay, out hit, viewDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                chasing = true;
            }
            else if (Vector3.Distance(hit.point, transform.position) < transform.localScale.x + wallDetectDistance)
            {
                transform.right *= -1;
                chasing = false;
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(100f * transform.right);
    }

    private void SpeedControl()
    {
        float speed = (chasing ? chaseSpeed : patrolSpeed);
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().Damage(100f);
            chasing = false;
        }
    }
}
