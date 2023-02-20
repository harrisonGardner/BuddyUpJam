using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 100f;

    public Vector3 lastCheckpoint;

    private void Start()
    {
        lastCheckpoint = transform.position;
    }

    public float Health 
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            HealthChangeChecks();
        }
    }

    public void Update()
    {
        if (gameObject.transform.position.y < -50)
        {
            Die();
        }
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        HealthChangeChecks();
    }

    //When the amount of health is changed check for things like death
    private void HealthChangeChecks()
    {
        if (health <= 0)
        {
            Die();
        }
        else if (health > 100f)
        {
            health = 100f;
        }
    }

    private void Die()
    {
        transform.position = lastCheckpoint;
        health = 100f;

        Rigidbody rb = GetComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
        rb.useGravity = true;
    }
}
