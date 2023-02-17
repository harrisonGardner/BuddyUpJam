using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("EnvironmentHazard"))
        {
            GetComponent<PlayerHealth>().Damage(collision.gameObject.GetComponent<EnvironmentHazard>().damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            GetComponent<PlayerHealth>().lastCheckpoint = other.gameObject.transform.position;
        }
    }
}
