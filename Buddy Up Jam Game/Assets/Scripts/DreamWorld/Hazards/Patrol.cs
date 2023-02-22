using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;

    public float viewDistance;
    public float wallDetectDistance = 0.25f;

    public bool leftStartDirection = false;
    private bool chasing = false;

    public float lifetime = 10f;
    private float lifeTimer = 0f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.right *= (leftStartDirection ? -1 : 1); 
    }

    // Update is called once per frame
    void Update()
    {
        SpeedControl();

        if (lifeTimer < lifetime)
        {
            lifeTimer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        //Ray playerRay = new Ray(transform.position, transform.right);
        //RaycastHit hit;
        //if (Physics.Raycast(playerRay, out hit, viewDistance))
        //{
        //    if (hit.collider.gameObject.CompareTag("Player"))
        //    {
        //        chasing = true;
        //    }
        //    //else if (Vector3.Distance(hit.point, transform.position) < transform.localScale.x + wallDetectDistance)
        //    //{
        //    //    transform.right *= -1;
        //    //    chasing = false;
        //    //}
        //}
    }

    /// <summary>
    /// Default direction is to the right
    /// </summary>
    public void ToggleDirection()
    {
        transform.right *= -1;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().Damage(100f);

            chasing = false;
        }
    }
}
