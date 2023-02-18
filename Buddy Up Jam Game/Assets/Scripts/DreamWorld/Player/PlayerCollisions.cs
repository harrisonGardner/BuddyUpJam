using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnvironmentHazard"))
        {
            GetComponent<PlayerHealth>().Damage(collision.gameObject.GetComponent<EnvironmentHazard>().damage);
        }
        else if (collision.gameObject.CompareTag("CrumbleHazard"))
        {
            collision.gameObject.GetComponent<CrumblePlatform>().StartCrumble();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject trigger = other.gameObject;
        if (trigger.CompareTag("Checkpoint"))
        {
            GetComponent<PlayerHealth>().lastCheckpoint = other.gameObject.transform.position;
        }
        else if (trigger.CompareTag("WebHazard"))
        {
            GetComponent<PlayerMovement2D>().FreezePlayer();
            trigger.GetComponent<WebHazard>().SetTarget(other.ClosestPoint(transform.position));
        }
    }
}
