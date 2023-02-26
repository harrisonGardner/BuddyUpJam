using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        else if (trigger.CompareTag("JumpBoost"))
        {
            GetComponent<PlayerMovement2D>().Jump(trigger.GetComponent<JumpBoost>().boostStrength);
        }
        else if (trigger.CompareTag("Goal"))
        {
            LevelManager.SetLevel(LevelManager.GetLevel() + 1);
            SceneFade.Instance.SceneTransition("Depression Room");
        }
    }
}
