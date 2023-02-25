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
            LevelTracker.SetLevel(LevelTracker.GetLevel() + 1);
            fadeToBlack = true;
        }
    }

    private bool fadeToBlack = false, fadeDone = false;
    private float fadeTimer = 0f;
    public float fadeSpeed = 2.5f;
    public Image fadeImage;

    private void Update()
    {
        if (fadeToBlack && !fadeDone)
        {
            fadeTimer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTimer / fadeSpeed);

            SoundManager.Instance.ChangeEffectsVolume(1 - fadeTimer / fadeSpeed);
            SoundManager.Instance.ChangeMusicVolume(1 - fadeTimer / fadeSpeed);

            if (fadeTimer >= fadeSpeed)
            {
                fadeDone = true;
            }
        }
        else if (fadeDone)
        {
            //Transition scene or whatever else
            SceneManager.LoadScene("Depression Room", LoadSceneMode.Single);
        }
    }
}
