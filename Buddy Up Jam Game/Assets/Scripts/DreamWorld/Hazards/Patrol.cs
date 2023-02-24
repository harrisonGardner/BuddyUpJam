using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float patrolSpeed = 5f;
    public float chaseSpeed = 10f;

    public float viewDistance = 0.3f;
    public float wallDetectDistance = 0.25f;

    public bool leftStartDirection = false;
    private bool chasing = false;

    public float lifetime = 10f;
    private float lifeTimer = 0f;

    public float attackKnockback = 5f;

    public float invicibiltyTime = 0.25f;
    private float invincibilityCooldown;

    private Rigidbody rb;

    //The amount of time after spawning before it will being to try and detect walls
    private float wallDetectDelay = 2f;
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

        //if (lifeTimer < lifetime)
        //{
        //    lifeTimer += Time.deltaTime;
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (invincibilityCooldown > 0)
        {
            invincibilityCooldown -= Time.deltaTime;
        }

        if (wallDetectDelay > 0)
        {
            wallDetectDelay -= Time.deltaTime;
        }
        else
        {   
            Ray wallRay = new Ray(transform.position, transform.right);
            RaycastHit hit;
            if (Physics.Raycast(wallRay, out hit, wallDetectDistance))
            {
                if (hit.collider.gameObject.CompareTag("TriggerWall"))
                {
                    transform.right *= -1;
                }
            }
        }

        if (Mathf.Abs(transform.position.x) > 100f)
        {
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().OutOfBoundsSpider();
            Kill();
        }
        
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

    /// <summary>
    /// Attempts to kill the enemy
    /// </summary>
    /// <returns>Returns true if the kill was successful</returns>
    public bool Kill()
    {
        if (invincibilityCooldown <= 0f)
        {
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().SpiderKilled();
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            invincibilityCooldown = invicibiltyTime;
            //other.gameObject.transform.position += Vector3.up * 1f;
            other.gameObject.GetComponent<PlayerHealth>().Damage(25f);
            //other.gameObject.GetComponent<Rigidbody>().AddForce((Vector3.up + (rb.velocity.normalized * 115)).normalized * attackKnockback, ForceMode.Impulse);

            chasing = false;
        }
    }
}
