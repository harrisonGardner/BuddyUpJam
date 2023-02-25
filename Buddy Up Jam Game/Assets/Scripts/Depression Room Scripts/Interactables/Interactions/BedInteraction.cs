using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BedInteraction : MonoBehaviour, IInteraction
{
    public void Interact()
    {
        if (LevelTracker.messagesRead)
        {
            //fadeToBlack = true;
            //fadeTimer = 0f;
            SceneFade.Instance.SceneTransition("Dream World");
        }
    }

    private void Update()
    {
        //if (fadeToBlack && !fadeDone)
        //{
        //    fadeTimer += Time.deltaTime;
        //    fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeTimer/fadeSpeed);

        //    SoundManager.Instance.ChangeEffectsVolume(1 - fadeTimer/fadeSpeed);
        //    SoundManager.Instance.ChangeMusicVolume(1 - fadeTimer/fadeSpeed);

        //    if (fadeTimer >= fadeSpeed)
        //    {
        //        fadeDone = true;
        //    }
        //}
        //else if(fadeDone)
        //{
        //    //Transition scene or whatever else
        //    SceneManager.LoadScene("Dream World", LoadSceneMode.Single);
        //}
    }
}
