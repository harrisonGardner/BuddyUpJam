using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedInteraction : MonoBehaviour, IInteraction
{
    private bool fadeToBlack = false;
    private bool fadeDone = false;

    public float fadeSpeed = 5f;
    public float fadeTimer = 0f;
    public Image fadeImage;

    public void Interact()
    {
        fadeToBlack = true;
        fadeTimer = 0f;
    }

    private void Update()
    {
        if (fadeToBlack && !fadeDone)
        {
            fadeTimer += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTimer/fadeSpeed);
            if (fadeTimer >= fadeSpeed)
            {
                fadeDone = true;
            }
        }
        else if(fadeDone)
        {
            //Transition scene or whatever else
            SceneManager.LoadScene("Dream World", LoadSceneMode.Single);
        }
    }
}
